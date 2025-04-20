using System;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem
{
    public List<Item> Storage = new List<Item>();
    public Item CurrentHeld;

    public int Max = 4;

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
        //Debug.Log(obj.name);
        //
        //if (Storage.Count >= Max)
        //{
        //    Debug.LogWarning("[InventorySystem] Inventory is full! Cannot add more items.");
        //    return false;
        //}
        //
        //Storage.Add(new Item
        //{
        //    GameObject = obj
        //    //amount or something you want to add to have more info
        //});
        //
        //return true;
        //
        //Debug.Log($"[InventorySystem] Added item: {obj.name}");

        Debug.Log($"[InventorySystem] Trying to add: {obj.name}");

        for (int i = 0; i < Max; i++)
        {
            if (Storage[i] == null)
            {
                Storage[i] = new Item { GameObject = obj };
                return true;
            }
        }
      
        return false;
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
    }

    public class Item
    {
        public GameObject GameObject;
        //amount or something, what info you want to store here
        //but in this sample I wil make it simple
    }

    public void Equip(Item item)
    {
        if (item != null)//CurrentHeld != null)
        {
            Debug.Log(item.GameObject.name);
            CurrentHeld = item;
        }
    }

    public void Equip(int index)
    {
        /*if (Storage.Count < index)
        {
            CurrentHeld = null;
            Debug.Log(CurrentHeld.GameObject.name);
        }
        else CurrentHeld = Storage[index];*/

        if (index < 0 || index >= Storage.Count || Storage[index] == null)
        {
            
            CurrentHeld = null;
            return;
        }

        CurrentHeld = Storage[index];
        

    }
}
