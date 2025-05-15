using TMPro;
using UnityEngine;


public class DialogueSpeechText : MonoBehaviour
{
    public string BinderName; // Unique name for this speaker (e.g. "Ghost")
    public TMP_Text TextMesh;
    public bool HideBetweenLines;

    private bool isPlaying;

    public void OnDialogueStart(string binderName)
    {
        isPlaying = binderName == BinderName;
        TextMesh.gameObject.SetActive(isPlaying);
    }

    public void OnSubtitle(string subtitleText)
    {
        if (!isPlaying) return;
        TextMesh.text = subtitleText;
    }

    public void OnDialogueEnd()
    {
        if (!isPlaying) return;
        TextMesh.text = "";
        TextMesh.gameObject.SetActive(false);
        isPlaying = false;
    }
}
