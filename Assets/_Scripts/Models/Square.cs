using UnityEngine;

public class Square
{
    public Transform transform;
    public char column;
    public int row;

    public Square(Transform _transform, char _column, int _row)
    {
        transform = _transform;
        column = _column;
        row = _row;
    }
}