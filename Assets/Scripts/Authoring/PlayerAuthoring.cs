using Unity.Entities;
using Unity.Entities.Serialization;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerAuthoring : MonoBehaviour
{
    public GameObject PresentationPrefab;

    class Baker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            Player component = default;
            AddComponent(entity, component);
            EntityPrefabReference entityPrefab = new EntityPrefabReference(authoring.PresentationPrefab);
            AddComponent(entity, new EntityPrefabReferenceComponent { Value = entityPrefab });
        }
    }
}
