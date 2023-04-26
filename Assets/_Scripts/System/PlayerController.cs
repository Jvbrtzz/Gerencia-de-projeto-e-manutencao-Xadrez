using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    public void OnPieceHovered(Piece piece)
    {
        if(GameManager.Get().isPlayersTurn)
        {
            EventsManager.Get().Call_HoverPiece(piece);
        }
    }

    private void Update() 
    {
       Target();
    }

    void Target()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] hits = Physics.RaycastAll(ray);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if (hit.collider.GetComponent<Piece>() != null)
            {
                var piece = hit.collider.GetComponent<Piece>();
                EventsManager.Get().Call_HoverElement(
                    piece.pieceName,
                    piece.currentSquare.column + piece.currentSquare.row.ToString()
                );
                
                OnPieceHovered(piece);
                break;
            }
            else if(hit.collider.GetComponent<Square>() != null)
            {
                var square = hit.collider.GetComponent<Square>();
                if(square.currentPiece == null)
                {
                    EventsManager.Get().Call_HoverElement(
                        "",
                        square.column + square.row.ToString()
                    );
                }
            }
        }
    }
}