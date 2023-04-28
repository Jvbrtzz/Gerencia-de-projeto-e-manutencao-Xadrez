using System.Collections.Generic;

public class Rook : Piece
{
    public override string pieceName { get { return "Torre"; } }
    public override float initialY { get => -17.8f; }

    public override Dictionary<Square, bool> LegalMovement(List<Square> possibleMovementSquares)
    {
        Dictionary<Square, bool> legalMovement = new Dictionary<Square, bool>();
        var obstacle = false;

        foreach(var possibleRowMovement in ListRowPieces(possibleMovementSquares, currentSquare.row))
        {
            if(possibleRowMovement.currentPiece != null)
            {
                if(!legalMovement.ContainsKey(possibleRowMovement))
                    legalMovement.Add(possibleRowMovement, false);

                if(possibleRowMovement.currentPiece != currentSquare.currentPiece)
                    obstacle = true;
            }

            if(obstacle)
            {
                if(!legalMovement.ContainsKey(possibleRowMovement))
                    legalMovement.Add(possibleRowMovement, false);
            }
            else
            {
                if(!legalMovement.ContainsKey(possibleRowMovement))
                    legalMovement.Add(possibleRowMovement, true);
            }
        }

        obstacle = false;

        foreach(var possibleColMovement in ListColumnPieces(possibleMovementSquares, currentSquare.column))
        {
            if(possibleColMovement.currentPiece != null)
            {
                if(!legalMovement.ContainsKey(possibleColMovement))
                    legalMovement.Add(possibleColMovement, false);

                if(possibleColMovement.currentPiece != currentSquare.currentPiece)
                    obstacle = true;
            }

            if(obstacle)
            {
                if(!legalMovement.ContainsKey(possibleColMovement))
                    legalMovement.Add(possibleColMovement, false);
            }
            else
            {
                if(!legalMovement.ContainsKey(possibleColMovement))
                    legalMovement.Add(possibleColMovement, true);
            }
        }

        return legalMovement;
    }
}