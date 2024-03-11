using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleGameUI : MonoBehaviour
{
    public GameObject PanelInstructionsPanel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowInstructions() {
        PanelInstructionsPanel.SetActive(true);
    }

    
    public void HideInstructions() {
        PanelInstructionsPanel.SetActive(false);
    }
}
