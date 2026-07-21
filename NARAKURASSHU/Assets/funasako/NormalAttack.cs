using UnityEngine;

public class NormalAttack : MonoBehaviour
{
    public Transform player;
    public Transform attackPoint;
    PlayerControl playerControl;
    HitStop hitStop;
    Boss boss;
    enemy1 enemy1;
    enemy2 enemy2;
    enemy3 enemy3;
    public AudioClip nHit;

    BoxCollider2D attackCollider;

    void Start()
    {
      //  Debug.Log("NormalAttack Start");
        attackCollider = GetComponent<BoxCollider2D>();
        playerControl = FindFirstObjectByType<PlayerControl>();
        hitStop = FindFirstObjectByType<HitStop>();
        boss = FindFirstObjectByType<Boss>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
     //   Debug.Log(playerControl);
     //   Debug.Log(boss);
        if (collision.CompareTag("enemy"))
        {
            Debug.Log("ヒット");
            playerControl.SpeGaugeIncrease();
            if (collision.TryGetComponent<enemy1>(out enemy1 enemy1))
            {
                enemy1.EnemyDamage(20);
                hitStop.HitStopBoot(1);
            }
            if (collision.TryGetComponent<enemy2>(out enemy2 enemy2))
            {
                enemy2.EnemyDamage(20);
                hitStop.HitStopBoot(1);
            }
            if (collision.TryGetComponent<enemy3>(out enemy3 enemy3))
            {
                enemy3.EnemyDamage(20);
                hitStop.HitStopBoot(1);
            }
        }
        if (collision.CompareTag("boss"))
        {
            //    Debug.Log("ヒット");
            hitStop.HitStopBoot(1);
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
