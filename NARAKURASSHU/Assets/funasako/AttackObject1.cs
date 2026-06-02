using UnityEngine;

public class AttackObject1 : MonoBehaviour
{
    Collider2D collider2d;
    PlayerControl player;


    public float direction = 1;
    float speed = 50;
    void Start()
    {
        Destroy(gameObject, 1f);
        collider2d = GetComponent<Collider2D>();
        player = FindFirstObjectByType<PlayerControl>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            Debug.Log("ヒット");
            player.SpeGaugeIncrease();
            Destroy(gameObject);
        }
;
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
