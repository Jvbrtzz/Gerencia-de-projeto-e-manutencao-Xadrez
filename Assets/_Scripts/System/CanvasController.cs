using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasController : MonoBehaviour
{
    public GameObject selectingMenu;
    public TextMeshProUGUI turnCounter;
    public TextMeshProUGUI hoverInfo;

    // Start is called before the first frame update
    void Start()
    {
        EventsManager.HoverElement += Hover; 
        EventsManager.HoverNothing += HoverNothing; 
    }

    private void OnDestroy() 
    {
        EventsManager.HoverElement -= Hover; 
        EventsManager.HoverNothing -= HoverNothing; 
    }

    void Hover(object[] obj)
    {
        hoverInfo.text = (string)obj[0] + " [" + ((string)obj[1]).ToUpper() + "]";
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
