using UnityEngine;

public class AttackObject1 : MonoBehaviour
{
    Collider2D collider2d;
    PlayerControl player;
    enemy1 enemy;
    Boss boss;

    public float direction = 1;
    float speed = 50;
    void Start()
    {
        Destroy(gameObject, 1f);
        collider2d = GetComponent<Collider2D>();
        player = FindFirstObjectByType<PlayerControl>();
        boss = FindFirstObjectByType<Boss>();
        enemy = FindFirstObjectByType<enemy1>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(player);
        Debug.Log(boss);
        if (collision.CompareTag("enemy"))
        {
            Debug.Log("ヒット");
            player.SpeGaugeIncrease();
            Destroy(gameObject);
            enemy.EnemyDamage(20);
        }

        if (collision.CompareTag("boss"))
        {
            Debug.Log("ボスヒット");
            player.SpeGaugeIncrease();
            Destroy(gameObject);
            boss.BossDamage(20);
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
