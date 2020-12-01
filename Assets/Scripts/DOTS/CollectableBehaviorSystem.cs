using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class CollectableBehaviorSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;
        float rotationInDegrees = 15f;
        Entities.WithAll<CollectedComponent, Rotation>().ForEach((ref Rotation rotation) => 
        { 
        rotation.Value = math.mul(rotation.Value, quaternion.RotateY(math.radians(rotationInDegrees * deltaTime))); 
        }).Schedule();
    }
}