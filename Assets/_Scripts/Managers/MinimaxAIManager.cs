using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    #endregion

    public override void Setup()
    {

    }

    public int Minimax(int position, int depth, bool isMaxPlayer)
    {
        return 0;
    }
}
