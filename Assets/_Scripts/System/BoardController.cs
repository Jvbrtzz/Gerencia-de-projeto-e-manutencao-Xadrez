using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    [SerializeField] private int squareSize = 4;
    [SerializeField] private GameObject squarePrefab;
    [SerializeField] private Dictionary<string, Square> squareGrid = new Dictionary<string, Square>();

    [SerializeField] private GameObject pawnPrefab;

    // Start is called before the first frame update
    void Start()
    {
        EventsManager.SelectPiece += SelectPiece;
        
        InitializeGrid();
        SpawnPieces();
    }

    private void OnDestroy() 
    {
        EventsManager.SelectPiece -= SelectPiece;
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
                squareGrid.Add(sqGO.name, new Square(sqGO.transform, GetAlphabetLetter(e), i + 1));
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
            pawn.GetComponent<Pawn>().currentSquare = squareGrid[GetAlphabetLetter(p) + "2"];
        }
    }

    void SelectPiece(object[] obj)
    {
        Piece piece = (Piece)obj[0];

        foreach(var square in squareGrid)
        {
            square.Value.transform.GetComponent<MeshRenderer>().enabled = piece.LegalMovement(square.Value);
        }
    }

    char GetAlphabetLetter(int pos)
    {
        string alphabet = "abcdefghijklmnopqrstuvwxyz";
        return alphabet[pos];
    }
}
