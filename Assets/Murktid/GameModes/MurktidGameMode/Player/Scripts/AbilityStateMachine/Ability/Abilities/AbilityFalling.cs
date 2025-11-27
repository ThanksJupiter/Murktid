using Murktid;
using UnityEngine;

public class AbilityFalling : PlayerAbility {
    protected override void Setup() {
        AddTag(AbilityTags.falling);
    }

    public override bool ShouldActivate() {
        if(Context.IsGrounded) {
            return false;
        }

        return true;
    }

    public override bool ShouldDeactivate() {
        if(!Context.IsGrounded) {
            return false;
        }

        return true;
    }

    public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime) {
        currentVelocity += Context.settings.gravity * deltaTime;
    }
}
