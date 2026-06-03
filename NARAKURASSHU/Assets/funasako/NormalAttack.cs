using UnityEngine;

public class NormalAttack : MonoBehaviour
{
    public Transform player;
    public Transform attackPoint;
    PlayerControl playerControl;
    Boss boss;
    enemy1 enemy;

    BoxCollider2D attackCollider;

    void Start()
    {
        Debug.Log("NormalAttack Start");
        attackCollider = GetComponent<BoxCollider2D>();
        playerControl = FindFirstObjectByType<PlayerControl>();
        boss = FindFirstObjectByType<Boss>();
        enemy = FindFirstObjectByType<enemy1>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(playerControl);
        Debug.Log(boss);
        if (collision.CompareTag("enemy"))
        {
            Debug.Log("ヒット");
            playerControl.SpeGaugeIncrease();
            enemy.EnemyDamage(20);
        }
        if (collision.CompareTag("boss"))
        {
            Debug.Log("ヒット");
            playerControl.SpeGaugeIncrease();
            boss.BossDamage(5);
        }
    }


    public void AttackOn()
    {
        attackCollider.enabled = true;
    }

    public void AttackOff()
    {
        attackCollider.enabled = false;
    }
}
