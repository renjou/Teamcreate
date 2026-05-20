using UnityEngine;

public class AttackObject1 : MonoBehaviour
{
    public float direction = 1;
    float speed = 100;
    void Start()
    {
        Destroy(gameObject, 1f);
    }

    void Update()
    {
        if (direction == 1)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
}
