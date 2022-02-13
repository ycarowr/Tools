using UnityEngine;

namespace YWR.Tools
{
    /// <summary>
    ///     Handler for the FSM. Usually the class which holds the FSM.
    /// </summary>
    public interface IStateMachineHandler
    {
        MonoBehaviour MonoBehaviour { get; }
    }
}