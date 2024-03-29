﻿using System.Collections.Generic;
using UnityEngine;

namespace YWR.Tools.StateMachineSO
{
    public class StateMachine : ScriptableObject
    {
        /// <summary> Necessary to track the history. </summary>
        private readonly Stack<IState> _stack = new Stack<IState>();

        /// <summary> The state on the top of the stack. </summary>
        public IState Current => _stack.Count < 1 ? null : _stack.Peek();

        public int Count => _stack.Count;

        private void OnDisable()
        {
            Clear();
        }

        private void OnDestroy()
        {
            Clear();
        }

        /// <summary> Return whether a state is the current one or not. </summary>
        public bool IsCurrent(IState state)
        {
            return Current == state;
        }

        public void Update()
        {
            Current?.Update();
        }

        /// <summary> Push a state to the current. </summary>
        public void PushState(IState state, bool isSilent = false)
        {
            if (_stack.Count > 0 && !isSilent)
            {
                Current?.Exit();
            }

            _stack.Push(state);
            state.Enter();
        }

        /// <summary> Remove the current state and move to the previous one. </summary>
        public IState PopState(bool isSilent = false)
        {
            if (Current == null)
            {
                return null;
            }

            IState state = _stack.Pop();
            state.Exit();

            if (!isSilent)
            {
                Current?.Enter();
            }

            return state;
        }

        /// <summary> Clear the registry. </summary>
        public void Clear()
        {
            if (_stack.Count < 1)
            {
                return;
            }

            foreach (IState state in _stack)
            {
                state.Clear();
            }

            _stack.Clear();
        }
    }
}