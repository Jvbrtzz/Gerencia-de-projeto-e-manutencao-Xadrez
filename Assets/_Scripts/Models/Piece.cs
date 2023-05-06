using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public virtual string pieceName { get; set; }
    public Square currentSquare;
    public virtual float initialY { get; set; }
    
    public bool isPlayerOwned;
    
    public abstract Dictionary<Square, bool> LegalMovement(List<Square> squares);

    private void OnDestroy() 
    {
        currentSquare.currentPiece = null;
    }
    
    public void UpdateSquareInformation(Square newSquare)
    {
        if(currentSquare != null)
            currentSquare.currentPiece = null;
        
        currentSquare = newSquare;
        currentSquare.currentPiece = this;
    }

    public void SetOwnership(bool playerOwned)
    {
        isPlayerOwned = playerOwned;
        GetComponent<OwnershipColor>().SetupColor(playerOwned);
    }

    public List<Square> ListRowPieces(List<Square> squares, int row)
    {
        var rowPieces = new List<Square>();
        foreach(var square in squares)
        {
            if(square.row == row)
            {
                rowPieces.Add(square);
            }
        }

        return rowPieces;
    }

    public List<Square> ListColumnPieces(List<Square> squares, char column)
    {
        var columnPieces = new List<Square>();
        foreach(var square in squares)
        {
            if(square.column == column)
            {
                columnPieces.Add(square);
            }
        }

        return columnPieces;
    }

    public async Task TriggerKillAnimation(Piece target)
    {
        await GetComponent<KillAnimation>().TriggerKillAnimation(target);
    }

    public void TriggerDeath()
    {
        var p = Instantiate(Resources.Load<GameObject>(CommonData.Common.prefabFolder + "DeathParticles"), this.transform);
        p.transform.parent = null;
        Destroy(this.gameObject);
        Destroy(p, 4);
    }
}