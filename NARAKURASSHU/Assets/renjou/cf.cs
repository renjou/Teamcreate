using UnityEngine;

public class cf : MonoBehaviour
{
   
    public Transform target;

    
    public Vector3 offset;

   
    public float smoothSpeed = 0f;


    void LateUpdate()
    {
        if (target == null) return;

        
        Vector3 targetPosition = target.position + offset;

     
        targetPosition.z = -10f;

       
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}