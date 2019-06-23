using System.Collections;
using UnityEngine;

namespace Tools
{
    public class FreezeFrame : MonoBehaviour
    {
        [SerializeField] private float delay;

        [SerializeField] [Tooltip("Target of the fixed framerate.")]
        private uint fixedFrameRate = 60;

        [SerializeField] [Tooltip("Fix the framerate when the game starts.")]
        private bool fixFrameRate = true;

        [SerializeField] private int frozenCount;
        [SerializeField] private float initialTimeScale;

        [SerializeField] [Tooltip("Whether the game is frozen or not.")]
        private bool isFrozen;

        //------------------------------------------------------------------------------------------------------

        [Header("Test")] [SerializeField] private float time;

        [SerializeField] [Tooltip("Duration in frames of the freeze.")]
        private float totalFramesFrozen;

        private void Start()
        {
            if (fixFrameRate)
                Application.targetFrameRate = (int) fixedFrameRate;
        }

        //------------------------------------------------------------------------------------------------------

        /// <summary>
        ///     Freeze the TimeScale for an amount of seconds.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="delay"></param>
        public void Freeze(float time, float delay)
        {
            if (isFrozen)
                return;

            totalFramesFrozen = time * Application.targetFrameRate;
            initialTimeScale = Time.timeScale;

            if (delay == 0)
                Freeze();
            else
                StartCoroutine(FreezeRoutine(delay));
        }

        /// <summary>
        ///     Unfreeze the time scale.
        /// </summary>
        [Button]
        public void Unfreeze()
        {
            frozenCount = 0;
            Time.timeScale = initialTimeScale;
            isFrozen = false;
        }


        private void Update()
        {
            if (!isFrozen)
                return;

            frozenCount++;

            if (frozenCount >= totalFramesFrozen)
                Unfreeze();
        }

        private IEnumerator FreezeRoutine(float delay)
        {
            yield return new WaitForSeconds(delay);
            Freeze();
        }

        private void Freeze()
        {
            initialTimeScale = Time.timeScale;
            Time.timeScale = 0;
            isFrozen = true;
        }

        [Button]
        private void TestFreeze()
        {
            Freeze(time, delay);
        }
    }
}