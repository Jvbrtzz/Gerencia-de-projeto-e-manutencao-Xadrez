using System.Collections.Generic;
using UnityEngine.Events;

public class Rook : Piece
{
    public override string pieceName { get { return "Torre"; } }
    public override float initialY { get => -17.8f; }

    public override Dictionary<Square, bool> LegalMovement(List<Square> possibleMovementSquares)
    {
        Dictionary<Square, bool> legalMovement = new Dictionary<Square, bool>();
        legalMovement = CalculateMovement(legalMovement, possibleMovementSquares, true);
        legalMovement = CalculateMovement(legalMovement, possibleMovementSquares, false);

        return legalMovement;
    }

    Dictionary<Square, bool> CalculateMovement(Dictionary<Square, bool> legalMovement, List<Square> possibleMovementSquares, bool isRow)
    {
        var listOfSquares = isRow ? ListRowPieces(possibleMovementSquares, currentSquare.row) : ListColumnPieces(possibleMovementSquares, currentSquare.column);
        var startingPos = 0;

        var obstacle = false;

        for (int i = 0; i < listOfSquares.Count; i++)
        {
            if(listOfSquares[i].currentPiece == this)
            {
                startingPos = i;
            }
        }

        for(int forward = startingPos + 1; forward < 8; forward++)
        {
            if(listOfSquares[forward].currentPiece != null && listOfSquares[forward].name != currentSquare.name)
            {
                obstacle = true;

                if(listOfSquares[forward].currentPiece.isPlayerOwned != isPlayerOwned)
                {
                    legalMovement.Add(listOfSquares[forward], obstacle);
                    continue;
                }
            }

            legalMovement.Add(listOfSquares[forward], !obstacle);
        }

        obstacle = false;

        for(int backwards = startingPos - 1; backwards >= 0; backwards--)
        {
            if(listOfSquares[backwards].currentPiece != null && listOfSquares[backwards].name != currentSquare.name)
            {
                obstacle = true;
                print(listOfSquares[backwards].name);
            }
            
            legalMovement.Add(listOfSquares[backwards], !obstacle);
        }

        return legalMovement;
    }
}