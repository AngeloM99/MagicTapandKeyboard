using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.Rendering;// For all, Out-Enter-Out will have material change be like Out-in-out coded in AcceStimulate.cs

// This set of code is design ONLY for keyboard.
public class S1AccModeForKeyboard : MonoBehaviour
{
    public AcceStimulateForKeyboard Acce;
    // public S1Player Player;
    public GetText Text;
    //public CancelRainbow CancelR;
    public CrossPlane cp;
    public GameObject ParentObject;
    public GameObject TriggerPlane;
    public GameObject ParentObjectParent;
    public S1SetVariableForKeyboard ssv;
    private void Start()
    {
        Acce = gameObject.GetComponent<AcceStimulateForKeyboard>();
        Text = gameObject.GetComponent<GetText>();
        ParentObject = transform.parent.gameObject;
        ssv = ParentObject.GetComponent<S1SetVariableForKeyboard>();

        if(ssv.FastForKeyboard == true)
        {
            ParentObjectParent = ParentObject.transform.parent.gameObject;
            TriggerPlane = ParentObjectParent.transform.Find("TriggeringPlane").gameObject;
            cp = TriggerPlane.GetComponent<CrossPlane>();
        }
    }

    public void SetKeyboardFastTapMode()
    {
        Acce.HesEvent.AddListener(Acce.OutMaterial);
        Acce.HesEvent.AddListener(Acce.OpenCloseSti); // CloseSti set to be false after out
        // tp.ExitEvent.AddListener(Text.UpdateDisplay);
        Acce.OutEvent.AddListener(Text.UpdateDisplay); 
    }
    public void SetHesMode()
    {
        Acce.OutEvent.AddListener(DebugOutput);

        Acce.BeforeHesEvent.AddListener(Acce.SetInvoked);
        Acce.HesEvent.AddListener(Text.UpdateDisplay);

        Acce.HesEvent.AddListener(Acce.OutMaterial);
        Acce.HesEvent.AddListener(Acce.OpenCloseSti); // CloseSti set to be false after out

        Acce.HesEvent.AddListener(Acce.showDebugger);

        Acce.AfterOutEvent.AddListener(Acce.UnInvoked);
    }
    public void SetFastMode()
    {
        Acce.OutEvent.AddListener(DebugOutput);
        Acce.HesEvent.AddListener(Acce.OutMaterial);
        Acce.HesEvent.AddListener(Acce.OpenCloseSti); // CloseSti set to be false after out
        //Acce.OutEvent.AddListener(Text.UpdateDisplay);
        //cp.ExitEvent.AddListener(Text.UpdateDisplay);
    }
    public void Set01Mode()
    {
        Acce.OutEvent.AddListener(DebugOutput);
        Acce.InEvent.AddListener(Text.UpdateDisplay);

        Acce.InEvent.AddListener(Acce.SetInvoked);
        Acce.InEvent.AddListener(Acce.OutMaterial);
        Acce.AfterOutEvent.AddListener(Acce.UnInvoked);

    }
    public void Set010Mode()
    {

        Acce.OutEvent.AddListener(DebugOutput);
        Acce.OutEvent.AddListener(Text.UpdateDisplay);

    }

    public void DebugOutput()
    {
        //print("success");
    }

    public void NewFastTapInEventMethodGroup()
    {
    }

    //public void set010cancelmode()
    //{
    //    acce.inevent.addlistener(cancelr.cancel);
    //    acce.outevent.addlistener(player.changecolor);
    //}

}
