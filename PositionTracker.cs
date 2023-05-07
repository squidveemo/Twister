using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PositionTracker : MonoBehaviour
{
    public Rigidbody Body;
    public bool one = false;
    public bool two = false;
    public bool three = false;
    public bool four = false;
    public int Scenes;
    [SerializeField] private int Lap = 1;
    public ParticleSystem Victory;
    // Start is called before the first frame update
    void Start()
    {
        Body = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Checkpoint")
        {
            one = true;
        }
        if (collision.gameObject.name == "Checkpoint (1)" && one)
        {
            two = true;
        }
        if (collision.gameObject.name == "Checkpoint (2)" && one && two)
        {
            three = true;
        }
        if (collision.gameObject.name == "Checkpoint (3)" && one && two && three)
        {
            four = true;
        }
        if (collision.gameObject.name == "Finish" && one == true && two == true && three == true && four == true)
        {
            one = false;
            two = false;
            three = false;
            four = false;
            Lap = Lap + 1;
            VictoryFX();
        }
        if (Lap == 6)
        {
            SceneManager.LoadScene(Scenes);
        }
    }
        public void VictoryFX()
        {
            Victory.Play();
        }
}
