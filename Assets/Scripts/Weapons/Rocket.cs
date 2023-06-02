using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public GameObject particleExplosion;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        transform.position += new Vector3(0, 0, 1) * Time.deltaTime;    
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(particleExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
