using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tools.Patterns.Observer
{
    public class ObserverAtt<T> : ScriptableObject where T : Attribute
    {
        readonly Dictionary<Type, List<IListener>> register = new Dictionary<Type, List<IListener>>();

        public virtual void AddListener(IListener listener)
        {
            if (listener == null)
                throw new ArgumentNullException("Can't register Null as a Listener");

            var type = listener.GetType();
            Debug.Log("Type: "+ type);
            var interfaces = type.GetInterfaces();
            
            foreach (var i in interfaces)
            {
                var customAttributes = i.GetCustomAttributes(true);    
                for (var att = 0; att < customAttributes.Length; att++)
                {
                    var subject = customAttributes[att];               
                    Debug.Log(subject);
                    if(subject is T)
                        CreateAndAdd(i, listener);
                }    
            }
        }

        public virtual void RemoveListener(IListener listener)
        {
            foreach (var pair in register)
                pair.Value.Remove(listener);
        }

        public virtual void RemoveSubject(Type subject)
        {
            if (register.ContainsKey(subject))
                register.Remove(subject);
        }

        public void Notify<T1>(Action<T1> subject) where T1 : class
        {
            var subjectType = typeof(T1);
            var isSubject = register.ContainsKey(subjectType);
            if (!isSubject)
                return;
            var listeners = register[subjectType];
            if (listeners.Count == 0) return;

            for (var i = 0; i < listeners.Count; i++)
            {
                var obj = listeners[i];
                if (obj != null)
                    subject(obj as T1);
            }
        }

        protected void CreateAndAdd(Type subject, IListener listener)
        {
            Debug.Log("Create and add: "+ subject);
            if (register.ContainsKey(subject))
                register[subject].Add(listener);
            else
                register.Add(subject, new List<IListener> {listener});
        }
    }
}