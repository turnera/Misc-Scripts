using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour {

    public float spawnTime = 0;
    public float spawnRate = 15;

    public BoxCollider Parent;
    public GameObject Customer;
    
    // Use this for initialization

    public void SpawnCustomer()
    {
        Instantiate(Customer, gameObject.transform.position, Quaternion.identity);
    }
}
