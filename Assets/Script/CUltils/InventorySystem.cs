using System;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem
{
    public List<Item> Storage = new List<Item>();
    public Item CurrentHeld;
    public Item NewAdd;

    public int Max = 4;
    public event Action OnInventoryChanged;

    public InventorySystem()
    {
        //fill all the slot at start ONCE   
        Storage = new List<Item>(Max);
        for (int i = 0; i < Max; i++)
        {
            Storage.Add(null);
        }
    }

    public bool Add(GameObject obj)
    {
        Debug.Log($"[InventorySystem] Trying to add: {obj.name}");

        bool result = false;
        for (int i = 0; i < Max; i++)
        {
            if (Storage[i] == null)
            {
                Storage[i] = new Item { GameObject = obj };
                NewAdd = Storage[i];
                result = true;
                break;
            }
        }

        OnInventoryChanged?.Invoke();
        return result;
    }

    public void HideNewAdd()
    {
        if(NewAdd!=null)
        {
            NewAdd.GameObject.SetActive(false);
            NewAdd = null;
        }
    }

    public void Remove(Item item)
    {
        //if (CurrentHeld == item) CurrentHeld = null;

        for (int i = 0; i < Storage.Count; i++)
        {
            if (Storage[i] == item)
            {
                Storage[i] = null;
                break;
            }
        }

        if (CurrentHeld == item)
            CurrentHeld = null;
        Storage.Remove(item);
        OnInventoryChanged?.Invoke();
    }

    public class Item
    {
        public GameObject GameObject;
        //amount or something, what info you want to store here
        //but in this sample I wil make it simple
    }

    public void Equip(int index)
    {
        if (index < 0 || index >= Storage.Count || Storage[index] == null)
        {
            
            CurrentHeld = null;
            return;
        }

        CurrentHeld = Storage[index];
        OnInventoryChanged?.Invoke();
    }
}
