using System.Collections;
using TMPro;
using UnityEngine;

public class ExperimentCounter : MonoBehaviour
{
    public int count = 0;
    public int MaxCount = 10;
    public int TaskNum = 1;

    public float coolDown = 1f;

    public AcceStimulate acce;
    public Collider SphereCollider;

    public TextMeshProUGUI DisplayNum;
    public TextMeshProUGUI TaskPrompt;
    public TextMeshProUGUI GesturePrompt;

    private string CurrentTask_trigger = "Trigger";
    private string CurrentTask_DoNotTrigger = "Do not trigger";

    private string NormalGesturePrompt = "  ";
    private string BackGesturePrompt = "Withdraw your hand";
    private string CrossingGesturePrompt = "Move Away your hand";

    private bool isCoolingDown = false; // Add a flag to check if the coroutine is running

    // Start is called before the first frame update
    void Start()
    {
        SphereCollider = gameObject.GetComponent<Collider>();
        acce = GetComponent<AcceStimulate>();
        acce.OutEvent.AddListener(UpdateCount);
        TaskNum = 0;
        SwitchTask();
    }

    // Update is called once per frame
    void Update()
    {
        if (count == MaxCount && !isCoolingDown) // Check if the coroutine is not running
        {
            count = 0;
            StartCoroutine(CoolDown());
            SphereCollider.enabled = true;
        }
    }

    public void UpdateCount()
    {
        count += 1;
        DisplayNum.text = count.ToString();
    }

    public void LockTriggerOnCrossing()
    {
        if(TaskNum == 3)
        {
            acce.OutEvent.AddListener(()=>
            {
            });
        }
    }

    public void SwitchTask()
    {
        TaskNum += 1;

        switch (TaskNum)
        {
            case 1:
                TaskPrompt.color = Color.green;
                TaskPrompt.text = CurrentTask_trigger;
                GesturePrompt.text = NormalGesturePrompt;
                break;
            case 2:
                TaskPrompt.color = Color.red;
                TaskPrompt.text = CurrentTask_DoNotTrigger;
                GesturePrompt.color = Color.red;
                GesturePrompt.text = BackGesturePrompt;
                break;
            case 3:
                TaskPrompt.color = Color.red;
                TaskPrompt.text = CurrentTask_DoNotTrigger;
                GesturePrompt.color = Color.red;
                GesturePrompt.text = CrossingGesturePrompt;
                break;
        }
    }

    private IEnumerator CoolDown()
    {
        isCoolingDown = true; // Set the flag to true when the coroutine starts

        if (TaskNum <= 3)
        {
            SwitchTask();
        }
        else
        {
            TaskNum = 1;
        }
        SphereCollider.enabled = false;

        yield return new WaitForSeconds(coolDown);
        DisplayNum.text = count.ToString();

        isCoolingDown = false; // Set the flag to false when the coroutine finishes
    }
}
