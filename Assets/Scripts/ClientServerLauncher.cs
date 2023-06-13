using Unity.Entities;
using Unity.NetCode;
using Unity.Networking.Transport;
using UnityEngine;

public class ClientServerLauncher : MonoBehaviour
{
    const ushort PORT = 7979;

    public void StartHost()
    {
        StartServer();
        StartClient();
    }

    public void StartServer()
    {
        ClientServerBootstrap.DefaultListenAddress.Port = PORT;
        //ClientServerBootstrap.AutoConnectPort = PORT;
        Debug.Log($"Listening on port {PORT}");
        World serverWorld = ClientServerBootstrap.CreateServerWorld("ServerWorld");
        NetworkEndpoint ep = NetworkEndpoint.AnyIpv4.WithPort(PORT);
        {
            using var drvQuery = serverWorld.EntityManager.CreateEntityQuery(ComponentType.ReadWrite<NetworkStreamDriver>());
            drvQuery.GetSingletonRW<NetworkStreamDriver>().ValueRW.Listen(ep);
        }
    }

    public void StartClient()
    {
        ClientServerBootstrap.AutoConnectPort = 0;
        World clientWorld = ClientServerBootstrap.CreateClientWorld("ClientWorld");
        NetworkEndpoint ep = NetworkEndpoint.LoopbackIpv4.WithPort(PORT);
        var drvQuery = clientWorld.EntityManager.CreateEntityQuery(ComponentType.ReadWrite<NetworkStreamDriver>());
        drvQuery.GetSingletonRW<NetworkStreamDriver>().ValueRW.Connect(clientWorld.EntityManager, ep);
        Debug.Log("Connected.");
    }
}

