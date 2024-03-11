using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tools;
    private int currentTool;

    void DeactivateAllTools() {
        for (int i = 0; i < tools.Length; i++)
            tools[i].SetActive(false);
    }

    public void ActivateTool(int newToolIndex) {
        tools[currentTool].SetActive(false);
        tools[newToolIndex].SetActive(true);
        currentTool = newToolIndex;
    }
}
