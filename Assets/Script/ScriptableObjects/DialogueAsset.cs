using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Custom/Dialogue Asset")]
public class DialogueAsset : ScriptableObject
{
    public List<DialogueEntry> Dialogues = new();

    [System.Serializable]
    public class DialogueEntry
    {
        public AudioClip Audio;
        public string SubtitleText;
        public float Duration = 2f;
    }
}
