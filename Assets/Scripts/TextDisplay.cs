using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextDisplay : MonoBehaviour
{
    public string preString;
    public string proString;
    public TMP_Text textToUpdate;
    public void UpdateText(string text)
    {
        textToUpdate.text = preString + text + proString;
    }

    public void UpdateText(int intText)
    {
        UpdateText(intText.ToString());
    }

    public void UpdateText(float floatText)
    {
        UpdateText(floatText.ToString());
    }

}
