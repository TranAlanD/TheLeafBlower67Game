using UnityEngine;

public class SlowStatus : IStatusEffect
{
    private readonly float factor;   // e.g., 0.5f means 50% speed
    private readonly float duration; // seconds
    private float t;

    public SlowStatus(float factor, float duration)
    {
        this.factor = Mathf.Clamp(factor, 0f, 1f);
        this.duration = Mathf.Max(0f, duration);
    }

    public bool Tick(GameObject host, float dt)
    {
        t += dt;

        // Example hookup: a field the player controller reads each frame.
        var pm = host.GetComponent<PlayerMovement>();
        if (pm != null)
        {
            // Keep the smallest multiplier when multiple slows stack
            pm.externalSpeedMultiplier = Mathf.Min(pm.externalSpeedMultiplier, factor);
        }

        return t < duration;
    }
}
