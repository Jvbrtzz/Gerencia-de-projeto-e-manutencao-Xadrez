using UnityEngine;

[System.Serializable]
public class Square : MonoBehaviour
{
    public char column;
    public int row;
    
    public Piece currentPiece;

    public Square(char _column, int _row)
    {
        column = _column;
        row = _row;
    }
}