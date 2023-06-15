using Unity.NetCode;
using UnityEngine.Scripting;

[Preserve]
public class Bootstrap : ClientServerBootstrap
{
    public override bool Initialize(string defaultWorldName)
    {
        AutoConnectPort = 0;
        CreateLocalWorld(defaultWorldName);
        return true;
    }
}
