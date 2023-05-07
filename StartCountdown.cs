using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartCountdown : MonoBehaviour
{
    public float time = 3;
    public Text Count;
    int Counter = 3;
    public GameObject Countdown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        if (time <= 2)
        {
            Counter = 2;
        }
        if (time <= 1)
        {
            Counter = 1;
        }
        if (time <= 0)
        {
            Counter = 0;
        }
        if (Counter <= 0)
        {
            Destroy(Countdown);
        }
        Count.text = Counter.ToString();
    }
}
