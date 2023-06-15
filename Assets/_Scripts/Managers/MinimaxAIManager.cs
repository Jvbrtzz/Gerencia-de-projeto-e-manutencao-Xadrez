using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MinimaxAIManager : Singleton
{
    //My custom Singleton Functions (Check Singleton script for more info);
    #region Singleton

    public static MinimaxAIManager instance = new MinimaxAIManager();
    public static MinimaxAIManager Get() => instance;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialize()
    {
        instance = (MinimaxAIManager)Init(instance);
        instance.Setup();
    }

    #endregion

    #region Variables
    public List<Movement> _moves = new List<Movement>();
    #endregion

    public override void Setup()
    {

    }

    public Movement AIMovement()
    {
        Square[] board = new Square[GameManager.Get().squareGrid.Count];

        GameManager.Get().squareGrid.CopyTo(board);

        List<Movement> moves = new List<Movement>();

        foreach (var square in board)
        {
            if (square.currentPiece != null)
            {
                if (!square.currentPiece.isPlayerOwned)
                {
                    var piece = square.currentPiece;

                    var possibleMovements = piece.LegalMovement(board.ToList());

                    foreach (var possibleMovement in possibleMovements)
                    {
                        int value = piece is Pawn && !((Pawn)piece).hasMoved && Random.Range(0, 3) == 0 ? 1 : RateMovement(possibleMovement.Key);
                        moves.Add(new Movement(value: value, currentPiece: piece, toSquare: possibleMovement.Key));
                    }
                }
            }
        }

        foreach (var square in board)
        {
            if (square.currentPiece != null)
            {
                if (square.currentPiece.isPlayerOwned)
                {
                    var piece = square.currentPiece;

                    var possibleMovements = piece.LegalMovement(board.ToList());

                    foreach (var possibleMovement in possibleMovements)
                    {
                        foreach(var move in moves)
                        {
                            if(move.toSquare == possibleMovement.Key)
                            {
                                int negativeValue = RatePlayerRetaliation(move.currentPiece);
                                move.value = move.value - negativeValue;
                            }
                        }
                    }
                }
            }
        }

        Movement bestMove = null;

        foreach (var move in moves)
        {
            if (bestMove == null)
                bestMove = move;

            else
            {
                if (bestMove.value == move.value)
                {
                    bestMove = Random.Range(0, 3) == 0 ? move : bestMove;
                }
                else
                    bestMove = bestMove.value < move.value ? move : bestMove;
            }
        }

        _moves = moves;

        return bestMove;
    }

    private int RateMovement(Square toSquare)
    {
        return toSquare.currentPiece == null ? 0 : toSquare.currentPiece.GetValue();
    }

    private int RatePlayerRetaliation(Piece pieceToBeLost)
    {
        return pieceToBeLost.GetValue();
    }
}

[System.Serializable]
public class Movement
{
    public Movement(int value, Piece currentPiece, Square toSquare)
    {
        this.value = value;
        this.currentPiece = currentPiece;
        this.toSquare = toSquare;
    }

    public int value;
    public Piece currentPiece;
    public Square toSquare;
}