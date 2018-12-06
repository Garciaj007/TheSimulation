using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSenseController : SenseController {

    private void OnTriggerEnter(Collider other)
    {
        Identifier ident = other.GetComponent<Identifier>();
        if(ident != null)
        {
            if(ident.identity != identity)
            {
                print("Enemy Touch Detected");
            }
        }
    }
}
