using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiate : MonoBehaviour {

    public Transform prefab;
    void Start()
    {
        StartCoroutine(Example());
    }

    IEnumerator Example()
    {
        while (true) {
            yield return new WaitForSeconds(3);
            Instantiate(prefab, transform.position, transform.rotation);
        }
    }
}
