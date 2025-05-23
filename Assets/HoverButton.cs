using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonChange : MonoBehaviour
{
    public TextMeshProUGUI buttonText;
    public Button button;
    public Sprite image1;
    public Image buttonImage;
    private Color defaultTextColor = Color.white;
    private Color hoverTextColor = Color.black;

    public void changeWhenHover()
    {
        if (buttonText != null)
        {
            buttonText.color = hoverTextColor;
            buttonImage.sprite = image1;

            Color newColor = buttonImage.color;
            newColor.a = 1f; // Full opacity
            buttonImage.color = newColor;

        }
    }

    public void changeBack()
    {
        if (buttonText != null)
        {
            buttonText.color = defaultTextColor;
            buttonImage.sprite = null;

            Color newColor = buttonImage.color;
            newColor.a = 0f; // Fully transparent
            buttonImage.color = newColor;
        }
    }
}