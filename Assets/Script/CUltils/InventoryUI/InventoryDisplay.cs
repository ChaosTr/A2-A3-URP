using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay: MonoBehaviour
{
    [SerializeField] private InventoryItemDisplay _prefab;
    [SerializeField] Transform _content;
    [SerializeField] private TMPro.TextMeshProUGUI _descriptionTxt;


    private List<InventoryItemDisplay> _list;
    InventorySystem System => Player.Instance.InventorySystem;

    public bool IsShowingInventory { get; private set; }

    private bool _didInit = false;

    private void Start()
    {
        init();
        HideInventory();
        System.OnInventoryChanged += updateInternal;
    }

    private void OnDestroy()
    {
        System.OnInventoryChanged -= updateInternal;
    }

    public void ViewInventory()
    {
        init();
        gameObject.SetActive(true);
        IsShowingInventory = true;

        updateInternal();
    }

    private void a()
    {
        var obj = System.CurrentHeld.GameObject;
        obj.SetActive(true);
    }

    private void updateInternal()
    {
        var listItemInInventory = System.Storage;
        for (int i = 0; i < listItemInInventory.Count; i++)
        {
            _list[i].SetSelected(false);
            _list[i].Setup(listItemInInventory[i]);
        }
        if (System.CurrentHeld != null)
        {
            int index = listItemInInventory.IndexOf(System.CurrentHeld);
            _list[index].SetSelected(true);
            
            //new
            var name = System.CurrentHeld.GameObject.GetComponent<ExamineSystem.ExaminableItem>().ItemDescription;
            _descriptionTxt.text = name;
        }

        //tat gameObject
    }

    public void HideInventory()
    {
        IsShowingInventory = false;
        gameObject.SetActive(false);

        //
    }

    private void init()
    {
        if (_didInit) return;

        _list = new List<InventoryItemDisplay>();
        for (int i = 0; i < System.Max; i++)
        {
            var item = Instantiate(_prefab, _content);
            _list.Add(item);
        }
        _didInit = true;
    }
}
