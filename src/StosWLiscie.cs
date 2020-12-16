namespace Stos
{
    public class StosWLiscie<T> : IStos<T>
    {
        private class Wezel
        {
            public T dane;
            public Wezel nastepnik;

            public Wezel(T e, Wezel nastepnik)
            {
                this.dane = e;
                this.nastepnik = nastepnik;
            }
        }

        private Wezel szczyt;

        public StosWLiscie()
        {
            szczyt = null;
        }
        public void Push(T e)
        {
            szczyt = new Wezel(e, szczyt);
        }

        public T Peek => IsEmpty ? throw new StosEmptyException() : szczyt.dane;

        public T Pop()
        {
            if(IsEmpty) throw new StosEmptyException();
            szczyt = szczyt.nastepnik;
            return szczyt.dane;
        }

        public int Count { get; }
        public bool IsEmpty => szczyt == null;
        public void Clear() => szczyt = null;

        public void TrimExcess()
        {
        }

        public T this[int index] => throw new System.NotImplementedException();

        public T[] ToArray()
        {
            var tab = new T[Count];
            for (int i = 0; i < Count; i++)
            {
                tab[i] = szczyt.dane;
                szczyt = szczyt.nastepnik;
            }
            return tab;
        }
    }
}