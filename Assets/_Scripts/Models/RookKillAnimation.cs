using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class RookKillAnimation : KillAnimation 
{
    float laserDuration = 1f;

    [SerializeField] LineRenderer laserLine;
    [SerializeField] ParticleSystem warming;

    public override async Task TriggerKillAnimation(Piece targetPiece)
    {
        warming.gameObject.SetActive(true);
        await Task.Delay(1000);
        // float heightDistance = Vector3.Distance(new Vector3(targetPiece.transform.position.x, laserLine.transform.position.y, targetPiece.transform.position.z), targetPiece.transform.position);
        // laserLine.SetPosition(1, new Vector3(-Vector3.Distance(laserLine.transform.position, targetPiece.transform.position), -heightDistance, 0));
        StartCoroutine(AnimateLine(targetPiece));
        var p = Instantiate(Resources.Load<GameObject>(CommonData.Common.prefabFolder + "LaserExplosion"), targetPiece.transform);
        await Task.Delay(1600);
        laserLine.SetPosition(0, Vector3.zero);
        laserLine.SetPosition(1, Vector3.zero);
        laserLine.numCapVertices = 0;
        await Task.Delay(100);
        targetPiece.TriggerDeath();
        await Task.Delay(700);
        warming.gameObject.SetActive(false);
    }

    IEnumerator AnimateLine(Piece targetPiece)
    {
        laserLine.numCapVertices = 5;
        float startTime = Time.time;
        float duration = 0.1f;
        Transform heart = targetPiece.transform.GetChild(targetPiece.transform.childCount-1);
        float heightDistance = Vector3.Distance(new Vector3(heart.position.x, laserLine.transform.position.y, heart.position.z), heart.position);
        float xDistance = -Vector3.Distance(laserLine.transform.position, heart.position);

        laserLine.SetPosition(1, new Vector3(xDistance+1.5f, -heightDistance, 0));
        Vector3 targetScale = laserLine.transform.localScale;

        float t = 0f;
        while (t < duration)
        {
            laserLine.transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, t / duration);
            t += Time.deltaTime;
            yield return null;
        }
    }
}