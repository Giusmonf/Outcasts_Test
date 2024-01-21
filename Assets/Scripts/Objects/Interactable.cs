using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private CanvasGroup inputPromptCanvas;
    [SerializeField] private InteractableObject interactableObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerInteraction>().SetCanInteract(true);
            other.GetComponent<PlayerInteraction>().SetInteractableItem(interactableObject);

            ShowCanvas();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerInteraction>().SetCanInteract(false);
            other.GetComponent<PlayerInteraction>().SetInteractableItem(null);

            HideCanvas();
        }
    }

    private void ShowCanvas()
    {
        inputPromptCanvas.alpha = 1;
    }

    private void HideCanvas()
    {
        inputPromptCanvas.alpha = 0;
    }
}

[System.Serializable]
public class InteractableObject
{
    public PlayerInteraction.InteractableItemType interactableItemType;
    public GameObject gameObject;
}
