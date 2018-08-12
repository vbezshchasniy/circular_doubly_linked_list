public class ContainerDebug : Container
{
    public static int number;
    private class Node
    {
        public Node Next;
        public Node Prev;
        public bool Value;
        public int Number = 0;


        public Node(Node prev)
        {
            Value = UnityEngine.Random.value < 0.5f;
            Prev = prev;
            Number = number++;
        }
    }

    private Node current;
    private readonly int count;

    public ContainerDebug(int count = 0) : base(count)
    {
        number = 0;
    }

    public int Number
    {
        get { return current.Number; }
        set { current.Number = value; }
    }
}