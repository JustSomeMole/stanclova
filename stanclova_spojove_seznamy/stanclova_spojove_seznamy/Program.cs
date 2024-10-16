namespace stanclova_spojove_seznamy;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        Node uzlik = new Node(8); //hodnota a hodnota next
    }
}

class Node
{
    public Note(int value) //konstruktor
    {
        Value = value;
    }
    public int Value { get; }
    public Node Next { get; set; }
}

class LinkedList
{
    public Node Head { get; set; }

    public void Add(int value)
    {
        if (Head == null) //když je senznam prázdný
            Head = new Node(value);
        else
        {
            Node newNode = new Node(value);
            newNode.Next = Head;
            Head = newNode;
        }
    }

    public bool Search(int value)
    {
        Node node = Head;
        while (node.Next != null)
        {
            if(node.Value == value) 
                return true;
            node = node.Next;
        }
        return false;
    }
}
