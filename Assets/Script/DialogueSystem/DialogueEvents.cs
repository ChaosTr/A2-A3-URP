using UnityEngine;
using UnityEngine.Events;

public class DialogueEvents : MonoBehaviour
{
    public UnityEvent OnDialogueStart;
    public UnityEvent OnDialogueEnd;

    public void InvokeStart()
    {
        OnDialogueStart?.Invoke();
    }

    public void InvokeEnd()
    {
        OnDialogueEnd?.Invoke();
    }
}
