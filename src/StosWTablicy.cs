using System;
using System.Collections;
using System.Collections.Generic;

namespace Stos
{
    public class StosWTablicy<T> : IStos<T>
    {
        private T[] tab;
        private int szczyt = -1;

        public StosWTablicy(int size = 10)
        {
            tab = new T[size];
            szczyt = -1;
        }

        public T Peek => IsEmpty ? throw new StosEmptyException() : tab[szczyt];

        public int Count => szczyt + 1;

        public bool IsEmpty => szczyt == -1;

        public void Clear() => szczyt = -1;

        public T this[int index] => index > Count - 1 ? throw new IndexOutOfRangeException() : tab[index];

        public T Pop() => IsEmpty ? throw new StosEmptyException() : tab[szczyt--];

        public void Push(T value)
        {
            if (Count + 1 >= tab.Length)
            {
                var temp = new T[2 * Count];
                Array.Copy(tab, temp, tab.Length);
                tab = temp;
            }
            tab[++szczyt] = value;
        }

        public T[] ToArray()
        {
            //return tab;  //bardzo źle - reguły hermetyzacji

            //poprawnie:
            T[] temp = new T[szczyt + 1];
            for (int i = 0; i < temp.Length; i++)
                temp[i] = tab[i];
            return temp;
        }

        public void TrimExcess() => Array.Resize(ref tab, (int) ((tab.Length - (tab.Length - szczyt - 1)) * 1.1));

        private class EnumeratorStosu : IEnumerator<T>
        {
            private StosWTablicy<T> stos;
            private int position = -1;

            internal EnumeratorStosu(StosWTablicy<T> stos) => this.stos = stos;

            public T Current => stos.tab[position];

            object IEnumerator.Current => Current;

            public void Dispose() { }

            public bool MoveNext()
            {
                if (position >= stos.Count - 1) return false;
                position++;
                return true;
            }

            public void Reset() => position = -1;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return this[i];
            }
        }

        public IEnumerable<T> TopToBottom
        {
            get
            {
                for (int i = Count - 1; i >= 0; i--)
                {
                    yield return this[i];
                }
            }
        }
        public System.Collections.ObjectModel.ReadOnlyCollection<T> ToArrayReadOnly()
        {
            return Array.AsReadOnly(tab);
        }
    }
}