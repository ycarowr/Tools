using System;
using UnityEngine;

namespace YWR.Tools
{
    public class Fade : SingletonMB<Fade>, IFade
    {
        private const float Threshold = 0.01f;
        public SpriteRenderer Renderer;
        [Range(1, 100f)] public float Speed;
        private Color Target;
        private Color Current => Renderer.color;

        private void Update()
        {
            if (!IsFading)
            {
                return;
            }

            float delta = Mathf.Abs(Current.a - Target.a);

            if (delta < Threshold)
            {
                IsFading = false;
                Renderer.color = Target;
                OnFinishFade?.Invoke();
                if (Current.a <= 0)
                {
                    Disable();
                }
            }
            else
            {
                Renderer.color = Color.Lerp(Current, Target, Speed * Time.deltaTime);
            }
        }

        public bool IsFading { get; set; }

        public Action OnFinishFade { get; set; } = () => { };
        public float Alpha => Renderer.color.a;

        //------------------------------------------------------------------------------------------------------------------------------

        public void SetAlphaImmediatly(float a)
        {
            Enable();
            Color color = Current;
            color.a = a;
            Renderer.color = color;
            if (Current.a <= 0)
            {
                Disable();
            }
        }

        public void SetAlpha(float a, float speed)
        {
            Enable();
            Speed = speed;
            Target.a = a;
            IsFading = true;
        }

        //------------------------------------------------------------------------------------------------------------------------------

        protected override void OnAwake()
        {
            Disable();
        }


        public void Disable()
        {
            Renderer.enabled = false;
        }

        public void Enable()
        {
            Target = Current;
            Renderer.enabled = true;
        }

        //------------------------------------------------------------------------------------------------------------------------------

        [Button]
        public void FadeTo1()
        {
            SetAlpha(0.8f, Speed);
        }

        [Button]
        public void FadeTo0()
        {
            SetAlpha(0, Speed);
        }
    }
}