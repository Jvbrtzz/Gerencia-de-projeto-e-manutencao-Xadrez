using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BishopKillAnimation : KillAnimation
{
    [SerializeField] GameObject holyParticles;
    [SerializeField] GameObject shootingParticles;

    public override async Task TriggerKillAnimation(Piece targetPiece)
    {
        holyParticles.SetActive(true);
        await Task.Delay(2100);
        shootingParticles.SetActive(true);
        await Task.Delay(100);
        targetPiece.TriggerDeath();
        await Task.Delay(300);
        holyParticles.SetActive(false);
        shootingParticles.SetActive(false);
    }
}
