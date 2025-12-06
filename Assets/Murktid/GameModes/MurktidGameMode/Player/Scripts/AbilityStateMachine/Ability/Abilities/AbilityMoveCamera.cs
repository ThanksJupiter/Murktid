using Murktid;
using UnityEngine;

public class AbilityMoveCamera : PlayerAbility {
    private float cameraHeight;

    public override bool ShouldActivate()
    {
        return true;
    }

    public override bool ShouldDeactivate()
    {
        return false;
    }

    protected override void OnActivate() {
        cameraHeight = Context.TargetCameraHeight;
    }

    protected override void Tick(float deltaTime)
    {
        cameraHeight = Mathf.Lerp(cameraHeight, Context.TargetCameraHeight, 1f - Mathf.Exp(-Context.settings.cameraHeightLerpSpeed * deltaTime));

        Context.cameraReference.transform.position = Vector3.Lerp(Context.cameraReference.transform.position,
            Context.transform.position + Vector3.up * cameraHeight,
            1f - Mathf.Exp(-Context.settings.cameraFollowSharpness * deltaTime));
    }
}
