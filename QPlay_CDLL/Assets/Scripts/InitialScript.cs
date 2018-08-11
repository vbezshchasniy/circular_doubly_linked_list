using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialScript : MonoBehaviour {

    [SerializeField] private int Nodes = 50;

    // Use this for initialization
    void Start () {
		
	}

    private void OnEnable()
    {
        Debug.Log("New");
        Container ContainerInstance = new Container(Nodes);
        Debug.Log("Nodes created: " + Nodes);

        Worker WorkerItem = new Worker(ContainerInstance);
        Debug.Log("Woker created");

        var watch = System.Diagnostics.Stopwatch.StartNew();
        int count = WorkerItem.CountNodes();
        watch.Stop();
        Debug.Log(string.Format("First Nodes counts {0}, Time spent {1}: ", count, watch.ElapsedMilliseconds));
        watch.Reset();
        watch.Start();
        int count2 = WorkerItem.CountNodes2();
        watch.Stop();
        Debug.Log(string.Format("Second Nodes counts {0}, Time spent {1}: ", count2, watch.ElapsedMilliseconds));
    }

    // Update is called once per frame
    void Update () {
		
	}
}
