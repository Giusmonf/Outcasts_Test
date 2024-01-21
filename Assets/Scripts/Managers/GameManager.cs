using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool hasKey;

    [SerializeField] private GameObject keyInventoryImage;

    [SerializeField] private GameObject doorInteractionColliderWithKey;
    [SerializeField] private GameObject doorInteractionColliderWithoutKey;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {

        }
    }

    

    public void GameOver()
    {
        Debug.Log("Hai perso!");
    }

    public void GameWon()
    {
        Debug.Log("Hai vinto!");
    }

    public void SwitchDoorColliders()
    {
        doorInteractionColliderWithKey.SetActive(true);
        doorInteractionColliderWithoutKey.SetActive(false);
    }

    public void ShowKeyInInventory()
    {
        keyInventoryImage.SetActive(true);
    }

    public void HideKeyInInventory()
    {

    }

    #region SETTERS/GETTERS

    public void SetHasKey(bool value)
    {
        hasKey = value;
    }

    public bool GetHasKey()
    {
        return hasKey;
    }

    #endregion
}
