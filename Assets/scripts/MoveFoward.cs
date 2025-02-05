using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveFoward : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float speed = 40;
    private float topBound = 70;
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if (transform.position.x > topBound) {
            Destroy(gameObject); 
        }
    }
}