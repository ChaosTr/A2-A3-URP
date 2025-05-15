using UnityEngine;
using UnityEngine.Events;

    public class DialogueBinder : MonoBehaviour
    {
        public string BinderName;

        [System.Serializable]
        public class SubtitleEvent : UnityEvent<string> { }

        public UnityEvent OnDialogueStart;
        public SubtitleEvent OnSubtitle;
        public UnityEvent OnDialogueEnd;

        public void TriggerStart(string binderName)
        {
            if (binderName == BinderName)
                OnDialogueStart?.Invoke();
        }

        public void TriggerSubtitle(string binderName, string text)
        {
            if (binderName == BinderName)
                OnSubtitle?.Invoke(text);
        }

        public void TriggerEnd(string binderName)
        {
            if (binderName == BinderName)
                OnDialogueEnd?.Invoke();
        }
    }