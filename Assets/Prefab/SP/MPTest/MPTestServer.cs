using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPTestServer : NetworkBehaviour
{
    public override void OnStartServer()
    {
        Debug.Log("OnStartServer!");
    }
    public override void OnStartClient()
    {
        Debug.Log("OnStartClient");
    }
}
