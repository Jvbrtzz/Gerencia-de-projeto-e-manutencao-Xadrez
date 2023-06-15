using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class King : Piece
{
    public override string pieceName { get { return "Rei"; } }
    public override float initialY { get => -17.8f; }

    public override Dictionary<Square, bool> LegalMovement(List<Square> possibleMovementSquares)
    {
        if (!isPlayerOwned)
            possibleMovementSquares.Reverse();

        Dictionary<Square, bool> legalMovement = new Dictionary<Square, bool>();

        foreach (var square in possibleMovementSquares)
        {
            if (IsSquareWithinKingRange(square) && (square.currentPiece == null || square.currentPiece.isPlayerOwned != isPlayerOwned))
            {
                legalMovement.Add(square, true);
            }
        }

        return legalMovement;
    }

    bool IsSquareWithinKingRange(Square square)
    {
        int rowDiff = Mathf.Abs(square.row - currentSquare.row);
        int colDiff = Mathf.Abs(square.column - currentSquare.column);

        return (rowDiff <= 1 && colDiff <= 1);
    }
}