using System;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem
{
    public List<Item> Storage = new List<Item>();
    public Item CurrentHeld;

    public int Max = 4;

    public bool Add(GameObject obj)
    {
        Debug.Log(obj.name);

        if (Storage.Count >= Max)
        {
            Debug.LogWarning("[InventorySystem] Inventory is full! Cannot add more items.");
            return false;
        }

        Storage.Add(new Item
        {
            GameObject = obj
            //amount or something you want to add to have more info
        });

        return true;

        Debug.Log($"[InventorySystem] Added item: {obj.name}");
    }

    public void Remove(Item item)
    {
        if (CurrentHeld == item) CurrentHeld = null;
        Storage.Remove(item);
    }

    public class Item
    {
        public GameObject GameObject;
        //amount or something, what info you want to store here
        //but in this sample I wil make it simple
    }

    public void Equip(Item item)
    {
        if (CurrentHeld != null)
        {
            Debug.Log(item.GameObject.name);
            CurrentHeld = item;
        }
    }

    public void Equip(int index)
    {
        if (Storage.Count < index)
        {
            CurrentHeld = null;
            Debug.Log(CurrentHeld.GameObject.name);
        }
        else CurrentHeld = Storage[index];
    }
}
