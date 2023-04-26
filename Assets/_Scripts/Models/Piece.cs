using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public virtual string pieceName { get; set; }
    public Square currentSquare;
    
    public abstract bool LegalMovement(Square square);

    public void UpdateSquareInformation(Square newSquare)
    {
        if(currentSquare != null)
            currentSquare.currentPiece = null;
        
        currentSquare = newSquare;
        currentSquare.currentPiece = this;
    }
}