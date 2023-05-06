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
        StartCoroutine(AnimateLine(targetPiece, false));
        var p = Instantiate(Resources.Load<GameObject>(CommonData.Common.prefabFolder + "LaserExplosion"), targetPiece.transform);
        await Task.Delay(1600);
        StartCoroutine(AnimateLine(targetPiece, true));
        await Task.Delay(100);
        targetPiece.TriggerDeath();
        await Task.Delay(700);
        warming.gameObject.SetActive(false);
    }

    IEnumerator AnimateLine(Piece targetPiece, bool end)
    {
        float startTime = Time.time;
        float duration = 0.1f;
        float elapsedTime = 0f;
        float heightDistance = Vector3.Distance(new Vector3(targetPiece.transform.position.x, laserLine.transform.position.y, targetPiece.transform.position.z), targetPiece.transform.position);
        float xDistance = -Vector3.Distance(laserLine.transform.position, targetPiece.transform.position);

        while (elapsedTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            float height = Mathf.Lerp(0, -heightDistance, t);
            float distance = Mathf.Lerp(laserLine.transform.position.x, xDistance, t);
            laserLine.SetPosition(end ? 0: 1, Vector3.Lerp(Vector3.zero, new Vector3(distance, height, 0), t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if(end)
        {
            laserLine.SetPosition(0, Vector3.zero);
            laserLine.SetPosition(1, Vector3.zero);
        }
    }
}