using Unity.Entities;
using Unity.Entities.Serialization;

public struct EntityPrefabReferenceComponent : IComponentData
{
    public EntityPrefabReference Value;
}