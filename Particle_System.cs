using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_System : MonoBehaviour
{
    public ParticleSystem Poof;
    public Rigidbody Body;
    // Start is called before the first frame update
    void Start()
    {
        Body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.layer == 3)
        {
            //CreatePoof();             
        }
    }
    void CreatePoof()
    {
        Poof.Play();
    }
}
