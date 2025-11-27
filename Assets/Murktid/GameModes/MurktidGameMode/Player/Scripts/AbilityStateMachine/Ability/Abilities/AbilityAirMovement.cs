using Murktid;
using UnityEngine;

public class AbilityAirMovement : PlayerAbility {
    protected override void Setup() {
        AddTag(AbilityTags.movement);
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

    protected override void Tick(float deltaTime) {
        Vector3 moveInputVector = new(Context.input.Move.value.x, 0f, Context.input.Move.value.y);

        Vector3 cameraPlanarDirection = Vector3.ProjectOnPlane(Context.cameraReference.transform.rotation * Vector3.forward, Context.CharacterUp).normalized;

        if(cameraPlanarDirection.sqrMagnitude == 0f) {
            cameraPlanarDirection = Vector3.ProjectOnPlane(Context.cameraReference.transform.rotation * Vector3.up, Context.CharacterUp).normalized;
        }

        Quaternion cameraPlanarRotation = Quaternion.LookRotation(cameraPlanarDirection, Context.CharacterUp);

        Context.MoveInputVector = cameraPlanarRotation * moveInputVector;
    }

    public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime) {
        if(Context.MoveInputVector.sqrMagnitude > 0f) {
            Vector3 addedVelocity = Context.MoveInputVector * (Context.settings.airAccelerationSpeed * deltaTime);

            Vector3 currentVelocityOnInputsPlane = Vector3.ProjectOnPlane(currentVelocity, Context.CharacterUp);

            // Limit air velocity from inputs
            if(currentVelocityOnInputsPlane.magnitude < Context.settings.maxAirMoveSpeed) {
                // clamp addedVel to make total vel not exceed max vel on inputs plane
                Vector3 newTotal = Vector3.ClampMagnitude(currentVelocityOnInputsPlane + addedVelocity, Context.settings.maxAirMoveSpeed);
                addedVelocity = newTotal - currentVelocityOnInputsPlane;
            }
            else {
                // Make sure added vel doesn't go in the direction of the already-exceeding velocity
                if(Vector3.Dot(currentVelocityOnInputsPlane, addedVelocity) > 0f) {
                    addedVelocity = Vector3.ProjectOnPlane(addedVelocity, currentVelocityOnInputsPlane.normalized);
                }
            }

            // Prevent air-climbing sloped walls
            if(Context.FoundAnyGround) {
                if(Vector3.Dot(currentVelocity + addedVelocity, addedVelocity) > 0f) {
                    Vector3 perpenticularObstructionNormal = Vector3.Cross(Vector3.Cross(Context.CharacterUp, Context.GroundNormal), Context.CharacterUp).normalized;
                    addedVelocity = Vector3.ProjectOnPlane(addedVelocity, perpenticularObstructionNormal);
                }
            }

            // Apply added velocity
            currentVelocity += addedVelocity;
        }

        // Gravity
        //currentVelocity += Gravity * deltaTime;

        // Drag
        currentVelocity *= (1f / (1f + (Context.settings.drag * deltaTime)));
    }
}
