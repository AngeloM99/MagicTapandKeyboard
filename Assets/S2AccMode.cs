using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
// For all, Out-Enter-Out will have material change be like Out-in-out coded in AcceStimulate.cs
public class S2AccMode : MonoBehaviour
{
    public AcceStimulate Acce;
    public TMPro.TMP_Text Number;
    public ButtonUtils BU;
    public S2SetVariable ParentSSV;

    public void Start()
    {
        ParentSSV = transform.parent.GetComponent<S2SetVariable>();
        Acce = GetComponent<AcceStimulate>();
    }
    public void SetHesMode()
    {
        if (ParentSSV.Long || ParentSSV.Velo || ParentSSV.Acce || ParentSSV.AcceOnly || ParentSSV.AcceOld)
            Acce.BeforeHesEvent.AddListener(Acce.SetInvoked);

        Acce.HesEvent.AddListener(BU.ButtonSelected);
        Acce.HesEvent.AddListener(Acce.ToggleHLMaterial);
        Acce.HesEvent.AddListener(Acce.OpenCloseSti); // CloseSti set to be false after out

        Acce.AfterOutEvent.AddListener(Acce.UnInvoked);
    }
    public void SetFastMode()
    {
        if (ParentSSV.Fast)
        {
            Acce.HesEvent.AddListener(Acce.OutMaterial);
            Acce.HesEvent.AddListener(Acce.OpenCloseSti); // CloseSti set to be false after out

            Acce.OutEvent.AddListener(BU.ButtonSelected);
            Acce.OutEvent.AddListener(Acce.ToggleHLMaterial);
        }
            
    }
    public void Set01Mode()
    {
        if (ParentSSV.Mode010)
        {
            Acce.InEvent.AddListener(BU.ButtonSelected);
            Acce.InEvent.AddListener(Acce.ToggleHLMaterial);
            Acce.InEvent.AddListener(Acce.SetInvoked);
            Acce.AfterOutEvent.AddListener(Acce.UnInvoked);
        }
            
    }   
    public void Set010Mode()
    {
        if (ParentSSV.Mode01)
        {
            Acce.OutEvent.AddListener(BU.ButtonSelected);
            Acce.OutEvent.AddListener(Acce.ToggleHLMaterial);
        }
        
    }
}
