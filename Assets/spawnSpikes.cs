using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnSpikes : MonoBehaviour {

    public Transform prefab;
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter(Collider other)
    {

        Vector3 SpawnHere = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Instantiate(prefab, SpawnHere, other.transform.rotation);
        Destroy(gameObject);
    }
}
