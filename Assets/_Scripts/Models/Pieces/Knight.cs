using System.Collections.Generic;
using UnityEngine.Events;

public class Knight : Piece
{
    public override string pieceName { get { return "Cavalo"; } }
    public override float initialY { get => -17.8f; }

    public override Dictionary<Square, bool> LegalMovement(List<Square> possibleMovementSquares)
    {
        if (!isPlayerOwned)
            possibleMovementSquares.Reverse();

        Dictionary<Square, bool> legalMovement = new Dictionary<Square, bool>();

        return legalMovement;
    }
}