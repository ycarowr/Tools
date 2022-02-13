using System;

namespace YWR.Tools
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