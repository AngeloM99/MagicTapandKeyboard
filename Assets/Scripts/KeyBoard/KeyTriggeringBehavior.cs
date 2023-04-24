using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using static KeyTriggeringBehavior;

[Serializable]
public class VisualFeedbacks
{
    public Material ActiveMaterial;
    public Material InactiveMaterial;
}

[Serializable]
public class KeyboardUtilities
{
    public AcceStimulateForKeyboard acce;
    public GetText gt;
    public CrossPlane cp;
}

[Serializable]
public class TextInputUtilities
{
    public TextMeshPro InputTextField;
    [SerializeField]
    public string InputText;
    public string ButtonText;
    public string PreEnteringText;
}

[Serializable]
public class ButtonAttributes
{
    public GameObject Button;
    public Renderer ButtonRenderer;
}

[Serializable]
public class ScriptConstants
{
    public Color PreEnteringColor = Color.grey;
    public Color EnteredColor = Color.black;
}

public class KeyTriggeringBehavior : MonoBehaviour
{
    public VisualFeedbacks VisualFeedback;
    public KeyboardUtilities Utilities;
    public TextInputUtilities TextInput;
    public ButtonAttributes Attributes;
    public ScriptConstants Variables;

    [Space()]
    public bool init = false;
    // Start is called before the first frame update
    void Start()
    {
        #region Initiation

        // Get Utilities
        Utilities.acce = GetComponent<AcceStimulateForKeyboard>();
        Utilities.gt = GetComponent<GetText>();
        Utilities.cp = GetComponent<CrossPlane>();

        // Get Button Attribute
        Attributes.Button = gameObject;
        Attributes.ButtonRenderer = GetComponent<Renderer>();

        // Get Text Utilities
        TextInput.InputText = TextInput.InputTextField.text;

        #endregion

        Utilities.cp.EnterEvent.AddListener(OnEntering);
        Utilities.acce.OutEvent.AddListener(OnOut);
    }

    public void OnEntering()
    {
        #region Archive Code -----   Make Sure Write This in Another Script that can loop through all buttons.
        //if(init == false)
        //{
        //    init = true;
        //    TextInput.InputTextField.text = " ";
        //}

        //if(init == true)
        //{
        //    Attributes.ButtonRenderer.material = VisualFeedback.ActiveMaterial;
        //}
        #endregion
        Attributes.ButtonRenderer.material = VisualFeedback.ActiveMaterial;
    }

    public void OnOut()
    {
        Attributes.ButtonRenderer.material = VisualFeedback.InactiveMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
