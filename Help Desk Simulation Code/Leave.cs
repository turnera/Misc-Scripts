using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leave : MonoBehaviour {

    //deletes leaving customer
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
