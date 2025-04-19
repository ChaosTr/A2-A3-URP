using UnityEngine;

public class ActionDecision
{
    public ItemPickup ItemPickup;

    public ActionDecision()
    { 
    
    }

    public void OnMouseLeftClickHit(Collider collider)
    {
        if (collider == null) return;
        if (collider.GetComponent<Pickable>() is Pickable pickable)
        {
            ItemPickup.PickupItem(pickable.gameObject);
        }
        else if(collider.GetComponent<IInteract>() is IInteract item)
        {
            item.Interact();


        }
    }

    public void OnMouseRightClick()
    {
        ItemPickup.DropItem();
    }
}