using System;
using System.Collections.Generic;

namespace YWR.Tools
{
    public abstract class BaseStateMachine
    {
        /// <summary> All registered states. </summary>
        private readonly Dictionary<Type, IState> m_Register = new Dictionary<Type, IState>();

        /// <summary> History of states the entity has passed. </summary>
        private readonly Stack<IState> m_Stack = new Stack<IState>();

        /// <summary> Boolean that indicates whether the FSM has been initialized or not. </summary>
        public bool IsInitialized { get; protected set; }

        /// <summary> Returns the state on the top of the stack. Can be Null. </summary>
        public IState Current => PeekState();

        /// <summary> Register a state into the state machine. </summary>
        public void RegisterState(IState state)
        {
            if (state == null)
            {
                throw new ArgumentNullException("Null is not a valid state");
            }

            Type type = state.GetType();
            m_Register.Add(type, state);
        }

        /// <summary> Initialize all states. All states have to be registered before the initialization. </summary>
        public void Initialize()
        {
            OnBeforeInitialize();
            foreach (IState state in m_Register.Values)
            {
                state.OnInitialize();
            }

            IsInitialized = true;
            OnInitialize();
        }

        /// <summary> Do something before the initialization. </summary>
        protected virtual void OnBeforeInitialize()
        {
        }

        /// <summary> Do something after the initialization. </summary>
        protected virtual void OnInitialize()
        {
        }

        /// <summary> Update the state on the top of the stack. </summary>
        public void Update()
        {
            Current?.OnUpdate();
        }

        /// <summary> Checks if a state is the current state. </summary>
        public bool IsCurrent<T>() where T : IState
        {
            return Current?.GetType() == typeof(T);
        }

        /// <summary> Checks if a state is the current state. </summary>
        public bool IsCurrent(IState state)
        {
            if (state == null)
            {
                throw new ArgumentNullException();
            }

            return Current?.GetType() == state.GetType();
        }

        /// <summary> Pushes a state </summary>
        public void PushState<T>(bool isSilent = false) where T : IState
        {
            Type stateType = typeof(T);
            IState state = m_Register[stateType];
            PushState(state, isSilent);
        }

        /// <summary> Pushes a state </summary>
        public void PushState(IState state, bool isSilent = false)
        {
            Type type = state.GetType();
            if (!m_Register.ContainsKey(type))
            {
                throw new ArgumentException("State " + state + " not registered yet.");
            }

            if (m_Stack.Count > 0 && !isSilent)
            {
                Current?.OnExitState();
            }

            m_Stack.Push(state);
            state.OnEnterState();
        }

        /// <summary> Peeks a state from the stack. A peek returns null if the stack is empty. It doesn't trigger any call. </summary>
        public IState PeekState()
        {
            return m_Stack.Count > 0 ? m_Stack.Peek() : null;
        }

        /// <summary> Pops a state from the stack. </summary>
        public IState PopState(bool isSilent = false)
        {
            if (Current == null)
            {
                return null;
            }

            IState state = m_Stack.Pop();
            state.OnExitState();

            if (!isSilent)
            {
                Current?.OnEnterState();
            }

            return state;
        }

        /// <summary> Clears and restart the states register. </summary>
        public virtual void Clear()
        {
            foreach (IState state in m_Register.Values)
            {
                state.OnClear();
            }

            m_Stack.Clear();
            m_Register.Clear();
        }

        public void FixedUpdate()
        {
            Current?.OnFixedUpdate();
        }
    }
}