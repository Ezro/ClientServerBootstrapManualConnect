using Unity.Entities;
using UnityEngine;

[DisallowMultipleComponent]
public class NetCapsuleSpawnerAuthoring : MonoBehaviour
{
    public GameObject Capsule;

    class Baker : Baker<NetCapsuleSpawnerAuthoring>
    {
        public override void Bake(NetCapsuleSpawnerAuthoring authoring)
        {
            NetCapsuleSpawner playerSpawner = default;
            playerSpawner.Capsule = GetEntity(authoring.Capsule, TransformUsageFlags.Dynamic);
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, playerSpawner);
        }
    }
}
