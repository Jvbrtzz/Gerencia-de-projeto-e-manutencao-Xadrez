using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    [SerializeField] private int squareSize = 4;
    [SerializeField] private GameObject squarePrefab;
    [SerializeField] private Dictionary<string, Square> squareGrid = new Dictionary<string, Square>();

    [SerializeField] private GameObject pawnPrefab;

    // Start is called before the first frame update
    void Start()
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

        SpawnPieces();
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

            foreach(var square in squareGrid)
            {
                square.Value.transform.GetComponent<MeshRenderer>().enabled = pawn.GetComponent<Piece>().LegalMovement(square.Value);
            }
            return;
        }

        
    }

    char GetAlphabetLetter(int pos)
    {
        string alphabet = "abcdefghijklmnopqrstuvwxyz";
        return alphabet[pos];
    }
}
