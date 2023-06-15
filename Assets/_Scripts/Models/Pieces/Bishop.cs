using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bishop : Piece
{
    public override string pieceName { get { return "Bispo"; } }
    public override float initialY { get => -19f; }

    public override Dictionary<Square, bool> LegalMovement(List<Square> possibleMovementSquares)
    {
        if (!isPlayerOwned)
            possibleMovementSquares.Reverse();

        Dictionary<Square, bool> legalMovement = new Dictionary<Square, bool>();

        foreach (var square in possibleMovementSquares)
        {
            if (IsSquareOnDiagonal(square))
            {
                if (IsPathClear(square))
                {
                    if (square.currentPiece == null || square.currentPiece.isPlayerOwned != isPlayerOwned)
                    {
                        legalMovement.Add(square, true);
                    }
                }
            }
        }

        return legalMovement;
    }

    bool IsSquareOnDiagonal(Square square)
    {
        int rowDiff = Mathf.Abs(square.row - currentSquare.row);
        int colDiff = Mathf.Abs(square.column - currentSquare.column);

        return (rowDiff == colDiff);
    }

    bool IsPathClear(Square square)
    {
        int rowDiff = square.row - currentSquare.row;
        int colDiff = square.column - currentSquare.column;
        int rowIncrement = (rowDiff == 0) ? 0 : (rowDiff > 0) ? 1 : -1;
        int colIncrement = (colDiff == 0) ? 0 : (colDiff > 0) ? 1 : -1;

        int row = currentSquare.row + rowIncrement;
        int col = currentSquare.column + colIncrement;

        while (row != square.row || col != square.column)
        {
            if (GameManager.Get().GetSquare(row, col).currentPiece != null)
            {
                return false;
            }

            row += rowIncrement;
            col += colIncrement;
        }

        return true;
    }
}