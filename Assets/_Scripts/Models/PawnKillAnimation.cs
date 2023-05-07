using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class PawnKillAnimation : KillAnimation 
{
    [SerializeField] GameObject particleSystem;

    public override async Task TriggerKillAnimation(Piece targetPiece)
    {
        await Task.Delay(500);
        StartCoroutine(Recoil());
        particleSystem.SetActive(true);
        await Task.Delay(350);
        targetPiece.TriggerDeath();
        await Task.Delay(800);
        particleSystem.SetActive(false);
    }

    IEnumerator Recoil()
    {
        Vector3 originalPosition = transform.position;
        float recoilDistance = 0.5f;
        float recoilSpeed = 10f;

        Vector3 recoilDirection = transform.right * recoilDistance;
        float recoilTime = recoilDistance / recoilSpeed;

        // Move the transform back during the recoil
        float elapsedRecoilTime = 0f;
        while (elapsedRecoilTime < recoilTime)
        {
            float recoilAmount = recoilSpeed * Time.deltaTime;
            transform.position += recoilDirection * recoilAmount;
            elapsedRecoilTime += Time.deltaTime;
            yield return null;
        }
    }
}