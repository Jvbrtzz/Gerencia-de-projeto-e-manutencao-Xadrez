using System.Collections.Generic;

public class Pawn : Piece
{
    public override string pieceName { get { return "Peão"; } }
    public override float initialY { get => 0; }

    public override Dictionary<Square, bool> LegalMovement(List<Square> possibleMovementSquares)
    {
        Dictionary<Square, bool> legalMovement = new Dictionary<Square, bool>();
        
        foreach(var possibleMovementSquare in possibleMovementSquares)
        {
            if(possibleMovementSquare.currentPiece != null)
            {
                //invalid if you own the piece
                legalMovement.Add(possibleMovementSquare, false);
            }

            if(possibleMovementSquare.column == currentSquare.column)
            {
                if(possibleMovementSquare.row == currentSquare.row + 1)
                {
                    legalMovement.Add(possibleMovementSquare, true);
                }
            }
        }

        return legalMovement;
    }
}