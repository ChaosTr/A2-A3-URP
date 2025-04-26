using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay: MonoBehaviour
{
    [SerializeField] private InventoryItemDisplay _prefab;
    [SerializeField] Transform _content;

    private List<InventoryItemDisplay> _list;
    InventorySystem System => Player.Instance.InventorySystem;

    public bool IsShowingInventory { get; private set; }

    private int _currentView = 0;
    private bool _didInit = false;

    private void Start()
    {
        init();
        HideInventory();
    }

    public void ViewInventory()
    {
        init();
        gameObject.SetActive(true);
        IsShowingInventory = true;

        var listItemInInventory = Player.Instance.InventorySystem.Storage;
        for (int i = 0; i < listItemInInventory.Count; i++)
        {
            _list[i].SetSelected(false);
            _list[i].Setup(listItemInInventory[i]);
        }
        _currentView = 0;
        _list[_currentView].SetSelected(true);
    }

    public void HideInventory()
    {
        IsShowingInventory = false;
        gameObject.SetActive(false);
    }

    public void CycleViewItem(int index)
    {
        _list[_currentView].SetSelected(false);
        _currentView += index;
        _currentView /= System.Max;
        _list[_currentView].SetSelected(true);
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
