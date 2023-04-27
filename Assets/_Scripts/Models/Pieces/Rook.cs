public class Rook : Piece
{
    public override string pieceName { get { return "Torre"; } }
    public override float initialY { get => -17.8f; }

    public override bool LegalMovement(Square possibleMovementSquare)
    {
        if(possibleMovementSquare.currentPiece != null)
        {
            //invalid if you own the piece
            return false;
        }

        if(possibleMovementSquare.column == currentSquare.column)
        {
            return true;
        }
        else if(possibleMovementSquare.row == currentSquare.row)
        {
            return true;
        }

        return false;
    }
}