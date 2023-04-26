public class Pawn : Piece
{
    public override bool LegalMovement(Square possibleMovementSquare)
    {
        if(possibleMovementSquare.column == currentSquare.column)
        {
            if(possibleMovementSquare.row == currentSquare.row + 1)
            {
                return true;
            }
        }

        return false;
    }
}