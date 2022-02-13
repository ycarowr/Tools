using System;

namespace YWR.Tools
{
    public partial class GenericPooler<T>
    {
        public class GenericPoolerArgumentException : ArgumentException
        {
            public GenericPoolerArgumentException(string message) : base(message)
            {
            }
        }
    }
}