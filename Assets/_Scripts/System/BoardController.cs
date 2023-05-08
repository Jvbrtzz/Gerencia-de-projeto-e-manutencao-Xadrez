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
    [SerializeField] private GameObject knightPrefab;
    [SerializeField] private GameObject queenPrefab;
    [SerializeField] private GameObject kingPrefab;
    [SerializeField] private GameObject bishopPrefab;

    // Start is called before the first frame update
    void Start()
    {
        EventsManager.HoverPiece += SelectPiece;
        EventsManager.HoverElement += Hover;
        EventsManager.HoverNothing += HoverNothing;
        
        InitializeGrid();
        SpawnPlayerPieces();
        SpawnOpponentPieces();
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

    void SpawnPlayerPieces()
    {
        SpawnPawns("2", true);
        SpawnRook("1", 0, true);
        SpawnRook("1", 7, true);
        SpawnKnight("1", 1, true);
        SpawnKnight("1", 6, true);
        SpawnBishop("1", 2, true);
        SpawnBishop("1", 5, true);
        SpawnKing("1", 3, true);
        SpawnQueen("1", 4, true);
    }

    void SpawnOpponentPieces()
    {
        SpawnPawns("7", false);
        SpawnRook("8", 0, false);
        SpawnRook("8", 7, false);
        SpawnKnight("8", 1, false);
        SpawnKnight("8", 6, false);
        SpawnBishop("8", 2, false);
        SpawnBishop("8", 5, false);
        SpawnKing("8", 3, false);
        SpawnQueen("8", 4, false);
    }

    void SpawnQueen(string row, int column, bool playerOwned)
    {
        var queen = Instantiate(queenPrefab);
        queen.transform.parent = squareGrid[GetAlphabetLetter(column) + row].transform;
        queen.transform.Rotate(new Vector3(0, 0, playerOwned ? 90 : 270));

        queen.AddComponent<Queen>();
        queen.transform.localPosition = new Vector3(0, queen.GetComponent<Queen>().initialY, 0);
        queen.GetComponent<Queen>().UpdateSquareInformation(squareGrid[GetAlphabetLetter(column) + row]);
        queen.GetComponent<Queen>().SetOwnership(playerOwned);
    }

    void SpawnKing(string row, int column, bool playerOwned)
    {
        var king = Instantiate(kingPrefab);
        king.transform.parent = squareGrid[GetAlphabetLetter(column) + row].transform;
        king.transform.Rotate(new Vector3(0, 0, playerOwned ? 90 : 270));

        king.AddComponent<King>();
        king.transform.localPosition = new Vector3(0, king.GetComponent<King>().initialY, 0);
        king.GetComponent<King>().UpdateSquareInformation(squareGrid[GetAlphabetLetter(column) + row]);
        king.GetComponent<King>().SetOwnership(playerOwned);
    }

    void SpawnBishop(string row, int column, bool playerOwned)
    {
        var bishop = Instantiate(bishopPrefab);
        bishop.transform.parent = squareGrid[GetAlphabetLetter(column) + row].transform;
        bishop.transform.Rotate(new Vector3(0, 0, playerOwned ? 90 : 270));

        bishop.AddComponent<Bishop>();
        bishop.transform.localPosition = new Vector3(0, bishop.GetComponent<Bishop>().initialY, 0);
        bishop.GetComponent<Bishop>().UpdateSquareInformation(squareGrid[GetAlphabetLetter(column) + row]);
        bishop.GetComponent<Bishop>().SetOwnership(playerOwned);
    }

    void SpawnKnight(string row, int column, bool playerOwned)
    {
        var knight = Instantiate(knightPrefab);
        knight.transform.parent = squareGrid[GetAlphabetLetter(column) + row].transform;
        knight.transform.Rotate(new Vector3(0, 0, playerOwned ? 90 : 270));

        knight.AddComponent<Knight>();
        knight.transform.localPosition = new Vector3(0, knight.GetComponent<Knight>().initialY, 0);
        knight.GetComponent<Knight>().UpdateSquareInformation(squareGrid[GetAlphabetLetter(column) + row]);
        knight.GetComponent<Knight>().SetOwnership(playerOwned);
    }

    void SpawnRook(string row, int column, bool playerOwned)
    {
        var rook = Instantiate(rookPrefab);
        rook.transform.parent = squareGrid[GetAlphabetLetter(column) + row].transform;
        rook.transform.Rotate(new Vector3(0, 0, playerOwned ? 90 : 270));

        rook.AddComponent<Rook>();
        rook.transform.localPosition = new Vector3(0, rook.GetComponent<Rook>().initialY, 0);
        rook.GetComponent<Rook>().UpdateSquareInformation(squareGrid[GetAlphabetLetter(column) + row]);
        rook.GetComponent<Rook>().SetOwnership(playerOwned);
    }

    void SpawnPawns(string row, bool playerOwned)
    {
        //Starting with Pawns
        for (int p = 0; p < 8; p++)
        {
            var pawn = Instantiate(pawnPrefab);
            pawn.transform.parent = squareGrid[GetAlphabetLetter(p) + row].transform;
            pawn.transform.localPosition = Vector3.zero;
            pawn.transform.Rotate(new Vector3(0, 0, playerOwned ? 90 : 270));

            pawn.AddComponent<Pawn>();
            pawn.GetComponent<Pawn>().UpdateSquareInformation(squareGrid[GetAlphabetLetter(p) + row]);
            pawn.GetComponent<Pawn>().SetOwnership(playerOwned);
        }
    }

    void SelectPiece(object[] obj)
    {
        Piece piece = (Piece)obj[0];

        GameManager.Get().possibleSquares.Clear();
        List<Square> possibleSquares = new List<Square>();

        foreach(var sq in squareGrid)
        {
            possibleSquares.Add(sq.Value);
        }

        foreach(var movement in piece.LegalMovement(possibleSquares))
        {
            movement.Key.transform.GetComponent<MeshRenderer>().material = selectableMaterial;

            if(movement.Key.currentPiece != null && movement.Key.currentPiece.isPlayerOwned != piece.isPlayerOwned)
            {
                movement.Key.transform.GetComponent<MeshRenderer>().material.color = Color.red;
                movement.Key.transform.GetComponent<MeshRenderer>().enabled = true;
                GameManager.Get().possibleSquares.Add(movement.Key);
            }
            else
            {
                movement.Key.transform.GetComponent<MeshRenderer>().enabled = movement.Value;

                if(movement.Value)
                {
                    GameManager.Get().possibleSquares.Add(movement.Key);
                }
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
