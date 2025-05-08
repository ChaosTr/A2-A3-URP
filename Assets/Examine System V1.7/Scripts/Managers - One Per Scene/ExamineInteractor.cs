using UnityEngine;

namespace ExamineSystem
{
    [RequireComponent(typeof(Camera))]
    public class ExamineInteractor : MonoBehaviour
    {
        public static ExamineInteractor Instance;
        [SerializeField] private float interactDistance = 5;

        private ExaminableItem examinableItem;
        public bool IsExamining {get; private set;}
        private Camera _camera;

        public Light light1, light2, light3;

        void Start()
        {
            Instance = this;
            if (!TryGetComponent<Camera>(out _camera))
            {
                Debug.LogError("Camera component not found on the GameObject.");
            }
        }

        void Update()
        {
            if (Physics.Raycast(_camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f)), transform.forward, out RaycastHit hit, interactDistance))
            {
                var examineItem = hit.collider.GetComponent<ExaminableItem>();
                if (examineItem != null)
                {
                    examinableItem = examineItem;
                    examinableItem.ItemHighlight(true);
                    HighlightCrosshair(true);
                }
                else
                {
                    ClearExaminable();
                }
            }
            else
            {
                ClearExaminable();
            }

            // if (examinableItem != null)
            // {
            //     if (Input.GetKeyDown(ExamineInputManager.instance.interactKey))
            //     {
            //         examinableItem.ExamineObject();
            //         SetLight(true);
            //     }
            //     if (Input.GetKeyDown(ExamineInputManager.instance.dropKey))
            //      {
            //         SetLight(false);
            //     }
            // }
            

            
        }

        public void InteractCurrentItem()
        {
            if (examinableItem != null){ examinableItem.ExamineObject(); IsExamining = true;}
        }

        public void PutbackObject()
        {
            if (examinableItem != null) {examinableItem.DropObject(true); IsExamining = false;}
        }

        public void SetLight (bool value){
            light1.enabled = value;
                    light2.enabled = value;
                    light3.enabled = value;
        }

        private void ClearExaminable()
        {
            if (examinableItem != null)
            {
                examinableItem.ItemHighlight(false);
                HighlightCrosshair(false);
                examinableItem = null;
            }
        }

        void HighlightCrosshair(bool on)
        {
            ExamineUIManager.instance.HighlightCrosshair(on);
        }
    }
}
