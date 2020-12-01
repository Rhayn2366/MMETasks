using Unity.Entities;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Transforms;

public class SpawnCollectableSystem : SystemBase
{
    BeginInitializationEntityCommandBufferSystem _entityCommandBufferSystem;

    protected override void OnCreate()
    {
        base.OnCreate();
        _entityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var seed = (uint)UnityEngine.Random.Range(1, 99999);
        var random = new Random(seed);
        float y = 1f;
        var commandBuffer = _entityCommandBufferSystem.CreateCommandBuffer().AsParallelWriter();
        Entities.WithBurst(FloatMode.Default, FloatPrecision.Standard, true)
            .ForEach((Entity entity, int entityInQueryIndex, in SpawnCollectableComponent spawnCollectableComponent) 
            => { 
                for(var i = 0; i < spawnCollectableComponent.collectableCount; i++)
                {
                    Entity entityInstance = commandBuffer.Instantiate(entityInQueryIndex, spawnCollectableComponent.collectablePrefab);

                    float x = random.NextFloat(-5, 5);
                    float z = random.NextFloat(-5, 5);

                    float3 position = new float3(x, y, z);
                    commandBuffer.SetComponent(entityInQueryIndex, entityInstance, new Translation { Value = position });
                }
                commandBuffer.DestroyEntity(entityInQueryIndex, entity);
            }).ScheduleParallel();
        _entityCommandBufferSystem.AddJobHandleForProducer(Dependency);
    }
}
