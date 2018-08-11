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

    // Use this for initialization
    void OnEnable()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int CountNodes(Container instance = null)
    {
        Container Instance =  instance ?? _instance;
        _nodes = new List<bool>();

        Instance.MoveBackward();
        bool endCachedValue = Instance.Value;
        Instance.MoveForward();

        while (true)
        {
            _nodes.Add(Instance.Value);
            int cnt = _nodes.Count;
            MoveBackward(cnt);
            if (_nodes[cnt - 1] != Instance.Value)
            {
                MoveForward(cnt + 1);
                continue;
            }

            bool tmp = Instance.Value;
            Instance.Value = !Instance.Value;
            MoveForward(cnt);
            if (tmp != Instance.Value)
            {
                _nodes[cnt - 1] = endCachedValue;
                break;
            }

            Instance.Value = !Instance.Value;
            Instance.MoveForward();
        }

        return _nodes.Count;
    }

    public int CountNodes2(Container instance = null)
    {
        Container Instance = instance ?? _instance;
        _nodes = new List<bool>();

        SaveNodeState(Instance.Value);
        _nodes.Add(Instance.Value);
        int iteration = 1;
        Instance.Value = true;

        while (true)
        {
            Instance.MoveForward();
            SaveNodeState(Instance.Value);
            if (Instance.Value)
            {
                Instance.Value = false;
                MoveBackward(iteration);
                if (Instance.Value)
                {
                    MoveForward(iteration);
                    iteration++;
                    continue;
                }
                else
                {
                    BackupContainer();
                    return iteration;
                }
            }
            iteration++;
        }
    }

    private void SaveNodeState(bool value)
    {
        _nodes.Add(value);
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
