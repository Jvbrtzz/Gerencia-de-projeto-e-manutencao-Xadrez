public class Pawn : Piece
{
    public override string pieceName { get { return "Peão"; } }

    public override bool LegalMovement(Square possibleMovementSquare)
    {
        if(possibleMovementSquare.currentPiece != null)
        {
            //invalid if you own the piece
        }

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