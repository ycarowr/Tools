using System;
using System.Collections.Generic;

namespace YWR.Tools
{
    public interface ISubject
    {
    }

    public interface IListener
    {
    }

    public class ObserverMB<T> : SingletonMB<ObserverMB<T>>
    {
        private readonly Dictionary<Type, List<IListener>> register = new Dictionary<Type, List<IListener>>();

        public virtual void AddListener(IListener listener)
        {
            if (listener == null)
            {
                throw new ArgumentNullException("Can't register Null as a Listener");
            }

            Type type = listener.GetType();
            Type[] interfaces = type.GetInterfaces();
            for (int i = 0; i < interfaces.Length; i++)
            {
                Type subject = interfaces[i];

                //TODO: ISubject and mid level interfaces are also added to the register
                bool isAssignableFrom = typeof(ISubject).IsAssignableFrom(subject);
                if (isAssignableFrom)
                {
                    CreateAndAdd(subject, listener);
                }
            }
        }

        public virtual void RemoveListener(IListener listener)
        {
            foreach (KeyValuePair<Type, List<IListener>> pair in register)
            {
                pair.Value.Remove(listener);
            }
        }

        public void RemoveSubject(Type subject)
        {
            if (register.ContainsKey(subject))
            {
                register.Remove(subject);
            }
        }

        public void Notify<T1>(Action<T1> subject) where T1 : class
        {
            Type subjectType = typeof(T1);
            bool isSubject = register.ContainsKey(subjectType);
            if (!isSubject)
            {
                return;
            }

            List<IListener> listeners = register[subjectType];
            if (listeners.Count == 0)
            {
                return;
            }

            for (int i = 0; i < listeners.Count; i++)
            {
                IListener obj = listeners[i];
                if (obj != null)
                {
                    subject(obj as T1);
                }
            }
        }

        protected void CreateAndAdd(Type subject, IListener listener)
        {
            if (register.ContainsKey(subject))
            {
                register[subject].Add(listener);
            }
            else
            {
                register.Add(subject, new List<IListener> {listener});
            }
        }
    }
}