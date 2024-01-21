using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private bool canInteract;
    [SerializeField] private InteractableObject currentInteractableObject;

    public enum InteractableItemType
    {
        NONE,
        KEY,
        DOOR
    }

    private void OnInteract()
    {
        if (canInteract)
        {
            switch(currentInteractableObject.interactableItemType)
            {
                case InteractableItemType.KEY:

                    GameManager.Instance.SetHasKey(true);

                    GameManager.Instance.ShowKeyInInventory();
                    GameManager.Instance.SwitchDoorColliders();

                    Destroy(currentInteractableObject.gameObject);

                    currentInteractableObject = null;

                    break; 

                case InteractableItemType.DOOR:

                    if (GameManager.Instance.GetHasKey())
                    {
                        GameManager.Instance.GameWon();
                    }

                    break;

                default:

                    break;
            }
        }
    }

    public bool GetInteract()
    {
        return canInteract;
    }

    public void SetCanInteract(bool value)
    {
        canInteract = value;
    }

    public InteractableObject GetCurrentInteractableObject()
    {
        return currentInteractableObject;
    }

    public void SetInteractableItem(InteractableObject value)
    {
        currentInteractableObject = value;
    }
}
