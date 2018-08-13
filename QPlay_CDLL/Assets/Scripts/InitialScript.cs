using System;
using UnityEngine;

[ObsoleteAttribute("This Class is obsolete. Use Autotest instead.")]
public class InitialScript : MonoBehaviour
{
    [SerializeField] private int _nodes = 50;

    private void OnEnable()
    {
        var containerInstance = new Container(_nodes);
        Debug.Log(string.Format("Nodes created: {0}", _nodes));
        var workerItem = new Worker(containerInstance);
        var watch = System.Diagnostics.Stopwatch.StartNew();
        watch.Start();
        var count = workerItem.CountNodes();
        watch.Stop();
        Debug.Log(string.Format("Second Nodes counts {0}, Time spent {1}: ", count, watch.ElapsedMilliseconds));
    }
}