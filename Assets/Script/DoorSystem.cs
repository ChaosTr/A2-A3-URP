using System.Collections;
using UnityEngine;

public class DoorSystem : MonoBehaviour, IInteract
{
    public KeyType requiredKey = KeyType.None;
    public Transform hinge; // The hinge or pivot point of the door
    public float openAngle = 90f;
    public float openSpeed = 3f;
    public float interactDistance = 3f;

    //public AudioSource audioSource;
    //public AudioClip openClip;
    //public AudioClip closeClip;
    //public AudioClip lockedClip;
    [Header("Door Shake")]
    public float shakeDuration = 0.3f;

    public float shakeAmount = 5f;
    public float shakeSpeed = 50f;
    public float elapsed = 0f;

    private bool isOpen = false;
    private bool hasOpened = false;
    private Vector3 initialForward;
    private bool isShaking;
    private Quaternion baseRotation;

    private void Start()
    {

        initialForward = transform.forward;
        baseRotation = hinge.rotation;
    }

    #region Deprecated

    //void OnMouseOver()
    //{
    //    GameObject player = GameObject.FindWithTag("Player");
    //
    //    if (Vector3.Distance(player.transform.position, transform.position) > interactDistance)
    //        return; // Too far away, don't do anything
    //
    //    if (Input.GetMouseButtonDown(0)) // Left-click
    //    {
    //        HandleDoorToggle();
    //    }
    //}

    /*void TryOpen()
    {
        GameObject player = GameObject.FindWithTag("Player");
        ItemPickup pickup = player.GetComponent<ItemPickup>();

        GameObject heldItem = GetHeldItem(pickup);
        KeyType heldKey = GetHeldKeyType(heldItem);

        if (requiredKey == KeyType.None || heldKey == requiredKey)
        {
            if (!hasOpened)
            {
                OpenDoor(player.transform.position);

                if (heldItem != null && requiredKey != KeyType.None)
                {
                    Debug.Log($"[DoorSystem] Used key: {heldKey}. Destroying key.");
                    Destroy(heldItem);
                }

                hasOpened = true;
            }
        }
        else
        {
            Debug.Log($"[DoorSystem] This door requires: {requiredKey}, but player has: {heldKey}");
        }
    }*/
    /*void ToggleDoor()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Vector3 playerPos = player.transform.position;

        // Flip open state
        isOpen = !isOpen;

        Vector3 toPlayer = playerPos - transform.position;
        float dot = Vector3.Dot(transform.right, toPlayer);
        float angle = (dot > 0) ? -openAngle : openAngle;

        Quaternion targetRotation = isOpen
            ? Quaternion.Euler(0, angle, 0) * transform.rotation
            : Quaternion.identity;

        Debug.Log("[DoorSystem] Door " + (isOpen ? "opened" : "closed"));
        StopAllCoroutines();
        StartCoroutine(RotateDoor(targetRotation));
    }*/

    #endregion Deprecated

    /*
    void HandleDoorToggle()
    {
        GameObject player = GameObject.FindWithTag("Player");
        ItemPickup pickup = player.GetComponent<ItemPickup>();
        GameObject heldItem = GetHeldItem(pickup);
        KeyType heldKey = GetHeldKeyType(heldItem);

        //Player is trying to open a locked door without correct key
        bool requiresKey = requiredKey != KeyType.None;
        bool wrongKey = heldKey != requiredKey && heldKey != KeyType.None;
        bool noKey = heldKey == KeyType.None;

        if (!hasOpened && requiresKey && (wrongKey || noKey))
        {
            Debug.Log("[DoorSystem] Door locked - shake it!");
            //if (lockedClip != null) audioSource.PlayOneShot(lockedClip);
            StartCoroutine(ShakeDoor()); // always allowed before unlock
            return;
        }

        //If door not yet unlocked and player has correct key
        if (!hasOpened)
        {
            hasOpened = true;
            Debug.Log("[DoorSystem] Door unlocked.");

            if (heldItem != null && requiredKey != KeyType.None)
            {
                Debug.Log($"[DoorSystem] Used key: {heldKey}. Destroying key.");
                Destroy(heldItem);
            }
        }

        //toggle open/close
        isOpen = !isOpen;

        Vector3 toPlayer = (player.transform.position - transform.position).normalized;
        float side = Vector3.Dot(transform.right, toPlayer);
        float front = Vector3.Dot(initialForward, toPlayer);

        float angle = 0f;

        if (front > 0)
            angle = (side > 0) ? openAngle : -openAngle;
        else
            angle = (side > 0) ? -openAngle : openAngle;

        Quaternion targetRotation = isOpen
            ? Quaternion.Euler(0, angle, 0) * Quaternion.identity
            : Quaternion.identity;

        StopAllCoroutines();
        StartCoroutine(RotateDoor(targetRotation));
    }

    KeyType GetHeldKeyType(GameObject item)
    {
        if (item == null) return KeyType.None;

        DoorKey key = item.GetComponent<DoorKey>();
        return key != null ? key.keyType : KeyType.None;
    }*/

