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
                if(possibleMovementSquare.currentPiece.isPlayerOwned == this.isPlayerOwned)
                    legalMovement.Add(possibleMovementSquare, false);
            }

            if(possibleMovementSquare.column == currentSquare.column)
            {
                if(possibleMovementSquare.row == currentSquare.row + (isPlayerOwned ? 1 : -1))
                {
                    legalMovement.Add(possibleMovementSquare, true);
                }
            }
        }

        return legalMovement;
    }
}