using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public Square currentSquare;
    
    public abstract bool LegalMovement(Square square);
}