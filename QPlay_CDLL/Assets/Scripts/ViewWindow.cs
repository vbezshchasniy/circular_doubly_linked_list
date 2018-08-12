using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class ViewWindow : EditorWindow
{
    [MenuItem("Window/View Window")]
    public static void ShowWindow()
    {
        GetWindowWithRect<ViewWindow>(new Rect(0, 0, 400, 800));
    }

    private enum Direction
    {
        None = 0,
        Up,
        Down
    };

    private const int Width = 400;
    private const int Height = 800;
    private const int Items = Height / ItemHieght * 3;

    private const int Padding = 5;
    private const int ItemHieght = 100;
    private const int ItemWidth = 400;

    private static readonly Vector2 ItemSize = new Vector2(ItemWidth, ItemHieght);

    private const string ButtonName = "Create New";

    List<bool> CurrentList = new List<bool>(Items);
    Vector2 Scroll;
    private Container ContainerInstance;
    private GUIStyle StyleForTrue;
    private GUIStyle StyleForFalse;

    void OnEnable()
    {
        Scroll = new Vector2(0, 0);
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
            Scroll = new Vector2(0, direction == Direction.Up ? Height : 0);
        DrawBoxes(CurrentList);
    }

    void OnGUI()
    {
        Rect ScrollView = new Rect(0, 0, Width, Height - ItemSize.y);
        Rect ViewRect = new Rect(0, 0, Width, Height - ItemSize.y);

        Scroll = GUI.BeginScrollView(ScrollView, Scroll, ViewRect, false, false, GUIStyle.none, GUIStyle.none);

        GUI.EndScrollView();

        if (Scroll.y <= 0.0f)
        {
            GetNodes(Direction.Up);
            CreateBoxes(Direction.Up);
        }
        else if (Scroll.y >= Height)
        {
            GetNodes(Direction.Down);
            CreateBoxes(Direction.Down);
        }
        else
            CreateBoxes();

        if (GUI.Button(new Rect(0, Height - ItemSize.y, Width, ItemSize.y), ButtonName))
        {
            IninContainer();
        }
    }

    private void DrawBoxes(List<bool> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int boxY = i * ItemHieght + Padding - Height;
            Rect rectBox = new Rect(Padding, boxY, ItemWidth - Padding * 2, ItemHieght - Padding);
            bool value = list[i];
            GUI.Box(rectBox, "<b>"+value.ToString()+"</b>", value ? StyleForTrue : StyleForFalse);
        }
    }


    private void GetNodes(Direction direction)
    {
        CurrentList.Clear();

        switch (direction)
        {
            case Direction.None:
            case Direction.Down:
                for (int i = 0; i < Items / 3; i++)
                    ContainerInstance.MoveBackward();


                for (int i = 0; i < Items * 2 / 3; i++)
                {
                    CurrentList.Add(ContainerInstance.Value);
                    ContainerInstance.MoveForward();
                }

                break;
            case Direction.Up:
                for (int i = 0; i < Items / 3; i++)
                    ContainerInstance.MoveForward();


                for (int i = 0; i < Items * 2 / 3; i++)
                {
                    CurrentList.Add(ContainerInstance.Value);
                    ContainerInstance.MoveBackward();
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