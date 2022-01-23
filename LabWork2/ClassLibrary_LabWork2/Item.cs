using System;
using System.Collections;
using System.Collections.Generic;

namespace ClassLibrary_LabWork2
{
    public class Item<T>
    {
        public T Data { get; set; } //значення елементу
        public Item<T> Next { get; set; } //посилання на наступний
        public Item<T> Previous { get; set; } //посилання на попередній
        public Item(T data) // конструктор
        {
            Data = data;
        }
    }
}
