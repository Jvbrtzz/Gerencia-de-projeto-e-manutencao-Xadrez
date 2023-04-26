using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasController : MonoBehaviour
{
    public TextMeshProUGUI turnCounter;
    public TextMeshProUGUI hoverInfo;

    // Start is called before the first frame update
    void Start()
    {
        EventsManager.HoverElement += Hover; 
    }

    private void OnDestroy() 
    {
        EventsManager.HoverElement -= Hover; 
    }

    void Hover(object[] obj)
    {
        hoverInfo.text = (string)obj[0] + " [" + ((string)obj[1]).ToUpper() + "]";
    }

    // Update is called once per frame
    void Update()
    {
        turnCounter.text = GameManager.Get().isPlayersTurn ? CommonData.Text.playerTurn : CommonData.Text.opponentTurn;
    }
}
