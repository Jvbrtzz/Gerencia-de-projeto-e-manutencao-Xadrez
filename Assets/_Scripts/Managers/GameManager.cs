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

    public bool isPlayersTurn = true;
    public Piece selectedPiece;
    public List<Square> possibleSquares = new List<Square>();

    #endregion

    public override void Setup()
    {

    }
}
