using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour {

    [SerializeField] private int Nodes = 50;

    private Container ContainerInstance;
    private List<bool> TmpNodes;


    // Use this for initialization
    void OnEnable () {
        Debug.Log("Start");
        ContainerInstance = new Container(Nodes);
        Debug.Log("Nodes created: " + Nodes);
        int count = CountNodes();
        Debug.Log("Nodes counts: " + count);
    }

    // Update is called once per frame
    void Update () {
		
	}

    private int CountNodes()
    {
        TmpNodes = new List<bool>();
        ContainerInstance.MoveBackward();
        bool endCachedValue = ContainerInstance.Value;
        ContainerInstance.MoveForward();

        while (true)
        {
            TmpNodes.Add(ContainerInstance.Value);
            int cnt = TmpNodes.Count;
            MoveBackward(cnt);
            if (TmpNodes[cnt-1] != ContainerInstance.Value)
            {
                MoveForward(cnt + 1);
                continue;
            }

            bool tmp = ContainerInstance.Value;
            ContainerInstance.Value = !ContainerInstance.Value;
            MoveForward(cnt);
            if (tmp != ContainerInstance.Value)
            {
                TmpNodes[cnt - 1] = endCachedValue;
                break;
            }

            ContainerInstance.Value = !ContainerInstance.Value;
            ContainerInstance.MoveForward();
        }

        return TmpNodes.Count;
    }

    private void MoveBackward(int count)
    {
        for (int i = 0; i < count; i++)
            ContainerInstance.MoveBackward();
    }

    private void MoveForward(int count)
    {
        for (int i = 0; i < count; i++)
            ContainerInstance.MoveForward();
    }
}
