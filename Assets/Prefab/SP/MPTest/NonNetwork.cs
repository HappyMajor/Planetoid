using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonNetwork : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Is Server: " + isServer);
        Debug.Log("Is Client: " + isClient);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
