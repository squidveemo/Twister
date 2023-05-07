using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player2 : MonoBehaviour
{
    public float Speed;
    public float RotateAngle = 1f;
    public float RotateTimer;
    public float Rotatepos;
    public float BoostMultiplier = 1f;
    public Rigidbody Body;
    public bool ButtonPressed = false;
    bool BoostMinus;
    public RaycastHit hit;
    public RaycastHit grounded;
    public bool Grounded;
    bool Apressed;
    bool Dpressed;
    [SerializeField] private LayerMask road;
    // Start is called before the first frame update
    void Start()
    {
        Body = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 80f, road))
        {
            Vector3 RoadPos = (Vector3.left - hit.normal * Vector3.Dot(Vector3.left, hit.normal) * Time.fixedDeltaTime);
            transform.Rotate(RoadPos);
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out grounded, 20f, road))
        {
            Grounded = true;
        }
        else
            Grounded = false;

        if (Grounded == false)
        {
            Body.AddForce(Physics.gravity * Body.mass);
        }
    }
    void Update()
    //go forward
    {
        if (RotateTimer >= 1.25)
        {
            transform.Translate(Vector3.forward * Speed * RotateTimer * BoostMultiplier * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.forward * Speed * BoostMultiplier * Time.deltaTime);
        }

        if (RotateTimer > 1 && ButtonPressed == false)
        {
            RotateTimer = RotateTimer - 3 * Time.deltaTime;
            Speed = 30;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            RotateTimer = 1;

            ButtonPressed = true;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {

            Vector3 newRotation = (new Vector3(0, -RotateAngle, 0) * Time.deltaTime);
            transform.Rotate(newRotation);
            ButtonPressed = true;
            Apressed = true;
            Speed = 0;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && RotateTimer <= 3 && Dpressed == false)
        {
            RotateTimer = RotateTimer + 1.5f * Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            Speed = 30;

            ButtonPressed = false;
            Apressed = false;
        }


        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ButtonPressed = true;
            RotateTimer = 1;
        }
        if (Input.GetKey(KeyCode.RightArrow) && RotateTimer <= 3 && Apressed == false)
        {
            RotateTimer = RotateTimer + 1.5f * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 newRotation = (new Vector3(0, RotateAngle, 0) * Time.deltaTime);
            transform.Rotate(newRotation);
            ButtonPressed = true;
            Dpressed = true;
            Speed = 0;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            Speed = 30;

            ButtonPressed = false;
            Dpressed = false;
        }
        if (BoostMultiplier <= 1)
        {
            BoostMinus = false;
        }
        if (BoostMinus == true)
        {
            BoostMultiplier = BoostMultiplier - Time.deltaTime;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Death")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }
    }
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "BoostFire")
        {
            BoostMultiplier = 2;
        }
    }
    private void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "BoostFire" && BoostMultiplier > 0)
        {
            BoostMinus = true;
        }
    }
}

