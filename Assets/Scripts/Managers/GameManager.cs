using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Inventory Properties")]

    [SerializeField] private bool hasKey;
    [SerializeField] private GameObject keyInventoryImage;
    [SerializeField] private GameObject doorInteractionColliderWithKey;
    [SerializeField] private GameObject doorInteractionColliderWithoutKey;

    [Header("Canvas Properties")]
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject youLostText;
    [SerializeField] private GameObject youWinText;
    [SerializeField] private GameObject continueButton;

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
            PauseGame();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Unpause()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1.0f;
    }
    
    public void GameOver()
    {
        youLostText.SetActive(true);
        continueButton.SetActive(false);
        PauseGame();
    }

    public void GameWon()
    {
        youWinText.SetActive(true);
        continueButton.SetActive(false);
        PauseGame();
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
