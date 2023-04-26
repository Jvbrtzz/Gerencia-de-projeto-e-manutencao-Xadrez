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
    public static event EventsManagerEvent HoverPiece;
    public static event EventsManagerEvent HoverElement;
    public static event EventsManagerEvent HoverNothing;



    public override void Setup()
    {

    }

    public void Call_HoverElement(string element, string square)
    {
        HoverElement?.Invoke(new object[] { element, square });
    }

    public void Call_HoverNothing()
    {
        HoverNothing?.Invoke(new object[] { });
    }

    public void Call_HoverPiece(Piece hoveredPiece)
    {
        HoverPiece?.Invoke(new object[] { hoveredPiece });
    }
}
