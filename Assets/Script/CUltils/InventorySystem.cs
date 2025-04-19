using System;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem
{
    public List<Item> Storage = new List<Item>();
    public Item CurrentHeld;

    public int Max = 4;

    public void Add(GameObject obj)
    {
        if (Storage.Count >= Max) return;

        Storage.Add(new Item
        {
            obj = obj
        });
    }

    public void Remove(Item item)
    {
        Storage.Remove(item);
    }

    public class Item
    {
        public GameObject obj;
        public Sprite Sprite;
    }

    public void Equip(Item item)
    {
        if (CurrentHeld != null)
        {
            CurrentHeld = item;
        }
    }

    public void Equip(int index)
    {
        if (Storage.Count < index)
        {
            CurrentHeld = null;
        }
        else CurrentHeld = Storage[index];
    }
}
