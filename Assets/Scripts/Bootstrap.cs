using Unity.NetCode;
using UnityEngine;
using UnityEngine.Scripting;

[Preserve]
public class Bootstrap : ClientServerBootstrap
{
    public override bool Initialize(string defaultWorldName)
    {
        AutoConnectPort = 7979;
        Debug.Log("Bootstrap Initialize");
        return base.Initialize(defaultWorldName);
    }
}
