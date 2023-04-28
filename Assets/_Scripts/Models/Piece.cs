using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public virtual string pieceName { get; set; }
    public Square currentSquare;
    public virtual float initialY { get; set; }
    
    public abstract Dictionary<Square, bool> LegalMovement(List<Square> squares);

    public void UpdateSquareInformation(Square newSquare)
    {
        if(currentSquare != null)
            currentSquare.currentPiece = null;
        
        currentSquare = newSquare;
        currentSquare.currentPiece = this;
    }

    public List<Square> ListRowPieces(List<Square> squares, int row)
    {
        var rowPieces = new List<Square>();
        foreach(var square in squares)
        {
            if(square.row == row)
            {
                rowPieces.Add(square);
            }
        }

        return rowPieces;
    }

    public List<Square> ListColumnPieces(List<Square> squares, char column)
    {
        var columnPieces = new List<Square>();
        foreach(var square in squares)
        {
            if(square.column == column)
            {
                columnPieces.Add(square);
            }
        }

        return columnPieces;
    }
}