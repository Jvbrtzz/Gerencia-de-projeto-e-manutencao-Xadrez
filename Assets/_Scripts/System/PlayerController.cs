using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    [SerializeField] private bool selectingPiece;
    [SerializeField] private Material ghostMaterial;
    [SerializeField] private Outline outlined;
    private GameObject ghostPiece;
    
    public void OnPieceHovered(Piece piece)
    {
        if(GameManager.Get().isPlayersTurn)
        {
            EventsManager.Get().Call_HoverPiece(piece);

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                selectingPiece = true;

                if(piece.transform.gameObject.GetComponent<Outline>() == null)
                {
                    outlined = piece.transform.gameObject.AddComponent<Outline>();
                    outlined.OutlineMode = Outline.Mode.OutlineVisible;
                    outlined.OutlineColor = Color.green;
                    outlined.OutlineWidth = 4;
                }

                GameManager.Get().selectedPiece = piece;
            }
        }
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopSelection();
        }

        if (selectingPiece && GameManager.Get().selectedPiece != null)
        {
            SelectMovablePiece();
            return;
        }
        Target();
    }
    public void StopSelection()
    {
        if (ghostPiece != null)
            Destroy(ghostPiece);

        if(outlined != null)
        {
            Destroy(outlined);
        }

        GameManager.Get().selectedPiece = null;
        selectingPiece = true;
    }

    void SelectMovablePiece()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] hits = Physics.RaycastAll(ray);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if (hit.collider.GetComponent<Square>() != null)
            {
                if (ghostPiece != null)
                {
                    Destroy(ghostPiece);
                }

                var square = hit.collider.GetComponent<Square>();

                if(GameManager.Get().possibleSquares.Contains(square))
                {
                    ghostPiece = Instantiate(GameManager.Get().selectedPiece.transform.gameObject, square.transform);
                    ghostPiece.transform.localPosition = new Vector3(0, GameManager.Get().selectedPiece.GetComponent<Piece>().initialY, 0);

                    var mats = ghostPiece.GetComponent<MeshRenderer>().materials;
                    var newGhostMaterial = new Material[mats.Length];
                    for(int e = 0; e < mats.Length; e++)
                    {
                        newGhostMaterial[e] = ghostMaterial;
                    }
                    foreach(var child in ghostPiece.GetComponentsInChildren<MeshRenderer>())
                    {
                        child.materials = newGhostMaterial;
                    }
                    ghostPiece.GetComponent<MeshRenderer>().materials = newGhostMaterial;
                }
            }
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

        if(hits.Length == 0)
        {
            EventsManager.Get().Call_HoverNothing();
        }
    }
}