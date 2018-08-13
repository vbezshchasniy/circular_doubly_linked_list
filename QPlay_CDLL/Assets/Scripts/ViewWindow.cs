using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ViewWindow : EditorWindow
{
    [MenuItem("Window/View Window")]
    public static void ShowWindow()
    {
        GetWindowWithRect<ViewWindow>(new Rect(0, 0, Width, Height));
    }

    private enum Direction
    {
        None = 0,
        Up,
        Down
    };

    private const string ButtonName = "Create New";
    private const int Width = 400;
    private const int Height = 800;
    private const int Padding = 5;
    private const int PaddingY = 15;

    private const int ItemWidth = 400;
    private const int ItemHieght = 100;
    private const int Items = Height / ItemHieght;

    /// <summary>
    /// items[8] up
    /// items[8] currently seen
    /// items[8] down
    /// </summary>
    private const int FullReel = Items * 3;


    List<bool> CurrentList = new List<bool>(FullReel);
//    List<int> CurrentListI = new List<int>(Items);

    Vector2 Scroll;
    private Container ContainerInstance;
    private GUIStyle StyleForTrue;
    private GUIStyle StyleForFalse;

    private void OnEnable()
    {
        Scroll = new Vector2(0, 1);
        InitGUIStyle();
        IninContainer();
    }

    private void IninContainer()
    {
        ContainerInstance = new Container();
        GetNodes(Direction.None);
    }

    private void CreateBoxes(Direction direction = Direction.None)
    {
        if (direction != Direction.None)
            Scroll = new Vector2(0, direction == Direction.Up ? Height : 1);
        DrawBoxes(CurrentList);
    }

    private void OnGUI()
    {
        var scrollView = new Rect(0, 0, Width, Height);
        var viewRect = new Rect(0, 0, Width, Height * 3);

        Scroll = GUI.BeginScrollView(scrollView, Scroll, viewRect, false, false, GUIStyle.none, GUIStyle.none);

        if (Scroll.y < 1f)
        {
            GetNodes(Direction.Up);
            CreateBoxes(Direction.Up);
        }

        else if (Scroll.y > Height + 1)
        {
            GetNodes(Direction.Down);
            CreateBoxes(Direction.Down);
        }
        else
            CreateBoxes();

        GUI.EndScrollView();

        Debug.Log(Scroll);

        if (GUI.Button(new Rect(0, Height - ItemHieght, Width, ItemHieght), ButtonName))
            IninContainer();
    }

    private void DrawBoxes(List<bool> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int boxY = i * ItemHieght + PaddingY - Height;
            Rect rectBox = new Rect(Padding, boxY, ItemWidth - Padding * 2, ItemHieght - PaddingY);
            bool value = list[i];
            GUI.Box(rectBox, string.Format("<b>{0}</b>", value), value ? StyleForTrue : StyleForFalse);
//            GUI.Box(rectBox, string.Format("<b> Number: {0} - {1}</b>", CurrentListI[i], value),
//                value ? StyleForTrue : StyleForFalse);
        }
    }


    private void GetNodes(Direction direction)
    {
        CurrentList.Clear();
//        CurrentListI.Clear();

        switch (direction)
        {
            case Direction.None:
                for (int i = 0; i < Items; i++)
                    ContainerInstance.MoveBackward();


                for (int i = 0; i < FullReel; i++)
                {
                    CurrentList.Add(ContainerInstance.Value);
//                    CurrentListI.Add(ContainerInstance.Number);
                    ContainerInstance.MoveForward();
                }

                break;
            case Direction.Down:
                // Move 2 screens down
                for (int i = 0; i < Items * 2; i++)
                    ContainerInstance.MoveBackward();


                for (int i = 0; i < FullReel; i++)
                {
                    CurrentList.Add(ContainerInstance.Value);
//                    CurrentListI.Add(ContainerInstance.Number);
                    ContainerInstance.MoveForward();
                }

                break;
            case Direction.Up:

                for (int i = 0; i < FullReel + Items; i++)
                    ContainerInstance.MoveBackward();

                for (int i = 0; i < FullReel; i++)
                {
                    CurrentList.Add(ContainerInstance.Value);
//                    CurrentListI.Add(ContainerInstance.Number);

                    ContainerInstance.MoveForward();
                }

                break;
            default:
                throw new ArgumentOutOfRangeException("direction", direction, null);
        }
    }

    private void InitGUIStyle()
    {
        StyleForTrue = new GUIStyle();
        StyleForFalse = new GUIStyle();
        Texture2D TextureForTrue = new Texture2D(1, 1);
        Texture2D TextureForFalse = new Texture2D(1, 1);
        for (int y = 0; y < 1; ++y)
        {
            for (int x = 0; x < 1; ++x)
            {
                TextureForTrue.SetPixel(x, y, new Color(0.64f, 1f, 0.75f));
                TextureForFalse.SetPixel(x, y, new Color(1f, 0.64f, 0.58f));
            }
        }

        TextureForTrue.Apply();
        TextureForFalse.Apply();
        StyleForTrue.normal.background = TextureForTrue;
        StyleForTrue.alignment = TextAnchor.MiddleCenter;
        StyleForTrue.richText = true;
        StyleForFalse.normal.background = TextureForFalse;
        StyleForFalse.alignment = TextAnchor.MiddleCenter;
        StyleForFalse.richText = true;
    }
}