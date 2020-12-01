using Unity.Entities;

[GenerateAuthoringComponent]
public struct SpawnCollectableComponent : IComponentData
{
    public int collectableCount;
    public Entity collectablePrefab;
}
