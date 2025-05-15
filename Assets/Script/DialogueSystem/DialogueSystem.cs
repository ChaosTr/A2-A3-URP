using System.Linq;
using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance;

    public CanvasGroup dialoguePanel;
    public TMP_Text dialogueText;
    public AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
        dialoguePanel.alpha = 0f;
        dialogueText.text = "";
    }

    public void PlayDialogue(DialogueAsset asset)
    {
        StopAllCoroutines();
        StartCoroutine(PlayDialogueSequence(asset));
    }

    IEnumerator PlayDialogueSequence(DialogueAsset asset)
    {
        dialoguePanel.alpha = 1f;

        foreach (var entry in asset.Dialogues)
        {
            audioSource.clip = entry.Audio;
            audioSource.Play();
            dialogueText.text = entry.SubtitleText;
            yield return new WaitForSeconds(entry.Duration);
        }

        dialoguePanel.alpha = 0f;
        dialogueText.text = "";
    }
}