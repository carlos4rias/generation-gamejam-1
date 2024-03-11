using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI keepAlive;
    public TextMeshProUGUI alive;
    void Start()
    {
        infoText.text = $"Puntos: {GameLogic.Instance.points}";   
        keepAlive.text = $"Arboles que necesitas mantener vivos {GameLogic.Instance.howManyTrees}";
        alive.text = $"Arboles que actualmente estan vivos {GameLogic.Instance.liveTrees}";                        
    }

    // Update is called once per frame
    void Update()
    {
        infoText.text = $"Puntos: {GameLogic.Instance.points}";   
        keepAlive.text = $"Arboles que necesitas mantener vivos {GameLogic.Instance.howManyTrees}";
        alive.text = $"Arboles que actualmente estan vivos {GameLogic.Instance.liveTrees}";
    }
}
