using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class S1ButtonBehavior : MonoBehaviour
{
    public AcceStimulate acce;
    public S1Clock s1clock;
    public float TestTime = 5f;
    public float coolDown = 5f;

    public bool canTrigger = true;
    public bool EnteredTheArea = false;
    public bool Completed = false;

    // Start is called before the first frame update
    void Start()
    {
        s1clock = GetComponentInChildren<S1Clock>();
        acce = GetComponent<AcceStimulate>();

        // Use Lambda fucntion
        acce.InEvent.AddListener(() =>
        {
            if(s1clock.CountdownTime > 0.01 && Completed == false)
            {
                s1clock.StartCoroutine("CountdownCoroutine");
            }
            else if (s1clock.CountdownTime < 0.005)
            {
                //Completed = true;
                s1clock.StopCoroutine("CountdownCoroutine");
                //s1clock.OnSucceed();
            }
        });

        s1clock.CountCompleteEvent.AddListener(() =>
        {
            Completed = true;
            //StartCoroutine("StartCoolDown");
        });

        acce.OutEvent.AddListener(() =>
        {
            //Completed = false;
            if (s1clock.CountdownTime > 0.01 && Completed == false)
            {
                s1clock.StopCountDown();
                //StartCoroutine("StartCoolDown");
                s1clock.Warning();
            }
            else
            {
                print("complete");
                s1clock.CountUpdate();
                Completed = false;
               // s1clock.OnSucceed();
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        //print(s1clock.CountdownTime);
        //print(s1clock.PrematureExit);
    }

    private IEnumerator StartCoolDown()
    {
        canTrigger = false;
        yield return new WaitForSecondsRealtime(coolDown);
        canTrigger = true;
    }
}
