using UnityEngine;
using NUnit.Framework;

public class NewEditorTest
{

    [Test]
    public void ContainerElementsSizeTest()
    {
        int nodes = 800;
        Debug.Log("Run ContainerElementsSizeTest");
        Container ContainerInstance = new Container(nodes);
        Debug.Log("Nodes created: " + nodes);
        Worker WorkerItem = new Worker(ContainerInstance);
        Debug.Log("Woker created");
        var watch = System.Diagnostics.Stopwatch.StartNew();
        int count = WorkerItem.CountNodes();
        watch.Stop();
        Debug.Log(string.Format("Nodes counts {0}, Time spent {1}: ", count, WorkerItem.Nodes.Count, watch.ElapsedMilliseconds));

        Assert.AreEqual(nodes, count);
    }
}