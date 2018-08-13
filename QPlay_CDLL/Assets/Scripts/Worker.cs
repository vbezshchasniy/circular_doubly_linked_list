using System.Collections.Generic;

public class Worker
{
    private readonly Container _instance;
    private List<bool> _nodes;

    public Worker(Container instance)
    {
        _instance = instance;
    }

    private void SaveNodeState(bool value)
    {
        _nodes.Add(value);
    }

    private void RemoveLastNode(int index)
    {
        _nodes.RemoveAt(index);
    }

    private void BackupContainer(Container arg = null)
    {
        var instance = arg ?? _instance;

        foreach (var item in _nodes)
        {
            instance.Value = item;
            instance.MoveForward();
        }
    }

    private void MoveBackward(int count, Container arg = null)
    {
        var instance = arg ?? _instance;

        for (var i = 0; i < count; i++)
            instance.MoveBackward();
    }

    private void MoveForward(int count, Container arg = null)
    {
        var instance = arg ?? _instance;

        for (var i = 0; i < count; i++)
            instance.MoveForward();
    }

    public int CountNodes(Container arg = null)
    {
        var instance = arg ?? _instance;
        _nodes = new List<bool>();

        var iteration = 0;
        SaveNodeState(instance.Value);
        instance.Value = true;

        while (true)
        {
            iteration++;
            instance.MoveForward();
            SaveNodeState(instance.Value);

            if (!instance.Value)
                continue;
            instance.Value = false;
            MoveBackward(iteration);
            if (instance.Value)
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
}