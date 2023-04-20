using Oculus.Platform.Models;
using Oculus.Platform.Samples.VrHoops;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class S2SetVariable : MonoBehaviour
{
    public Material OutMaterial;
    public Material InMaterial;
    public Material HLMaterial;
    public bool Mode01, Mode010, Long, Fast, Acce, Velo, AcceOnly, AcceOld, showDebug;
    public TMPro.TMP_Text Logic, Task, Round;
    public S2AccMode[] Buttons;
    private string[] LogicStr;
    public int RoundNum, CorrectNum, TaskType, CurRnd, CurLogic;
    public HashSet<int> ChosenIndex, CurCorrectIndex;
    private int[,,] CorrectIndexArray, GameArray;

    private void Start()
    {
        RoundNum = 5;
        CorrectNum = 5;
        TaskType = 4;
        LogicStr = new string[3] { "01", "010","Fast" };
        CorrectIndexArray = new int[2, 5, 5]
        {
            {
                {25,18,15,21,17},
                {22,18,15,19,17},
                {25,18,15,21,17},
                {25,18,15,21,17},
                {25,18,15,21,17}
            },
            {
                {25,18,15,21,17},
                {22,18,15,19,17},
                {25,18,15,21,17},
                {25,18,15,21,17},
                {25,18,15,21,17}
            }
        };
        GameArray = new int[3, 5, 12]
        {
            {
                {1,25,6,18,15,8,21,17,37,7,54,35},
                {1,22,6,18,15,8,19,17,37,7,54,35},
                {1,25,6,18,15,8,21,17,37,7,54,35},
                {1,25,6,18,15,8,21,17,37,7,54,35},
                {1,25,6,18,15,8,21,17,37,7,54,35}
            },
            {
                {1,25,6,18,15,8,21,17,37,7,54,35},
                {1,22,6,18,15,8,19,17,37,7,54,35},
                {1,25,6,18,15,8,21,17,37,7,54,35},
                {1,25,6,18,15,8,21,17,37,7,54,35},
                {1,25,6,18,15,8,21,17,37,7,54,35}
            },
            {
                {1,25,6,18,15,8,21,17,37,7,54,35},
                {1,22,6,18,15,8,19,17,37,7,54,35},
                {1,25,6,18,15,8,21,17,37,7,54,35},
                {1,25,6,18,15,8,21,17,37,7,54,35},
                {1,25,6,18,15,8,21,17,37,7,54,35}
            }
        };


        ChosenIndex = new HashSet<int>();

        CurRnd = 0;
        CurLogic = 0;
        CurCorrectIndex = new HashSet<int>();
        for (int i = 0; i < CorrectNum; i++)
        {
            CurCorrectIndex.Add(CorrectIndexArray[CurLogic, CurRnd, i]);
        }

        Buttons = new S2AccMode[transform.childCount - 1];
        int temp = 0;
        foreach (Transform t in transform)
        {
            if (t.name.StartsWith("Sphere"))
            {
                Buttons[temp++] = t.gameObject.GetComponent<S2AccMode>();
            }
        }

        Init(gameObject);
        SetButtonText();
        SetMaterial(gameObject);
        SetMode(gameObject);
    }
    private void Update()
    {
        if (ChosenIndex.Count == 5)
        {
            if (ChosenIndex.SetEquals(CurCorrectIndex))
            {
                Debug.Log("Bingo!");
                GoNextRnd();
            }
            else
            {
                Debug.Log("Fail!");
            }
            ResetButtons();
        }
    }
    public void Init(GameObject root)
    {
        if (root.transform.childCount == 0) return;
        foreach (Transform t in root.transform)
        {
            AcceStimulate acce;
            S2AccMode sam;
            ButtonUtils bu;
            acce = t.GetComponent<AcceStimulate>();
            sam = t.GetComponent<S2AccMode>();
            bu = t.GetComponent<ButtonUtils>();
            if (acce != null && sam != null)
            {
                sam.Acce = acce;
                sam.Number = sam.GetComponentInChildren<TMPro.TMP_Text>();
                sam.BU = bu;
            }
        }
    }
    public void SetMaterial(GameObject root)
    {
        AcceStimulate acce;
        acce = root.GetComponent<AcceStimulate>();

        if (acce != null)
        {
            acce.OutMate = OutMaterial;
            acce.InMate = InMaterial;
            acce.HLMate = HLMaterial;
        }
        if (root.transform.childCount == 0) return;
        foreach (Transform t in root.transform)
        {
            SetMaterial(t.gameObject);
        }
    }
    public void SetButtonText()
    {
        int i = 0;
        foreach (S2AccMode bt in Buttons)
        {
            bt.GetComponentInChildren<TMPro.TMP_Text>().text = GameArray[CurLogic, CurRnd, i].ToString();
            i++;
        }
    }
    public string GetRound()
    {
        return "Win: " + CurRnd.ToString() + "/" + RoundNum.ToString();
    }
    public void SetMode(GameObject root)
    {
        AcceStimulate acce;
        acce = root.GetComponent<AcceStimulate>();
        if (acce != null)
        {
            SetAcce(acce);
        }
        if (root.transform.childCount == 0) return;
        foreach (Transform t in root.transform)
        {
            SetMode(t.gameObject);
        }
    }

    public void SetAcce(AcceStimulate tas)
    {
        //Debug.LogFormat("{0}: setacce", tas.gameObject.name);
        tas.Delay = Long;
        tas.Fast = Fast;
        tas.Acce = Acce;
        tas.Velo = Velo;
        tas.AcceOnly = AcceOnly;
        tas.AcceOld = AcceOld;
        tas.showDebug = showDebug;
        if (showDebug)
        {
            tas.Debugger = new TMPro.TMP_Text[4];
            foreach (Transform t in tas.transform)
            {
                if (t.gameObject.name == "A")
                    tas.Debugger[0] = t.GetComponent<TMPro.TMP_Text>();
                if (t.gameObject.name == "V")
                    tas.Debugger[1] = t.GetComponent<TMPro.TMP_Text>();
                if (t.gameObject.name == "T")
                    tas.Debugger[2] = t.GetComponent<TMPro.TMP_Text>();
                if (t.gameObject.name == "Con")
                    tas.Debugger[3] = t.GetComponent<TMPro.TMP_Text>();
            }
        }
        S2AccMode sam = tas.gameObject.GetComponent<S2AccMode>();
        if (Mode01)
        { sam.Set01Mode(); }
        else if (Mode010)
        { sam.Set010Mode(); }
        else if (Fast)
        { sam.SetFastMode(); }
        else if (Long || Velo || Acce || AcceOnly || AcceOld)
        { sam.SetHesMode(); }
    }

    public void GoNextRnd()
    {
        // Update Round
        if (CurRnd == 5)
        {
            CurRnd = 0;
            if (CurLogic == LogicStr.Length)
                CurLogic = 0;
            else
                CurLogic++;
            SetLogic();
        }
        else
            CurRnd++;
        // Update Title
        Logic.text = "Logic: "+LogicStr[CurLogic];
        Round.text = GetRound();
        // Update Logic
        // Update KeyboardButtons
        SetButtonText();
        // Update Correct Index
        CurCorrectIndex.Clear();
        for (int i = 0; i < CorrectNum; i++)
        {
            CurCorrectIndex.Add(CorrectIndexArray[CurLogic, CurRnd, i]);
        }
    }
    public void SetLogic()
    {
        if (CurLogic == 0)
        {
            Mode01 = true;
            Mode010 = false;
            Fast = false;
            Long = false;
            Velo = false;
            Acce = false;
        }
        else if(CurLogic == 1)
        {
            Mode01 = false;
            Mode010 = true;
            Fast = false;
            Long = false;
            Velo = false;
            Acce = false;
        }
        else if(CurLogic == 2)
        {
            Mode01 = false;
            Mode010 = false;
            Fast = true;
            Long = false;
            Velo = false;
            Acce = false;
        }
        SetMode(gameObject);
    }
    public void ResetButtons()
    {
        ChosenIndex.Clear();
        foreach (S2AccMode sam in Buttons)
        {
            sam.BU.ButtonReset();
        }
    }
}
