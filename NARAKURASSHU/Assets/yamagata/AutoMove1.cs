using UnityEngine;

public class AutoMove : MonoBehaviour
 {
    public float speed = 3.0f;


    void Update()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
    }
}
