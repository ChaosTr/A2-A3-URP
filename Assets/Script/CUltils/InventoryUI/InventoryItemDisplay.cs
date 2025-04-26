using UnityEngine;
using UnityEngine.UI;

public class InventoryItemDisplay : MonoBehaviour
{
    [SerializeField] private Graphic _selected;
    [SerializeField] private TMPro.TextMeshProUGUI _name;

    private InventorySystem.Item _item;

    public void SetSelected(bool value)
    {
        _selected.gameObject.SetActive(_item == null ? false : value);
    }

    public void Setup(InventorySystem.Item item)
    {
        _item = item;
        _name.text = _item == null ? "" : _item.GameObject.name;
    }
}