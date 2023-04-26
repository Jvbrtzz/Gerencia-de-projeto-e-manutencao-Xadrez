using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : Singleton
{
    #region Singleton

    public static EventsManager instance = new EventsManager();
    public static EventsManager Get() => instance;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialize()
    {
        instance = (EventsManager)Init(instance);
        instance.Setup();
    }

    #endregion

    public delegate void EventsManagerEvent(object[] obj = null);
    public static event EventsManagerEvent SelectPiece;



    public override void Setup()
    {

    }

    public void Call_SelectPiece(Piece selectedPiece)
    {
        SelectPiece?.Invoke(new object[] { selectedPiece });
    }
}
