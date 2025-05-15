using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TMPTextHelper : MonoBehaviour
{
    public TMP_Text text;

    public void SetSubtitle(string subtitle)
    {
        text.SetText(subtitle);
    }
}
