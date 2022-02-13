using System;

namespace YWR.Tools
{
    public partial class Collection<T>
    {
        public class CollectionArgumentException : ArgumentException
        {
            public CollectionArgumentException(string message) : base(message)
            {
            }
        }
    }
}