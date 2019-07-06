using System;

namespace Tools.UI.Fade
{
    public interface IFade
    {
        Action OnFinishFade { get; set; }
        bool IsFading { get; }
        float Alpha { get; }
        void SetAlphaImmediatly(float alpha);
        void SetAlpha(float alpha, float speed);
    }
}