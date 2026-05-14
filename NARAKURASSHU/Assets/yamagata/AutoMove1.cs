using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AutoMove : MonoBehaviour
 {
    public float speed = 1.0f;
    private int direction = 1; 

    void Update()
    {
        transform.position += new Vector3(direction*speed * Time.deltaTime, 0, 0);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        
            direction = -1;

        Vector3 scale = transform.localScale;
        scale.x = -1;
        transform.localScale = scale;
    
  }
}