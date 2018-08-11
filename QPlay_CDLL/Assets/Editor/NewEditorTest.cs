using UnityEngine;
using NUnit.Framework;

public class NewEditorTest
{

    [Test]
    public void ContainerElementsSizeTest()
    {
        Debug.Log("Run ContainerElementsSizeTest");
        Container ContainerInstance = new Container(50);
        Debug.Log("Nodes created: " + 50);
        Worker WorkerItem = new Worker(ContainerInstance);
        Debug.Log("Woker created");
        var watch = System.Diagnostics.Stopwatch.StartNew();
        int count = WorkerItem.CountNodes2();
        watch.Stop();
        Debug.Log(string.Format("Nodes counts {0}, Time spent {1}: ", count, watch.ElapsedMilliseconds));

        Assert.AreEqual(50, count);
    }
}