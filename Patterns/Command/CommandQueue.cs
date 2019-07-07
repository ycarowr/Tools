﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Patterns
{
    /// <summary>
    ///     Realization of a command queue.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="T1"></typeparam>
    public class CommandQueue<T, T1> : SingletonMB<T>, ICommandQueue<T1> 
        where T : MonoBehaviour 
        where T1 : Command
    {
        protected Queue<T1> Commands { get; set; } = new Queue<T1>();
        public bool IsEmpty => Size == 0;
        public int Size => Commands.Count;
        public Action OnEmpty { get; set; } = () => { };
        
        //--------------------------------------------------------------------------------------------------------------
        
        public virtual void Enqueue(T1 command)
        {
            if (command == null)
                return;
            
            Commands?.Enqueue(command);
        }

        public virtual T1 Dequeue()
        {
            if (IsEmpty)
                return null;

            var command = Commands.Dequeue();
            command?.Execute();
            
            if (IsEmpty)
                OnEmptyQueue();
            return command;
        }

        protected virtual void OnEmptyQueue()
        {
            OnEmpty?.Invoke();
            // override to do something else.
        }
    }
}
