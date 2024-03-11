using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleGameUI : MonoBehaviour
{

    public static HandleGameUI Instance { get; private set; }
    public GameObject PanelInstructionsPanel;
    public GameObject WinPanel;
    public GameObject LosePanel;
    public GameObject Helper;
    public TextMeshProUGUI points;

    public TextMeshProUGUI information;


    private void Awake() {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameLogic.Instance.gameState == 0)
            LosePanel.SetActive(true);
        if (GameLogic.Instance.gameState == 1) {
            WinPanel.SetActive(true);    
            points.text = $"Obtuviste {GameLogic.Instance.points} Puntos!!!";
        }
        
    }

    public void ShowInstructions() {        
        PanelInstructionsPanel.SetActive(true);
    }

    public void showHelper(string message) {
        information.text = message;
        Helper.SetActive(true);
    }

    public void hideHelper() {
        Helper.SetActive(false);
    }
    
    public void HideInstructions() {
        PanelInstructionsPanel.SetActive(false);
    }

    public void RestartGame() {
        SceneManager.LoadScene(1);
    }
}
