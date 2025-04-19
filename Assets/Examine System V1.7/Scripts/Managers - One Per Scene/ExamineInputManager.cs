using Unity.VisualScripting;
using UnityEngine;

namespace ExamineSystem
{
    public class ExamineInputManager : MonoBehaviour
    {
        [Header("Raycast Pickup Input")]
        public KeyCode interactKey;

        [Header("Rotation Key Inputs")]
        public KeyCode rotateKey;
        public KeyCode dropKey;

        [Header("Should persist?")]
        [SerializeField] private bool persistAcrossScenes = true;

        public static ExamineInputManager instance;

        public Light light1, light2, light3;
        public bool isTriggered = false;
        public bool isOn = false;

        public void Update()
        {
            if (Input.GetKeyDown(interactKey) && !isTriggered)
            {
                isOn = !isOn;
                light1.enabled = isOn;
                light2.enabled = isOn;
                light3.enabled = isOn;
                isTriggered = true;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isTriggered = false;
                light1.enabled = false;
                light2.enabled = false;
                light3.enabled = false;
                isOn = false;
            }
        }

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                if (persistAcrossScenes)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
        }
    }
}
