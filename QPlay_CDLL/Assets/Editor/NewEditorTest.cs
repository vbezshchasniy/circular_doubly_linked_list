using UnityEngine;
using NUnit.Framework;

public class NewEditorTest
{

    [Test]
    public void ContainerElementsSizeTest()
    {
        const int nodes = 800;
        Debug.Log("Run ContainerElementsSizeTest");
        var containerInstance = new Container(nodes);
        Debug.Log(string.Format("Nodes created: {0}", nodes));
        var workerItem = new Worker(containerInstance);
        Debug.Log("Woker created");
        var watch = System.Diagnostics.Stopwatch.StartNew();
        var count = workerItem.CountNodes();
        watch.Stop();
        Debug.Log(string.Format("Nodes counts {0}, Time spent {1}: ", count, watch.ElapsedMilliseconds));

        Assert.AreEqual(nodes, count);
    }
}