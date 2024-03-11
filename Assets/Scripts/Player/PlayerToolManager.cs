using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToolManager : MonoBehaviour
{
    [SerializeField]
    private ToolManager[] playerTools;

    private int toolIndex;

    private void Awake() {
        toolIndex = 0;
        playerTools[toolIndex].gameObject.SetActive(true);
    }

    private void Update() {        
        ChangeTool();
    }

    public void ActivateTool(int toolIdx) {
        playerTools[toolIndex].ActivateTool(toolIdx);
    }

    private void ChangeTool() {
        if (Input.GetKeyDown(KeyCode.T)) {
            playerTools[toolIndex].gameObject.SetActive(false);
            toolIndex = (toolIndex + 1) % playerTools.Length;
            playerTools[toolIndex].gameObject.SetActive(true);
        }
    }
}
