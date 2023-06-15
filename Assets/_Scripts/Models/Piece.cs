using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public abstract class Piece : MonoBehaviour
{
    public virtual string pieceName { get; set; }
    public Square currentSquare;
    public virtual float initialY { get; set; }

    public bool isPlayerOwned;
    public bool hasMoved = false;

    public abstract Dictionary<Square, bool> LegalMovement(List<Square> squares);

    public virtual void OnFirstMovement() { hasMoved = true; }

    public virtual void OnSpecialAvailableMovement() { }

    private void FixedUpdate() 
    {
        currentSquare.currentPiece = this;
    }

    private void OnDestroy()
    {
        currentSquare.currentPiece = null;
    }

    public void UpdateSquareInformation(Square newSquare)
    {
        if (currentSquare != null)
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
        foreach (var square in squares)
        {
            if (square.row == row)
            {
                rowPieces.Add(square);
            }
        }

        return rowPieces;
    }

    public List<Square> ListColumnPieces(List<Square> squares, char column)
    {
        var columnPieces = new List<Square>();
        foreach (var square in squares)
        {
            if (square.column == column)
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
        if(this is King)
        {
            GameManager.Get().EndGame(!isPlayerOwned);
        }

        var p = Instantiate(Resources.Load<GameObject>(CommonData.Common.prefabFolder + "DeathParticles"), this.transform);
        p.transform.parent = null;
        Destroy(this.gameObject);
        Destroy(p, 4);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + -transform.right * 100f);
    }

    public void Find(Piece target)
    {
        bool hasHit = false;
        while (!hasHit)
        {
            // Spin the object
            transform.RotateAroundLocal(Vector3.up, 100f * Time.deltaTime);

            // Cast a ray in the direction the object is facing
            foreach (var hit in Physics.RaycastAll(transform.position, -transform.right, 100f))
            {
                if (hit.collider.tag == "Heart")
                {
                    // Check if the ray hit an object with the target tag
                    if (hit.collider.GetComponentInParent<Piece>() == target)
                    {
                        hasHit = true;
                        break;
                    }
                }
            }
        }
    }

    public int GetValue()
    {
        // Assign a value to each piece type based on its importance or strength
        switch (pieceName)
        {
            case "Peão":
                return 1;
            case "Torre":
                return 5;
            case "Cavalo":
                return 3;
            case "Bispo":
                return 3;
            case "Rainha":
                return 9;
            case "Rei":
                return 100;
            default:
                return 0;
        }
    }
}