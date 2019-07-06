using System;
using Patterns;
using UnityEngine;

namespace Tools.UI.Fade
{
    public class FadeComponent : MonoBehaviour, IFade
    {
        private const float Threshold = 0.01f;
        public SpriteRenderer Renderer;
        [Range(0.1f, 4f)] public float speed;
        public bool DisableOnAwake;
        private Color target;
        private Color Current => Renderer.color;
        public bool IsFading { get; set; }
        public Action OnFinishFade { get; set; } = () => { };
        public float Alpha => Renderer.color.a;

        //------------------------------------------------------------------------------------------------------------------------------

        private void Awake()
        {
            if(DisableOnAwake)
                Disable();
        }

        private void Update()
        {
            if (!IsFading)
                return;

            var delta = Mathf.Abs(Current.a - target.a);

            if (delta < Threshold)
            {
                IsFading = false;
                Renderer.color = target;
                OnFinishFade?.Invoke();
                if (Current.a <= 0)
                    Disable();
            }
            else
            {
                Renderer.color = Color.Lerp(Current, target, speed * Time.deltaTime);
            }
        }
        
        //------------------------------------------------------------------------------------------------------------------------------

        public void SetAlphaImmediatly(float a)
        {
            Enable();
            var color = Current;
            color.a = a;
            Renderer.color = color;
            if (Current.a <= 0)
                Disable();
        }

        public void SetAlpha(float a, float speed)
        {
            Enable();
            this.speed = speed;
            target.a = a;
            IsFading = true;
        }

        public void SetAlpha(float a)
        {
            SetAlpha(a, speed);
        }

        public void Disable()
        {
            Renderer.enabled = false;
        }

        public void Enable()
        {
            target = Current;
            Renderer.enabled = true;
        }

        //------------------------------------------------------------------------------------------------------------------------------

        [Button]
        public void FadeTo1()
        {
            SetAlpha(1f, speed);
        }

        [Button]
        public void FadeTo0()
        {
            SetAlpha(0, speed);
        }
    }
}