﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace binarz_search_tree
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // odtud by mělo být přístupné jen to nejdůležitější, žádné vnitřní pomocné implementace.
            // Strom a jeho metody mají fungovat jako černá skříňka, která nám nabízí nějaké úkoly a my se nemusíme starat o to, jakým postupem budou splněny.
            // rozhodně také nechceme mít možnost datovou stukturu nějak měnit jinak, než je dovoleno (třeba nějakým jiným způsobem moct přidat nebo odebrat uzly, aniž by platili invarianty struktury)

            BinarySearchTree<Student> tree = new BinarySearchTree<Student>();

            // čteme data z CSV souboru se studenty (soubor je uložen ve složce projektu bin/Debug u exe souboru)
            // CSV je formát, kdy ukládáme jednotlivé hodnoty oddělené čárkou
            // v tomto případě: Id,Jméno,Příjmení,Věk,Třída
            using (StreamReader streamReader = new StreamReader("studenti_shuffled.csv"))
            {
                string line = streamReader.ReadLine();
                while (line != null)
                {
                    string[] studentData = line.Split(',');

                    Student student = new Student(
                        Convert.ToInt32(studentData[0]),    // Id
                        studentData[1],                     // Jméno
                        studentData[2],                     // Příjmení
                        Convert.ToInt16(studentData[3]),    // Věk
                        studentData[4]);                    // Třída

                    // vložíme studenta do stromu, jako klíč slouží jeho Id
                    tree.Insert(student.Id, student);
                    line = streamReader.ReadLine();
                }
            }
            Console.WriteLine(tree.Find(20).Value);

        }
    }

    // Na uzel si vytvoříme třídu, protože u něj chceme sledovat vícero vlastností: klíč, uloženou hodnotu, levý a pravý syn v budoucím stromě
    // uzel sám o sobě však nic nedělá => nemá žádné metody
    class Node<T> // Použijeme generický typ T, aby hodnota v uzlu mohla být cokoli (číslo, text, vlastní datový typ...) si chceme organizovaně ukládat
    {
        public int Key { get; set; }    // aby se jednalo o vlastnost, použijeme tzv. getter a setter
                                        // oba jsou public, jelikož klíč uzlu budeme potřebovat měnit z metod stromu
                                        // veřejná vlastnost vždy začíná velkým písmenem
        public T Value { get; set; }
        public Node(int key, T value)
        {
            Key = key; // hodnotu přiřadím vlastnostem třídy
            Value = value;
            LeftSon = null; RightSon = null;
        }

        public Node<T> LeftSon { get; set; }
        public Node<T> RightSon { get; set; }
    }


    class BinarySearchTree<T> // zde potřebujeme opět uvést generický typ T a podle toho vytářet uzly stromu daného typu
    {
        public Node<T> Root { get; private set; } // kořen stromu nastavujeme interně, ne z žádné jiné třídy => private set 


        public void Insert(int newKey, T newValue) // chceme, aby nikdo zvenku nemusel specifikovat kořen stromu, a dtrom sám ví, co je jeho kořen => rozdělíme na public Insert a rekurzivní private _insert
        {
            Node<T> _insert(Node<T> node, int newKey, T newValue)
            {

            }


            if (Root == null)
            {
                Root = new Node<T>(newKey, newValue);
            }
            else
                _insert(Root, newKey, newValue);
        }


        public Node<T> Find(int key)
        {
            Node<T> _find(Node<T> node, int key2) // privátní funkci mohu založit i uvnitř jiné funkce. Je pak viditelná, jen z té vnější funkce
                                                  // key ... např node 3, tak ta 3
            {
                if (node == null)
                    return null;

                if (key2 == node.Key)
                    return node;

                if (key2 > node.Key)
                    return _find(node.RightSon, key2);

                else
                    return _find(node.LeftSon, key2);
            }
            return _find(Root, key); // nebo pokud bychom chtěli vracet číslo, tak by šlo použít default

        }


        public string Show()
        {
            void _show(Node<T> node, StringBuilder nodes) //uděláme privátní funkci jen zde; to první je kořen
            {
                if (node != null)
                {
                    _show(node.LeftSon, nodes); 
                    nodes.Append(node.Key); 
                    nodes.Append(" ");
                    _show(node.RightSon, nodes);
                }
            }

            if (Root == null)
            {
                return "Strom je prázdný.";
            }

            StringBuilder sb = new StringBuilder(); //využijeme StringBuilder, abychom nevytvářely s každým dalším klíčem nový string => časově i paměťově daleko méně náročné než += u stringu
                                                    // VLASTNĚ STEJNÉ JAKO STRING DO KTERÉHO PŘIDÁVÁŠ: string += node.Key.String() + ""
            _show(Root, sb);

            return sb.ToString(); // výpis ponecháme jednou naráz v Mainu, WriteLine do konzole je časově drahá operace
        }


        public Node<T> Min()
        {
            return _min(Root);
        }
        private Node<T> _min(Node<T> node)
        {
            if (node.LeftSon == null)
                return node;

            return _min(node.LeftSon);
        }

    }

    class Student
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public int Age { get; }

        public string ClassName { get; }

        public Student(int id, string firstName, string lastName, int age, string ClassName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        // aby se nám při Console.WriteLine(student) nevypsala jen nějaká adresa v paměti,
        // upravíme výpis objektu typu student na něco čitelného
        public override string ToString()
        {
            return string.Format("{0} {1} (ID: {2}) ze třídy {3}", FirstName, LastName, Id, ClassName);
        }
    }
}