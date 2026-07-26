using UnityEngine;

public class DeathZoneBoundaries : CameraBoundaries
{
    private const float extendFactor = 2.2f;

    protected override void Awake()
    {
        camera = transform.GetComponentInParent<Camera>();
        extend = new(extendFactor, extendFactor);
    }
}
