using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RaycastRotator : MonoBehaviour
{
    float spinSpeed = 500f;
    float raycastLength = 100f;

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + -transform.right * raycastLength);
    }

    public Vector3 Find(Piece target)
    {
        while(true)
        {
            // Spin the object
            transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);

            // Cast a ray in the direction the object is facing
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, raycastLength))
            {
                // Check if the ray hit an object with the target tag
                if (hit.collider.GetComponent<Piece>() == target)
                {
                    break;
                }
            }
        }
        print(this.transform.localEulerAngles);
        return this.transform.localEulerAngles;
    }
}
