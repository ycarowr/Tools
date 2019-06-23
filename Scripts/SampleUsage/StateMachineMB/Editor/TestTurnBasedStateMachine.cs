using ExampleStateMachine;
using UnityEditor;
using UnityEngine;

namespace SimpleTurnBasedGame
{
    [CustomEditor(typeof(TurnBasedStateMachine))]
    public class TestTurnBasedStateMachine : Editor
    {
        private TurnBasedStateMachine Target => target as TurnBasedStateMachine;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!Application.isPlaying)
                return;
            if (GUILayout.Button("Push: " + typeof(PlayerStateTurn)))
                Target.PushState<PlayerStateTurn>();

            if (GUILayout.Button("Push: " + typeof(AiStateTurn)))
                Target.PushState<AiStateTurn>();

            var current = Target.PeekState();

            if (current == null)
                return;

            if (GUILayout.Button("Pop: " + current.GetType()))
                Target.PopState();
        }
    }
}