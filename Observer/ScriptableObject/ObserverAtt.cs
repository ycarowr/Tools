using System;
using System.Collections.Generic;
using UnityEngine;

namespace YWR.Tools
{
    public interface IDispatcher
    {
        void Notify<T1>(Action<T1> subject) where T1 : class;
        void AddListener(IListener listener);
        void RemoveListener(IListener listener);
        void RemoveSubject(Type subject);
    }

    public class ObserverAtt<T> : ScriptableObject, IDispatcher where T : Attribute
    {
        private readonly Dictionary<Type, List<IListener>> _register = new Dictionary<Type, List<IListener>>();

        public void AddListener(IListener listener)
        {
            if (listener == null)
            {
                throw new ArgumentNullException("Can't register Null as a Listener");
            }

            Type type = listener.GetType();
            Type[] interfaces = type.GetInterfaces();

            foreach (Type i in interfaces)
            {
                object[] customAttributes = i.GetCustomAttributes(true);
                foreach (object subject in customAttributes)
                {
                    if (subject is T)
                    {
                        CreateAndAdd(i, listener);
                    }
                }
            }
        }

        public void RemoveListener(IListener listener)
        {
            foreach (KeyValuePair<Type, List<IListener>> pair in _register)
            {
                pair.Value.Remove(listener);
            }
        }

        public void RemoveSubject(Type subject)
        {
            if (_register.ContainsKey(subject))
            {
                _register.Remove(subject);
            }
        }

        public void Notify<T1>(Action<T1> subject) where T1 : class
        {
            Type subjectType = typeof(T1);
            bool isSubject = _register.ContainsKey(subjectType);
            if (!isSubject)
            {
                return;
            }

            List<IListener> listeners = _register[subjectType];
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

        private void CreateAndAdd(Type subject, IListener listener)
        {
            if (_register.ContainsKey(subject))
            {
                _register[subject].Add(listener);
            }
            else
            {
                _register.Add(subject, new List<IListener> {listener});
            }
        }
    }
}