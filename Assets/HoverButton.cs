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
    private Color defaultTextColor = Color.white;
    private Color hoverTextColor = Color.black;

    public void changeWhenHover()
    {
        if (buttonText != null)
        {
            buttonText.color = hoverTextColor;
            button.image.sprite = image1;

            Color newColor = button.image.color;
            newColor.a = 1f; // Full opacity
            button.image.color = newColor;

        }
    }

    public void changeBack()
    {
        if (buttonText != null)
        {
            buttonText.color = defaultTextColor;
            button.image.sprite = null;

            Color newColor = button.image.color;
            newColor.a = 0f; // Fully transparent
            button.image.color = newColor;
        }
    }
}