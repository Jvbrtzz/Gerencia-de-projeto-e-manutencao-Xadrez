using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Events;

//Holds every user-related information the app may need;

public class GameManager : Singleton
{
    //My custom Singleton Functions (Check Singleton script for more info);
    #region Singleton

    public static GameManager instance = new GameManager();
    public static GameManager Get() => instance;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialize()
    {
        instance = (GameManager)Init(instance);
        instance.Setup();
    }

    #endregion

    #region Variables

    public bool isGameOver = false;
    public bool isPlayersTurn = true;
    public Piece selectedPiece;
    public List<Square> squareGrid = new List<Square>();
    public List<Square> possibleSquares = new List<Square>();

    public King playerKing;
    public King opponentKing;

    #endregion

    public override void Setup()
    {

    }

    public void PassTurn()
    {
        isPlayersTurn = !isPlayersTurn;
        EventsManager.Get().Call_PassTurn();
    }

    public Square GetSquare(int row, int column)
    {
        foreach (var p in squareGrid)
        {
            if (p.row == row && p.column == column)
                return p;
        }

        return null;
    }

    public void EndGame(bool isPlayerVictory)
    {
        GameManager.Get().isGameOver = true;
        EventsManager.Get().Call_EndGame(isPlayerVictory);
    }

    public bool CheckForCheck(Piece king, List<Square> squareGrid)
    {
        Square[] board = new Square[squareGrid.Count];
        GameManager.Get().squareGrid.CopyTo(board);

        foreach(Square square in board)
        {
            if(square.currentPiece != null)
            {
                if(square.currentPiece.isPlayerOwned != king.isPlayerOwned)
                {
                    foreach(var possibleMovement in square.currentPiece.LegalMovement(board.ToList()))
                    {
                        if(possibleMovement.Key == king.currentSquare)
                        {
                            print("CHECK");
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }

    public bool CheckIfMovementChangesCheck(Piece king, Piece currentPiece, Square toSquare)
    {
        Square[] board = new Square[GameManager.Get().squareGrid.Count];
        GameManager.Get().squareGrid.CopyTo(board);
        
        for(int i = 0; i < board.Length; i++)
        {
            if(board[i] == toSquare)
            {
                board[i].currentPiece = currentPiece;
            }
        }

        return CheckForCheck(king, board.ToList());
    }
}
