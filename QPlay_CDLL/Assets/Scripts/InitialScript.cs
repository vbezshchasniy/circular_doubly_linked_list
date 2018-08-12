using System;
using UnityEngine;

[ObsoleteAttribute("This Class is obsolete. Use Autotest instead.")]
public class InitialScript : MonoBehaviour {

    [SerializeField] private int Nodes = 50;

    // Use this for initialization
    void Start () {
		
	}

    private void OnEnable()
    {
        Container ContainerInstance = new Container(Nodes);
        Debug.Log("Nodes created: " + Nodes);
        Worker WorkerItem = new Worker(ContainerInstance);
        var watch = System.Diagnostics.Stopwatch.StartNew();
        watch.Start();
        int count = WorkerItem.CountNodes();
        watch.Stop();
        Debug.Log(string.Format("Second Nodes counts {0}, Time spent {1}: ", count, watch.ElapsedMilliseconds));
    }

    // Update is called once per frame
    void Update () {
		
	}
}
