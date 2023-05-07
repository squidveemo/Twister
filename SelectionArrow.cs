using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionArrow : MonoBehaviour
{
    //public Rigidbody Body;
    // Start is called before the first frame update
    public bool Left = true;
    public bool Right = true;
    void Start()
    {
        //Body = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && Left || (Input.GetKeyDown(KeyCode.LeftArrow) && Left))
        {
            transform.position = transform.position + (new Vector3(-15, 0, 0));
        }
        if (Input.GetKeyDown(KeyCode.D) && Right || (Input.GetKeyDown(KeyCode.RightArrow) && Right))
        {
            transform.position = transform.position + (new Vector3(15, 0, 0));
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = transform.position + (new Vector3(0, -3, 0));
        }
    }
    void OnTriggerEnter(Collider Coll)
    {
        if (Coll.gameObject.name == "SummerSpeedway")
        {
            SceneManager.LoadScene(3);
        }
        if (Coll.gameObject.name == "BlossomPark")
        {
            SceneManager.LoadScene(4);
        }
        if (Coll.gameObject.name == "FallSlopes")
        {
            SceneManager.LoadScene(6);
        }
        if (Coll.gameObject.name == "WinterWorld")
        {
            SceneManager.LoadScene(7);
        }
        if (Coll.gameObject.name == "BL")
        {
            Left = false;
        }
        if (Coll.gameObject.name == "BR")
        {
            Right = false;
        }
    }
        void OnTriggerExit(Collider Coll)
    {
        if (Coll.gameObject.name == "BL")
        {
            Left = true;
        }
        if (Coll.gameObject.name == "BR")
        {
            Right = true;
        }
    }
    }
    
