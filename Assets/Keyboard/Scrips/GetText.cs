using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.ProBuilder.MeshOperations;
using VRKeys;
using UnityEngine.Rendering.UI;
// using UnityEditor.Animations;

public class GetText : MonoBehaviour
{
    // Get KeyboardButtons here
    public GameObject KeyboardButton;
    public bool HighlightedKey;
    #region special Keys 

    // InputKeys
    public GameObject SpaceBar, DeleteKey, TabKey, EnterKey;

    //SpecialFunction Keys.
    public GameObject ShiftKey, LanguageKey, SwitchNumberKey, SwitchPinyinKey;

    // Delete Key Behavior. 
    public DeleteBehavior DeleteBehavior;

    #endregion
///<param name="textbox">The input textbox object, this is required to access Components</param>
///<param name="textUpdate">This is for updating the textbox</param>
///<param name="Txtinput">A shared string is needed to store all the input in a separate location to avoid the text being refreshed everytime we pressed the button</param>
    public GameObject textbox;
    public TextMeshProUGUI textUpdate;
    public Textinput Txtinput;

    public TextMeshPro ButtonText;

    public AcceStimulateForKeyboard acce;

    string objectName, inputString;
    // Start is called before the first frame update
    void Start()
    {

        #region Get Special Keys

        if (gameObject.name == "2_space_text")
        {
            SpaceBar = gameObject;
        }

        if (gameObject.name == "2_tab")
        {
            TabKey = gameObject;
        }

        if (gameObject.name == "2_enter")
        {
            EnterKey = gameObject;
        }

        if (gameObject.name == "2_delete")
        {
            DeleteBehavior = gameObject.GetComponent<DeleteBehavior>();
            DeleteKey = gameObject;
        }

        if (gameObject.name == "2_left_upcase" || gameObject.name == "2_right_upcase")
        {
            HighlightedKey = true;
            ShiftKey = gameObject;
        }

        #endregion

        acce = GetComponent<AcceStimulateForKeyboard>();
        KeyboardButton = gameObject;
        Txtinput = textbox.GetComponent<Textinput>();

        ButtonText = GetComponentInChildren<TextMeshPro>();
        if (ButtonText != null)
        {
            CaseControl("toLower");
        }
        // acce.InEvent.AddListener(PrintTextOnEnter);
        // acce.InEvent.AddListener(UpdateDisplay);
    }

    // Update is called once per frame

    /// <param name="Update Display">Update text to display, main funtion for Updating keys</param>
    public void UpdateDisplay()
    {
        GetKeyBindingText();
        textUpdate.text += inputString;
    }

    public void GetKeyBindingText()
    {
        if (gameObject == DeleteKey)
        {
            DeleteKeyBehavior();
        }
        else if (gameObject == SpaceBar)
        {
            inputString = " ";
        }
        else if (gameObject == TabKey)
        {
            inputString = "\t";
        }
        else if (gameObject == EnterKey)
        {
            inputString = "\n";
        }
        #region Shift key pressed
        else if (gameObject == ShiftKey)
        {
            ShiftKeyPressed();
        }
        #endregion
        else
        {
            GetKeyValue();
            //print("Got");
        }
    }

    /// <param name="Get Key Value">Get value from key </param>
    public void GetKeyValue()
    {
        // print("success");
        objectName = KeyboardButton.name;
        inputString = objectName;
        //print(objectName);
    }


    /// <param name="CaseType">Set input to Upper or Lower Case</param>
    void CaseControl(string CaseType)
    {
        if (CaseType == "toUpper")
        {
            string ButtonTextUpperCase = ButtonText.text.ToUpper();
            ButtonText.text = ButtonTextUpperCase;
        }
        if (CaseType == "toLower")
        {
            string ButtonTextLowerCase = ButtonText.text.ToLower();
            ButtonText.text = ButtonTextLowerCase;
        }
    }

    public void DeleteKeyBehavior()
    {
        if (textUpdate.text.Length > 0)
        {
            textUpdate.text = textUpdate.text.Substring(0, textUpdate.text.Length - 1);
        }
    }

    #region Shift Key Behavior

    void ShiftKeyPressed()
    {
        return;
    }

    #endregion

}
