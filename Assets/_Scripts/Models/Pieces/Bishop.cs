using System.Collections.Generic;
using UnityEngine.Events;

public class Bishop : Piece
{
    public override string pieceName { get { return "Bispo"; } }
    public override float initialY { get => -19f; }

    public override Dictionary<Square, bool> LegalMovement(List<Square> possibleMovementSquares)
    {
        if (!isPlayerOwned)
            possibleMovementSquares.Reverse();

        Dictionary<Square, bool> legalMovement = new Dictionary<Square, bool>();

        return legalMovement;
    }
}