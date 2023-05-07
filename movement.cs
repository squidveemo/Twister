using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class movement : MonoBehaviour
{
    public float Speed;
    public float RotateAngle = 1f;
    public float RotateTimer;
    public float BoostMultiplier = 1f;
    public float time = 3;

    public Rigidbody Body;
    public bool ButtonPressed = false;
    public KeyCode Left;
    public KeyCode Right;

    bool BoostMinus;

    public RaycastHit hit;
    public RaycastHit grounded;
    public RaycastHit Wall;
    
    public bool WallHit;
    public bool Grounded;
    public int LosScene;
    
    bool Apressed;
    bool Dpressed;

    [SerializeField] private LayerMask road;
    [SerializeField] private LayerMask wall;

    Vector3 newRotation;
    // Start is called before the first frame update
    void Start()
    {
        Body = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    //go forward
    {
            if (time <= 0)
            {
                {
                    Body.AddForce(Physics.gravity * Body.mass * 4);
                }
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10f, road))
                {
                    Vector3 RoadPos = (Vector3.left - hit.normal * Vector3.Dot(Vector3.left, hit.normal) * Time.fixedDeltaTime);
                    transform.Rotate(RoadPos);
                }
                if (RotateTimer >= 1.25)
                {
                    transform.Translate(Vector3.forward * Speed * RotateTimer * BoostMultiplier * Time.fixedDeltaTime);
                }
                else
                {
                    transform.Translate(Vector3.forward * Speed * BoostMultiplier * Time.fixedDeltaTime);
                }
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out grounded, 3f, road))
                {
                    Grounded = true;
                }
                else
                    Grounded = false;            
    }
        //Wall detection
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Wall, 2.2f, wall))
        {
            Speed = 0;
            WallHit = true;
        }
        else
        {
            WallHit = false;
        }
    }
    void Update()
    {
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            if (RotateTimer > 1 && ButtonPressed == false)
            {
                RotateTimer = RotateTimer - 3 * Time.deltaTime;
                if (WallHit == false)
                {
                    Speed = 30;
                }
            }
            //Left movement
            if (Input.GetKeyDown(Left))
            {
                RotateTimer = 1;

                ButtonPressed = true;
            }
            if (Input.GetKey(Left) && Grounded)
            {
                newRotation = (new Vector3(0, -RotateAngle, 0) * Time.deltaTime);
                transform.Rotate(newRotation);
                ButtonPressed = true;
                Apressed = true;
                Speed = 0;
            }
            if (Input.GetKey(Left) && RotateTimer <= 3 && Dpressed == false && Grounded)
            {
                RotateTimer = RotateTimer + 1.5f * Time.deltaTime;
            }
            if (Input.GetKeyUp(Left) && Grounded)
            {
                if (WallHit == false)
                {
                    Speed = 30;
                }
                ButtonPressed = false;
                Apressed = false;
            }
            //Right movement
            if (Input.GetKeyDown(Right) && Grounded)
            {
                ButtonPressed = true;
                RotateTimer = 1;
            }
            if (Input.GetKey(Right) && RotateTimer <= 3 && Apressed == false && Grounded)
            {
                RotateTimer = RotateTimer + 1.5f * Time.deltaTime;
            }
            if (Input.GetKey(Right) && Grounded)
            {
                newRotation = (new Vector3(0, RotateAngle, 0) * Time.deltaTime);
                transform.Rotate(newRotation);
                ButtonPressed = true;
                Dpressed = true;
                Speed = 0;
            }
            if (Input.GetKeyUp(Right) && Grounded)
            {
                if (WallHit == false)
                {
                    Speed = 30;
                }
                ButtonPressed = false;
                Dpressed = false;
            }
            //boost
            if (BoostMultiplier <= 1)
            {
                BoostMinus = false;
            }
            if (BoostMinus == true)
            {
                BoostMultiplier = BoostMultiplier - Time.deltaTime;
            }
        }
        //collision & triggers
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Death")
            {
                SceneManager.LoadScene(LosScene);
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