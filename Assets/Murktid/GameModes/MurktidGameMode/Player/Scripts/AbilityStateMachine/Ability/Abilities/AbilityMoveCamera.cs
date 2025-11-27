using Murktid;
using UnityEngine;

public class AbilityMoveCamera : PlayerAbility
{
    public override bool ShouldActivate()
    {
        return true;
    }

    public override bool ShouldDeactivate()
    {
        return false;
    }

    protected override void Tick(float deltaTime)
    {
        Context.cameraReference.transform.position = Vector3.Lerp(Context.cameraReference.transform.position,
            Context.transform.position + Vector3.up * Context.settings.cameraHeight,
            1f - Mathf.Exp(-Context.settings.cameraFollowSharpness * deltaTime));
    }
}
