using UnityEngine;

namespace YWR.Tools
{
    public class ShakeAnimation : MonoBehaviour
    {
        [Tooltip("How big are the width and height of the shake.")] [SerializeField]
        private float amplitude = 0.15f;

        [Tooltip("Duration of the shake in seconds")]
        public float duration = 0.5f;

        [Tooltip("How often the shake happens during its own duration. Value has to be smaller than the duration.")]
        [SerializeField]
        private float frequency = 0.01f;

        private Vector3 InitialPosition { get; set; }
        private Transform CachedTransform { get; set; }
        private bool IsShaking { get; set; }
        private float CounterFrequency { get; set; }
        private float CounterDuration { get; set; }

        private void Awake()
        {
            CachedTransform = transform;
        }

        private void Update()
        {
            if (!IsShaking)
            {
                return;
            }

            UpdateShake();
        }

        /// <summary> Method which starts the shake movement. </summary>
        [Button]
        public void Shake()
        {
            if (IsShaking)
            {
                return;
            }

            InitialPosition = CachedTransform.position;
            IsShaking = true;
        }

        /// <summary> Clear the shake instantly. </summary>
        [Button]
        public void Stop()
        {
            IsShaking = false;
            CachedTransform.position = InitialPosition;
            ResetCounters();
        }

        private void ResetCounters()
        {
            CounterDuration = 0;
            CounterFrequency = 0;
        }

        private void UpdateShake()
        {
            float deltaTime = Time.deltaTime;
            CounterDuration += deltaTime;
            if (CounterDuration >= duration)
            {
                Stop();
            }
            else
            {
                if (CounterFrequency < frequency)
                {
                    CounterFrequency += deltaTime;
                }
                else
                {
                    CachedTransform.position = InitialPosition + Random.insideUnitSphere * amplitude;
                    CounterFrequency = 0;
                }
            }
        }
    }
}