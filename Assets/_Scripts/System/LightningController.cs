using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningController : MonoBehaviour
{
    [SerializeField] private Light spotlight;
    [SerializeField] private Color enemyColor;
    [SerializeField] private Color playerColor;

    private void Start() 
    {
        EventsManager.PassTurn += OnPassTurn;
    }

    private void OnDestroy() 
    {
        EventsManager.PassTurn -= OnPassTurn;
    }

    void OnPassTurn(object[] args)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeColor());
    }

    IEnumerator ChangeColor()
    {
        float duration = 1;
        Color startingColor = spotlight.color;
        Color endColor = GameManager.Get().isPlayersTurn ? playerColor : enemyColor; 

        float t = 0f;
        while (t < duration)
        {
            spotlight.color = Color.Lerp(startingColor, endColor, t / duration);
            t += Time.deltaTime;
            yield return null;
        }

        spotlight.color = endColor;
    }
}
