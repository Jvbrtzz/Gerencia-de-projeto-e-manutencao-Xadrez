using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    public void OnPieceClicked(Piece piece)
    {
        if(GameManager.Get().isPlayersTurn)
        {
            EventsManager.Get().Call_SelectPiece(piece);
        }
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Target();
        }
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
                OnPieceClicked(hit.collider.GetComponent<Piece>());
                break;
            }
        }
    }
}