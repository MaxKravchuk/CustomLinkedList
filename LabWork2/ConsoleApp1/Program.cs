using System;
using System.Collections;
using System.Collections.Generic;
using ClassLibrary_LabWork2;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomLL<int> s = new CustomLL<int>(1, 2, 3, 4, 5, 6, 7);
            s.Notify += DisplayMsg;
            s.AddAfter(6, 56);
            s.AddBefore(2, 65);
            s.Remove(65);

            Console.Write("\n-----------\n");
            foreach (var i in s)
            {
                Console.Write(i + " ");
            }
            Console.Write("\n-----------\n");

            Console.WriteLine($"Count - {s.Count}");
        }

        private static void DisplayMsg(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
