using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasController : MonoBehaviour
{
    public GameObject selectingMenu;
    public TextMeshProUGUI turnCounter;
    public TextMeshProUGUI hoverInfo;
    public GameObject gameOver;
    public TextMeshProUGUI gameOverText;
    public Button restartGame;
    public Button closeGame;

    // Start is called before the first frame update
    void Start()
    {
        EventsManager.HoverElement += Hover;
        EventsManager.HoverNothing += HoverNothing;
        EventsManager.EndGame += EndGame;
    }

    private void OnDestroy()
    {
        EventsManager.HoverElement -= Hover;
        EventsManager.HoverNothing -= HoverNothing;
        EventsManager.EndGame -= EndGame;
    }

    void Hover(object[] obj)
    {
        hoverInfo.text = (string)obj[0] + " [" + ((string)obj[1]).ToUpper() + "]";
    }

    void EndGame(object[] obj)
    {
        gameOver.SetActive(true);
        gameOverText.text = (bool)obj[0] ? CommonData.Text.gameOverVictory : CommonData.Text.gameOverLose;

        restartGame.onClick?.AddListener(() =>
        {
            GameManager.Get().squareGrid.Clear();
            GameManager.Get().isGameOver = false;
            GameManager.Get().selectedPiece = null;
            GameManager.Get().possibleSquares.Clear();
            GameManager.Get().playerKing = null;
            GameManager.Get().opponentKing = null;
            GameManager.Get().isPlayersTurn = true;

            UnityEngine.SceneManagement.SceneManager.LoadScene("Chess");
        });

        closeGame.onClick?.AddListener(() =>
        {
            Application.Quit();
        });
    }

    void HoverNothing(object[] obj)
    {
        hoverInfo.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        selectingMenu.SetActive(GameManager.Get().selectedPiece != null);
        turnCounter.text = GameManager.Get().isPlayersTurn ? CommonData.Text.playerTurn : CommonData.Text.opponentTurn;
    }
}
