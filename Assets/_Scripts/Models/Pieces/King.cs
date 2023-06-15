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
            else if (CanCastle(square))
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

    bool CanCastle(Square square)
    {
        // Check if the king has moved
        if (hasMoved)
            return false;

        // Check if the square contains a rook owned by the same player
        if (square.currentPiece != null && square.currentPiece.isPlayerOwned == isPlayerOwned && square.currentPiece is Rook)
        {
            Rook rook = (Rook)square.currentPiece;

            // Check if the rook has not moved
            if (!rook.hasMoved)
            {
                // Check if the squares between the king and rook are empty
                int direction = square.column > currentSquare.column ? 1 : -1;
                int startColumn = currentSquare.column + direction;
                int endColumn = square.column - direction;

                for (int column = startColumn; column != endColumn; column += direction)
                {
                    Square intermediateSquare = GameManager.Get().GetSquare(currentSquare.row, column);
                    print(intermediateSquare.column + intermediateSquare.row.ToString() + " " + intermediateSquare.currentPiece);

                    if (intermediateSquare.currentPiece != null)
                        return false;
                }
                
                return true;
            }
        }

        return false;
    }

}