public class Pawn : Piece
{
    public override string pieceName { get { return "Peão"; } }
    public override float initialY { get => 0; }

    public override bool LegalMovement(Square possibleMovementSquare)
    {
        if(possibleMovementSquare.currentPiece != null)
        {
            //invalid if you own the piece
        }

        if(possibleMovementSquare.column == currentSquare.column)
        {
            if(possibleMovementSquare.row > currentSquare.row && possibleMovementSquare.row <= currentSquare.row + 3)
            {
                return true;
            }
        }

        return false;
    }
}