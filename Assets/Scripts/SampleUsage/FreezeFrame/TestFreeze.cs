using Tools;
using UnityEngine;

public class TestFreeze : MonoBehaviour
{
    public FreezeFrame freezeer;
    public float Time { get; set; }
    public float Delay { get; set; }

    public void Freeze()
    {
        freezeer.Freeze(Time, Delay);
    }

    public void Unfreeze()
    {
        freezeer.Unfreeze();
    }

    public void SetTime(string time)
    {
        float t;
        float.TryParse(time, out t);
        Time = t;
    }

    public void SetDelay(string delay)
    {
        float t;
        float.TryParse(delay, out t);
        Delay = t;
    }
}