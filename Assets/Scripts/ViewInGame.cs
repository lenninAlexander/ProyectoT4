using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ViewInGame : MonoBehaviour
{
    public Text coinsLabel;
    public Text pointsLabel;

    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame || GameManager.sharedInstance.currentGameState == GameState.gameOver) 
        {
            this.coinsLabel.text = GameManager.sharedInstance.CollectedMoney.ToString();
            this.pointsLabel.text = "PUNTOS: " + GameManager.sharedInstance.CollectedPoints.ToString();
        }
    }
}
