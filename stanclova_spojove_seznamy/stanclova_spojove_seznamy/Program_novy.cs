using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;

namespace informatika_ukoly
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LinkedList seznam = new LinkedList();
            seznam.Add(4);
            seznam.Add(0);
            seznam.Add(-5);
            seznam.Add(25);

            int x = seznam.SearchForMin();
            if (x == Int32.MaxValue)
            {
                Console.WriteLine("Seznam je prázdný.");
            }
            else
            {
                Console.WriteLine("Nalezeno minimum: " + x);
            }
            //Node uzlik = new Node(8); //hodnota a hodnota next, ... bude novým objektem v paměti, který je zpracován prostřednictvím třídy Node
        }
    }

    class Node
    {
        public Node(int value) //konstruktor, ... přijme jen celé číslo
                               //inicializuje nový uzel s hodnotou value, která je nastavena pomocí Value
        {
            Value = value; //hodnota argumentu předaná do konstruktoru je přiřazena vlastnosti Value nově vytvořeného uzlu
        }
        public int Value { get; } //deklaruje vlastnost Value, která uchovává hodnotu uzlu (je pouze pro čtení)
        public Node Next { get; set; } //odkazuje na další uzel v seznamu (nebo je null, pokud je poslední)
    }

    class LinkedList
    {
        public Node Head { get; set; } //public = je dostupná z jakéhokoliv místa v programu
                                       //Node = odkaz na třídu ... tady to znamená, že se bavíme o jednom uzlu
                                       //head = název vlastnosti ... první uzel v seznamu -> funkce bude odkazovat na uzel 1. v seznamu

        public void Add(int value)
        {
            if (Head == null) //když je senznam prázdný
                Head = new Node(value); //vložíme do ukazatele na první prvek (Head) nový prvek typu Node
            else
            {
                Node newNode = new Node(value); //nový prvek typu Node
                newNode.Next = Head; //jeho ukazatel na další prvek (Next) nastavíme na prvek, kam ukazovala hlava seznamu -> přidáváme před původní první prvek
                Head = newNode; //přehodíme hlavu, aby ukazovala na nový první prvek
            }
        }

        public bool Search(int value)
        {
            Node node = Head; //vytvoříme si pomocnou proměnnou, ve které bude aktuální prohlížený prvek. Na začátku je jím hlava, tedy prvek první.
            while (node.Next != null) //dokud není konec seznamu
            {
                if (node.Value == value) //pokud prvek se stejnou hodnotou jakou hledáme
                    return true;
                node = node.Next;
            }
            return false;
        }

        public int SearchForMin()
        {
            if (Head == null)
            {
                //Console.WriteLine("Prázdný seznam, vráceno MaxInt.");
                return Int32.MaxValue;
            }

            else
            {
                Node node = Head;
                int minValue = Head.Value;
                while (node.Next != null)
                {
                    if (node.Value < minValue)
                    {
                        minValue = node.Value;
                    }
                    node = node.Next;
                }
                //Console.WriteLine("Nalezené minimum: " + minValue);
                return minValue;
            }
        }
    }
}
