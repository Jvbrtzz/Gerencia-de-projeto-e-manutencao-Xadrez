using UnityEngine;

public class OwnershipColor : MonoBehaviour 
{
    public int mainMaterialPos;
    public int secondaryMaterialPos;
    public int glassMaterialPos;
    
    public void SetupColor(bool isPlayerOwned)
    {
        Material blackMat = Resources.Load<Material>(CommonData.Common.materialFolder + "Black");
        Material whiteMat = Resources.Load<Material>(CommonData.Common.materialFolder + "White");

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Material[] newMats = new Material[3];
        newMats[mainMaterialPos] = isPlayerOwned ? whiteMat : blackMat;
        newMats[secondaryMaterialPos] = isPlayerOwned ? blackMat : whiteMat;
        newMats[glassMaterialPos] = isPlayerOwned ? blackMat : whiteMat;
        
        meshRenderer.materials = newMats;

        //Invert colors for wheels!
        Material[] wheelMats = new Material[1];
        wheelMats[0] = isPlayerOwned ? blackMat : whiteMat;

        foreach(var child in GetComponentsInChildren<MeshRenderer>())
        {
            if(child.transform != this.transform)
                child.materials = wheelMats;
        }
    }
}