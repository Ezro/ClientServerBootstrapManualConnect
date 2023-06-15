using Unity.Entities;
using Unity.NetCode;
using Unity.Networking.Transport;
using UnityEngine;

public class Frontend : MonoBehaviour
{
    const ushort PORT = 7978;

    public void StartHost()
    {
        var server = ClientServerBootstrap.CreateServerWorld("ServerWorld");
        var client = ClientServerBootstrap.CreateClientWorld("ClientWorld");

        //Destroy the local simulation world to avoid the game scene to be loaded into it
        //This prevent rendering (rendering from multiple world with presentation is not greatly supported)
        //and other issues.
        DestroyLocalSimulationWorld();
        if (World.DefaultGameObjectInjectionWorld == null)
            World.DefaultGameObjectInjectionWorld = server;

        NetworkEndpoint ep = NetworkEndpoint.AnyIpv4.WithPort(PORT);
        {
            using var drvQuery = server.EntityManager.CreateEntityQuery(ComponentType.ReadWrite<NetworkStreamDriver>());
            drvQuery.GetSingletonRW<NetworkStreamDriver>().ValueRW.Listen(ep);
        }

        ep = NetworkEndpoint.LoopbackIpv4.WithPort(PORT);
        {
            using var drvQuery = client.EntityManager.CreateEntityQuery(ComponentType.ReadWrite<NetworkStreamDriver>());
            drvQuery.GetSingletonRW<NetworkStreamDriver>().ValueRW.Connect(client.EntityManager, ep);
        }

    }

    void DestroyLocalSimulationWorld()
    {
        foreach (World world in World.All)
        {
            if (world.Flags == WorldFlags.Game)
            {
                world.Dispose();
                break;
            }
        }
    }
}

