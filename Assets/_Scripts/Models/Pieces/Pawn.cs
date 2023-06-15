using System.Collections.Generic;

public class Pawn : Piece
{
    public override string pieceName { get { return "Peão"; } }
    public override float initialY { get => 0; }

    //Pawns exception to move two squares on its first turn
    public bool hasMoved = false;

    public override Dictionary<Square, bool> LegalMovement(List<Square> possibleMovementSquares)
    {
        Dictionary<Square, bool> legalMovement = new Dictionary<Square, bool>();

        if(!isPlayerOwned)
            possibleMovementSquares.Reverse();
        
        var obstacle = false;

        foreach(var possibleMovementSquare in possibleMovementSquares)
        {
            if(possibleMovementSquare.column == currentSquare.column)
            {
                if(possibleMovementSquare.row == currentSquare.row + (isPlayerOwned ? 1 : -1)
                    || (!hasMoved && possibleMovementSquare.row == currentSquare.row + (isPlayerOwned ? 2 : -2)))
                {
                    if(possibleMovementSquare.currentPiece == null && !obstacle)
                        legalMovement.Add(possibleMovementSquare, true);
                    else
                        obstacle = true;
                }
            }
            else
            {
                if(possibleMovementSquare.row == currentSquare.row + (isPlayerOwned ? 1 : -1))
                {
                    if(possibleMovementSquare.column == currentSquare.column -1 || possibleMovementSquare.column == currentSquare.column + 1)
                        if(possibleMovementSquare.currentPiece != null && possibleMovementSquare.currentPiece.isPlayerOwned != isPlayerOwned)
                            legalMovement.Add(possibleMovementSquare, true);
                }
            }
        }

        return legalMovement;
    }

    public override void OnFirstMovement()
    {
        hasMoved = true;
    }
}