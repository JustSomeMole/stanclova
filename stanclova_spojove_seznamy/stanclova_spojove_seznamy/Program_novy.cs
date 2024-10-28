using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Linq;

namespace informatika_ukoly
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Node uzlik = new Node(8); //hodnota a hodnota next, ... bude novým objektem v paměti, který je zpracován prostřednictvím třídy Node
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

            Console.WriteLine(seznam.PrintLinkedList());
            seznam.SortLinkedList();
            Console.WriteLine(seznam.PrintLinkedList());

            LinkedList seznamDva = new LinkedList();
            seznamDva.Add(3);
            seznamDva.Add(-5);
            seznamDva.Add(28);
            seznamDva.Add(0);
            seznamDva.Add(4);

            //otestovani ... prosim funguj
            seznam.PrintLinkedList();
            seznamDva.PrintLinkedList();
            seznam.DestructiveIntersection(seznam, seznamDva);
            seznam.PrintLinkedList();

            Console.ReadLine();
        }
    }

    class Node
    {
        public Node(int value) //konstruktor, ... přijme jen celé číslo
                               //inicializuje nový uzel s hodnotou value, která je nastavena pomocí Value
        {
            Value = value; //hodnota argumentu předaná do konstruktoru je přiřazena vlastnosti Value nově vytvořeného uzlu
        }
        public int Value { get; set; } //deklaruje vlastnost Value, která uchovává hodnotu uzlu (je pouze pro čtení)
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

        public string PrintLinkedList()
        {
            if (Head == null)
                return "Seznam je prázdný";
            Node praveTed = Head;
            StringBuilder listCisel = new StringBuilder();

            while (praveTed != null)
            {
                listCisel.Append(praveTed.Value + " ");
                praveTed = praveTed.Next;
            }
            return listCisel.ToString();
        }

        public void SortLinkedList()
        {
            if (Head == null)
                return;

            Node praveTed = Head;
            int promena = 0;
            bool uz = false;
            while (true)
            {
                uz = false;
                praveTed = Head;
                while (praveTed.Next != null)
                {
                    if (praveTed.Value > praveTed.Next.Value)
                    {
                        promena = praveTed.Value;
                        praveTed.Value = praveTed.Next.Value;
                        praveTed.Next.Value = promena;
                        uz = true;
                    }
                    praveTed = praveTed.Next;
                }
                if (uz == false)
                    break;
            }
        }

        public void ClearList()
        {
            Head = null;
        }

        public static void DestructiveIntersection(LinkedList list1, LinkedList list2)
        {
            Node praveTed1 = list1.Head;
            Node predTed1 = null;

            while (praveTed1 != null)
            {
                Node praveTed2 = list2.Head;
                bool nalezenoVlist2 = false;

                while (praveTed2 != null)
                {
                    if (praveTed1.Value == praveTed2.Value) //najdu stejný prvek ... v obou seznamech
                    {
                        nalezenoVlist2 = true;
                        break;
                    }
                    praveTed2 = praveTed2.Next;
                }

                if (nalezenoVlist2 == false) //pokud jsem nenašla stejný prvek v seznamu2 jako prvek v 1. seznamu
                {
                    if (predTed1 != null) //před prvkem něco je
                    {
                        predTed1.Next = praveTed1.Next; //sloučim je dohromady, hus fuc prvek = odstranění
                    }
                    else //pokud před prvkem nic není -> predTed1 je null ... musím hlavu posunout na další prvek, jinak bych byla v pytli (to už jsem)
                    {
                        list1.Head = praveTed1.Next;
                    }
                    Node chciOdstranit = praveTed1;
                    praveTed1 = praveTed1.Next;
                    chciOdstranit = null;
                }
                else //našla jsem, takže nic nedělám
                {
                    predTed1 = praveTed1;
                    praveTed1 = praveTed1.Next;
                }
            }
            list2.ClearList();
        }
    }
}
