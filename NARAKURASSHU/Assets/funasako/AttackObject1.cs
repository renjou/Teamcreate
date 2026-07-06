using UnityEngine;

public class AttackObject1 : MonoBehaviour
{
    Collider2D collider2d;
    PlayerControl player;
    enemy1 enemy1;
    enemy2 enemy2;
    enemy3 enemy3;
    Boss boss;
    public AudioClip hit;

    public float direction = 1;
    float speed = 50;
    void Start()
    {
        Destroy(gameObject, 1f);
        collider2d = GetComponent<Collider2D>();
        player = FindFirstObjectByType<PlayerControl>();
        boss = FindFirstObjectByType<Boss>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log(player);
        // Debug.Log(boss);
        if (collision.CompareTag("enemy"))
        {
            Debug.Log("ヒット");
            player.SpeGaugeIncrease();
            if (collision.TryGetComponent<enemy1>(out enemy1 enemy1))
            {
                Destroy(gameObject);
                enemy1.EnemyDamage(20);
            }
            if (collision.TryGetComponent<enemy2>(out enemy2 enemy2))
            {
                Destroy(gameObject);
                enemy2.EnemyDamage(20);
            }
            if (collision.TryGetComponent<enemy3>(out enemy3 enemy3))
            {
                enemy3.EnemyDamage(20);
            }
        }

        if (collision.CompareTag("boss"))
        {
            Debug.Log("ボスヒット");
            player.SpeGaugeIncrease();
            Destroy(gameObject);
            boss.BossDamage(10);
        }
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
