using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    [SerializeField] private int squareSize = 4;
    [SerializeField] private GameObject squarePrefab;
    [SerializeField] private Dictionary<string, Square> squareGrid = new Dictionary<string, Square>();
    [SerializeField] private Material hoverMaterial;
    [SerializeField] private Material selectableMaterial;
    [SerializeField] private GameObject pawnPrefab;
    [SerializeField] private GameObject rookPrefab;

    // Start is called before the first frame update
    void Start()
    {
        EventsManager.HoverPiece += SelectPiece;
        EventsManager.HoverElement += Hover;
        EventsManager.HoverNothing += HoverNothing;
        
        InitializeGrid();
        SpawnPieces();
    }

    private void OnDestroy() 
    {
        EventsManager.HoverPiece -= SelectPiece;
        EventsManager.HoverElement -= Hover;
        EventsManager.HoverNothing -= HoverNothing;
    }

    private void Update()
    {
       
    }

    void InitializeGrid()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int e = 0; e < 8; e++)
            {
                Vector3 pos = new Vector3(i * squareSize, squarePrefab.transform.position.y, e * squareSize);
                var sqGO = Instantiate(squarePrefab, this.transform);
                sqGO.transform.localPosition = pos;
                sqGO.name = GetAlphabetLetter(e) + (i + 1).ToString();
                sqGO.AddComponent<Square>();
                sqGO.GetComponent<Square>().column = GetAlphabetLetter(e);
                sqGO.GetComponent<Square>().row = i + 1;
                squareGrid.Add(sqGO.name, sqGO.GetComponent<Square>());
            }
        }
    }

    void SpawnPieces()
    {
        //Starting with Pawns
        for(int p = 0; p < 8; p++) 
        {
            var pawn = Instantiate(pawnPrefab);
            pawn.transform.parent = squareGrid[GetAlphabetLetter(p) + "2"].transform;
            pawn.transform.localPosition = Vector3.zero;
            pawn.transform.Rotate(new Vector3(0, 0, 90));

            pawn.AddComponent<Pawn>();
            pawn.GetComponent<Pawn>().UpdateSquareInformation(squareGrid[GetAlphabetLetter(p) + "2"]);
        }

        var rook = Instantiate(rookPrefab);
        rook.transform.parent = squareGrid[GetAlphabetLetter(0) + "1"].transform;
        rook.transform.Rotate(new Vector3(0, 0, 90));

        rook.AddComponent<Rook>();
        rook.transform.localPosition = new Vector3(0, rook.GetComponent<Rook>().initialY, 0);
        rook.GetComponent<Rook>().UpdateSquareInformation(squareGrid[GetAlphabetLetter(0) + "1"]);
    }

    void SelectPiece(object[] obj)
    {
        Piece piece = (Piece)obj[0];

        GameManager.Get().possibleSquares.Clear();

        foreach(var square in squareGrid)
        {
            square.Value.transform.GetComponent<MeshRenderer>().material = selectableMaterial;
            square.Value.transform.GetComponent<MeshRenderer>().enabled = piece.LegalMovement(square.Value);
            if(piece.LegalMovement(square.Value))
            {
                GameManager.Get().possibleSquares.Add(square.Value);
            }
        }
    }

    void HoverNothing(object[] obj)
    {
        foreach(var square in squareGrid)
        {
            square.Value.transform.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    void Hover(object[] obj)
    {
        foreach(var square in squareGrid)
        {
            square.Value.transform.GetComponent<MeshRenderer>().enabled = false;
        }
        
        squareGrid[(string)obj[1]].transform.GetComponent<MeshRenderer>().material = hoverMaterial;
        squareGrid[(string)obj[1]].transform.GetComponent<MeshRenderer>().enabled = true;
    }

    char GetAlphabetLetter(int pos)
    {
        string alphabet = "abcdefghijklmnopqrstuvwxyz";
        return alphabet[pos];
    }
}
