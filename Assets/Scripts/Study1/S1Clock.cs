using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Build.Editor;
using Oculus.Interaction.Samples;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using static Facebook.WitAi.WitTexts;

public class S1Clock: MonoBehaviour
{
    [Header("This script is to control the behaviour of the counter and the clock")]
    public Transform clockTransform;
    public TextMeshProUGUI ClockText, PerformedActionText, PerformedActionNum, HintText;
    public Material CapsuleMaterial;
    public Vector4 DisplayBubbleActiveColor = new Vector4(0.8f, 0.8f, 0.8f, 1f);
    public Vector4 DisplayBubbleInactiveColor = new Vector4(0.3f, 0.3f, 0.3f, 1f);

    [Space]
    public string KeepStill = "Please Keep Still";
    public string MoveAway = "Please Move Away";
    public string insertion = "Press the bubble";

    [Space]
    public bool PrematureExit = false;

    private UnityEvent CountCompleteEvent;

    [Space]
    public int Count = 0;
    public float CountdownTime = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        CapsuleMaterial = clockTransform.Find("Capsule").GetComponent<Renderer>().material;
        //StartCoroutine(CountdownCoroutine());
        CountCompleteEvent ??= new UnityEvent();
        //CountCompleteEvent.AddListener(CountUpdate);
        setBubbleColor(DisplayBubbleInactiveColor);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CountUpdate()
    {
        if(Count != 3)
        {
            Count += 1;
            PerformedActionNum.text = Count.ToString();
        }
        else
        {
            Count = 0;
            PerformedActionNum.text = Count.ToString();
        }
    }

    public void setTextColor(Vector4 ColorVector)
    {
        ClockText.color = ColorVector;
    }

    public void setBubbleColor(Vector4 ColorVector)
    {
        CapsuleMaterial.SetVector("_EmissionColor", ColorVector);
    }

    public void StartCountdown()
    {
        StartCoroutine(CountdownCoroutine());
    }

    public void StopCountDown()
    {
        StopCoroutine(CountdownCoroutine());
    }

    public IEnumerator CountdownCoroutine()
    {
        float timer = CountdownTime;
        setBubbleColor(DisplayBubbleActiveColor);

        while (timer >= 0)
        {
            HintText.text = KeepStill;
            // update the text with the current timer value
            ClockText.text = timer.ToString("F2");

            // wait for one frame
            yield return null;

            // decrement the timer by delta time
            timer -= Time.deltaTime;
        }
        // countdown finished, update the text to show "0"
        setBubbleColor(Color.green);
        setTextColor(Color.green);
        ClockText.text = "0.00";
        HintText.text = MoveAway;
        CountCompleteEvent.Invoke();

    }

    public void ResetStates()
    {
        setBubbleColor(DisplayBubbleInactiveColor);
        float timer = CountdownTime;
        ClockText.text = timer.ToString("F2");
        setBubbleColor(Color.grey);
        setTextColor(Color.white);
        HintText.text = insertion;
    }
}
