using System;
using System.Collections.Generic;

public class Worker
{
    private Container _instance;
    private List<bool> _nodes;

    public Worker(Container instance)
    {
        _instance = instance;
    }

    public List<bool> Nodes
    {
        get
        {
            return _nodes;
        }
    }

    public int CountNodes(Container instance = null)
    {
        Container Instance = instance ?? _instance;
        _nodes = new List<bool>();

        int iteration = 0;
        SaveNodeState(Instance.Value);
        Instance.Value = true;

        while (true)
        {
            iteration++;
            Instance.MoveForward();
            SaveNodeState(Instance.Value);

            if (!Instance.Value)
                continue;
            Instance.Value = false;
            MoveBackward(iteration);
            if (Instance.Value)
            {
                MoveForward(iteration);
            }
            else
            {
                RemoveLastNode(iteration);
                BackupContainer();
                return iteration;
            }
        }
    }

    private void SaveNodeState(bool value)
    {
        _nodes.Add(value);
    }

    private void RemoveLastNode(int index)
    {
        _nodes.RemoveAt(index);
    }

    private void BackupContainer(Container instance = null)
    {
        Container Instance = instance ?? _instance;

        for (int i = 0; i < _nodes.Count; i++)
        {
            Instance.Value = _nodes[i];
            Instance.MoveForward();
        }
    }

    private void MoveBackward(int count, Container instance = null)
    {
        Container Instance = instance ?? _instance;

        for (int i = 0; i < count; i++)
            Instance.MoveBackward();
    }

    private void MoveForward(int count, Container instance = null)
    {
        Container Instance = instance ?? _instance;

        for (int i = 0; i < count; i++)
            Instance.MoveForward();
    }

}
