using System;
using System.Collections;
using System.Collections.Generic;

namespace ClassLibrary_LabWork2
{
    public class CustomLL<T> : IEnumerable<T>
    {
        private Item<T> head; //початок списку
        private Item<T> tail; //кінець списку
        private int count; //кількість е-нтів

        public delegate void ListHandler(string msg);
        public event ListHandler Notify;

        public Item<T> Head //властивість
        {
            get => head;
        }

        public Item<T> Tail //властивість
        {
            get => tail;
        }

        public int Count //властивість
        {
            get => count;
            private set => count = value;
        }

        public bool IsEmpty => count == 0;

        public CustomLL(params T[] values) //конструктор
        {
            if (values == null) throw new ArgumentNullException(new string("Values"));
            foreach (var x in values)
            {
                Add(x);
            }
        }

        public void Add(T item) //метод додавання елемента
        {
            if (item == null) throw new ArgumentNullException(new string("item"));
            Item<T> node = new Item<T>(item);

            if (head == null)
                head = node;
            else
            {
                tail.Next = node;
                node.Previous = tail;
            }
            tail = node;
            count++;
            Notify?.Invoke($"Value has been added; Count - {count}");
        }

        public void AddFirst(T item) //додавання як першого
        {
            if (item == null) throw new ArgumentNullException(new string("item"));
            Item<T> node = new Item<T>(item);
            Item<T> temp = head;
            node.Next = temp;
            head = node;
            if (count == 0)
                tail = head;
            else
                temp.Previous = node;
            count += 1;
            Notify?.Invoke($"Value has been added; Count - {count}");
        }

        public void AddLast(T item) //додавання як останнього
        {
            if (item == null) throw new ArgumentNullException(new string("item"));
            Item<T> node = new Item<T>(item);
            Item<T> temp = tail;
            node.Previous = temp;
            tail = node;
            if (count == 0)
                head = tail;
            else
                temp.Next = node;
            count++;
            Notify?.Invoke($"Value has been added; Count - {count}");
        }

        public void AddAfter(int index, T item) //додавання після
        {
            if (index > count) throw new ArgumentException(new string("index"));
            if (item == null) throw new ArgumentNullException(new string("item"));

            Item<T> newItem = new Item<T>(item);

            Item<T> node = head;
            for (int i = 0; i < index - 1; i++)
            {
                node = node.Next;
            }

            if (index == count)
            {
                AddLast(item);
            }
            else
            {
                Item<T> temp = node.Next;
                node.Next = newItem;
                newItem.Previous = node;
                newItem.Next = temp;
                temp.Previous = newItem;
            }
            count++;
            Notify?.Invoke($"Value has been added after; Count - {count}");
        }

        public void AddBefore(int index, T item) //додавання перед
        {
            if (index > count) throw new ArgumentException(new string("index"));
            if (item == null) throw new ArgumentNullException(new string("item"));

            Item<T> newItem = new Item<T>(item);

            Item<T> node = head;
            for (int i = 0; i < index - 1; i++)
            {
                node = node.Next;
            }

            if (index == 1)
            {
                AddFirst(item);
            }
            else
            {
                Item<T> temp = node.Previous;
                node.Previous = newItem;
                newItem.Next = node;
                newItem.Previous = temp;
                temp.Next = newItem;
            }
            count++;
            Notify?.Invoke($"Value has been added before; Count - {count}");
        }

        public bool Remove(T item) //видалення конкретного
        {
            if (item == null) throw new ArgumentNullException(new string("item"));

            Item<T> current = head;
            while (current != null)
            {
                if (current.Data.Equals(item))
                {
                    break;
                }
                current = current.Next;
            }

            if (current != null)
            {
                if (current.Next != null)
                {
                    current.Next.Previous = current.Previous;
                }
                else
                {
                    tail = current.Previous;
                }

                if (current.Previous != null)
                {
                    current.Previous.Next = current.Next;
                }
                else
                {
                    head = current.Next;
                }
                count -= 1;
                Notify?.Invoke($"Value has been removed; Count - {count}");
                return true;
            }
            return false;
        }

        public bool RemoveLast() //видалення останнього
        {
            if (Remove(this[count]))
            {
                Notify?.Invoke($"Value has been removed; Count - {count}");
                return true;
            }
            return false;
        }

        public bool RemoveFirst() //видалення першого
        {
            if (Remove(this[1]))
            {
                Notify?.Invoke($"Value has been removed; Count - {count}");
                return true;
            }
            return false;
        }

        public void Clear() //видалення всього
        {
            if (count == 0) throw new ArgumentNullException(new string("count = 0"));
            head = null;
            tail = null;
            count = 0;
            Notify?.Invoke($"List cleared; Count - {count}");
        }

        public bool Contains(T item) //перевірка на наявність
        {
            if (item == null) throw new ArgumentNullException(new string("item"));
            Item<T> current = head;
            while (current != null)
            {
                if (current.Data.Equals(item))
                    return true;
                current = current.Next;
            }
            return false;
        }
        /*
        public bool Show() //вивід
        {
            Console.WriteLine("///////////");
            Item<T> temp = head;
            while (temp.Next != null)
            {
                Console.Write(temp.Data + " ");
                temp = temp.Next;
            }
            Console.Write(temp.Data);
            Console.WriteLine();
            Console.WriteLine("///////////");
            return true;
        }
        */
        public T this[int index] //індексатор
        {
            get
            {
                if (index < 0 || index > count) throw new IndexOutOfRangeException(new string("index"));
                Item<T> current = head;
                for (int i = 0; i < index - 1; i++)
                    current = current.Next;
                return current.Data;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            Item<T> current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }

        public IEnumerable<T> BackEnumerator()
        {
            Item<T> current = tail;
            while (current != null)
            {
                yield return current.Data;
                current = current.Previous;
            }
        }
    }
}
