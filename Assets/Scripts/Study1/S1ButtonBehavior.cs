using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class S1ButtonBehavior : MonoBehaviour
{
    public AcceStimulate acce;
    public S1Clock s1clock;
    public float TestTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        s1clock = GetComponentInChildren<S1Clock>();
        acce = GetComponent<AcceStimulate>();
        acce.InEvent.AddListener(s1clock.StartCountdown);
        acce.OutEvent.AddListener(() =>
        {
            s1clock.ResetStates();
            print("out");
        });
    }

    // Update is called once per frame
    void Update()
    {
        //print(s1clock.PrematureExit);
    }
}
