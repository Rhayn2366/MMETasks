using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class PlayerBehaviorSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float elapsedTime = (float)Time.ElapsedTime;
        float deltaTime = Time.DeltaTime;

        var horizontalMove = Input.GetAxis("Horizontal");
        var verticalMove = Input.GetAxis("Vertical");

        Entities.ForEach((ref PlayerComponent player, ref Translation trans, ref Rotation rotation)
        =>
        {
            player.RotationAngle += horizontalMove * deltaTime;
            float3 targetDirection = new float3(math.sin(player.RotationAngle), 0, math.cos(player.RotationAngle));
            rotation.Value =  quaternion.LookRotationSafe(targetDirection, Vector3.up);
            trans.Value += targetDirection * player.Speed * verticalMove * elapsedTime * deltaTime;
        }).ScheduleParallel();
    }
}