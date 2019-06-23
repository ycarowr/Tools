using Tools;
using UnityEngine;

public class TrailUsage : MonoBehaviour
{
    private void Start()
    {
        var renderer = GetComponent<SpriteRenderer>();
        var trail = GetComponentInChildren<TrailParticles>();
        trail.PlayFromRender(renderer);
    }
}