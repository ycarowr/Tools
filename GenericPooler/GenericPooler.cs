using System.Collections.Generic;

namespace YWR.Tools
{
    public partial class GenericPooler<T> where T : class, IPoolableObject, new()
    {
        private readonly List<T> m_Busy = new List<T>();
        private readonly List<T> m_Free = new List<T>();

        public GenericPooler(int startingSize)
        {
            StartSize = startingSize;
            for (int i = 0; i < StartSize; ++i)
            {
                T obj = new T();
                m_Free.Add(obj);
            }
        }

        public int StartSize { get; }
        public int SizeFreeObjects => m_Free.Count;
        public int SizeBusyObjects => m_Busy.Count;

        /// <summary> Get an object </summary>
        public T Get()
        {
            T pooled = null;

            if (SizeFreeObjects > 0)
            {
                pooled = m_Free[SizeFreeObjects - 1];
                m_Free.Remove(pooled);
            }
            else
            {
                pooled = new T();
            }

            m_Busy.Add(pooled);
            return pooled;
        }

        /// <summary> Release an object of the type T </summary>
        public void Release(T released)
        {
            if (released == null)
            {
                throw new GenericPoolerArgumentException("Can't Release a null object");
            }

            released.Restart();
            m_Free.Add(released);
            m_Busy.Remove(released);
        }
    }
}