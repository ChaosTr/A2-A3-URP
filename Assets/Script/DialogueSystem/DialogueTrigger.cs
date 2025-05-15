using UnityEngine;


#if UNITY_EDITOR
using UnityEditor;
#endif
  public class DialogueTrigger : MonoBehaviour
{

    public DialogueAsset dialogueAsset;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && dialogueAsset != null)
        {
            DialogueSystem.Instance.PlayDialogue(dialogueAsset);
        }
    }
}
  