    private IEnumerator RotateDoor(Quaternion targetRotation)
    {
        while (Quaternion.Angle(hinge.rotation, targetRotation) > 0.1f)
        {
            hinge.rotation = Quaternion.Slerp(hinge.rotation, targetRotation, Time.deltaTime * openSpeed);
            yield return null;
        }

        hinge.rotation = targetRotation;
    }

    private void shakeDoor()
    {
        StartCoroutine(cor());

        IEnumerator cor()
        {
            Debug.Log("[DoorSystem] Already shaking, skipping new shake.");

            if (isShaking) yield break;

            Debug.Log("[DoorSystem] Starting shake!");

            isShaking = true;
            float baseY = transform.localEulerAngles.y;

            Quaternion originalRotation = baseRotation;

            while (elapsed < shakeDuration)
            {
                float shakeOffset = Mathf.Sin(elapsed * shakeSpeed) * shakeAmount;
                float currentY = baseY + shakeOffset;
                transform.localRotation = Quaternion.Euler(0, currentY, 0);

                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.rotation = originalRotation;
            isShaking = false;
            Debug.Log("[DoorSystem] Shake finished!");
        }
    }

    private bool isUnlocked = false;

    private void open()
    {
        isOpen = true;
        isUnlocked = true;
        // Open based on player's position
        OpenDoor_Internal(Player.Instance.transform.position);

        //your old function, I just copy patse to here
        void OpenDoor_Internal(Vector3 playerPos)
        {
            // Determine which side the player is on
            Vector3 toPlayer = (Player.Instance.transform.position - transform.position).normalized;
            float dot = Vector3.Dot(hinge.right, toPlayer); // Check side

            float angle = (dot > 0) ? -openAngle : openAngle;

            //  Rotate from original baseRotation, not current rotation
            Quaternion targetRotation = Quaternion.Euler(0, angle, 0) * baseRotation;
            StopAllCoroutines();
            StartCoroutine(RotateDoor(targetRotation));

            Debug.Log("[DoorSystem] Door opened to angle: " + angle);

            isOpen = true;
            //Debug.Log("[DoorSystem] Door opened to angle: " + angle);
        }
    }

    private bool checkOpen()
    {
        //can check if door is unlocked, open freely
        if (isUnlocked) return true;
        if (requiredKey == KeyType.None) return true;
        else
        {
            var item = Player.Instance.InventorySystem.CurrentHeld;
            if (item != null && item.GameObject?.GetComponent<DoorKey>() is DoorKey doorKey)
            {
                if (requiredKey == doorKey.keyType)
                {
                    //spend key succes
                    Player.Instance.InventorySystem.Remove(item);
                    Player.Instance.PickItemBehavior.UpdateEquipment();

                    //destroy key if not need anymore, else just hide it. depend what you guys want
                    Destroy(item.GameObject);
                    return true;
                }
            }

            return false;
        }
    }

    private void closeDoor()
    {
        isOpen = false;
        Debug.Log("[DoorSystem] Closing door.");
        StopAllCoroutines();
        StartCoroutine(RotateDoor(baseRotation));
    }

    public void Interact()
    {
        if (!isOpen)
        {
            if (checkOpen()) open();
            else shakeDoor();
        }
        //cua dang mo
        else
        {
            closeDoor();
        }
    }
}