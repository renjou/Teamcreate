using UnityEngine;
using UnityEngine.InputSystem;

public class enemy1 : MonoBehaviour
{
    public int EnemyHp = 3;

    // 出現させるボス
    public GameObject boss;

    // 左右移動
    public float Bspeed = 2f;
    public float Brange = 1f;

    // 初期位置
    private Vector3 startpos;

    // 移動方向
    private int direction = -1;

    private Animator anim;

    private Vector3 baseScale;

    public AudioSource audioSource;
    public AudioClip deathSE;
    public AudioClip damageSE;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        startpos = transform.position;

        baseScale = transform.localScale;

        anim = GetComponent<Animator>();
    }

    void Update()
    {/*
        // Pキーでダメージ
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            EnemyDamage(1);
        }
        */
        // 左右移動
        transform.Translate(
            Vector2.right * direction * Bspeed * Time.deltaTime);

        
        // 一定距離で反転
        if (transform.position.x > startpos.x + Brange)
        {
            direction = -1;

            transform.localScale =
                new Vector3(baseScale.x, baseScale.y, baseScale.z);
        }
        else if (transform.position.x < startpos.x - Brange)
        {
            direction = 1;

            transform.localScale =
                new Vector3(-baseScale.x, baseScale.y, baseScale.z);
        }

        anim.SetBool("Run", true);
    }

    // ダメージ
    public void EnemyDamage(int damage)
    {
        audioSource.PlayOneShot(damageSE);

        // ダメージアニメーション
        anim.SetTrigger("Damage");

        EnemyHp -= damage;

        Debug.Log("Enemy HP : " + EnemyHp);

        if (EnemyHp <= 0)
        {
            Die();
        }
    }

    // 死亡
    void Die()
    {
        audioSource.PlayOneShot(deathSE);

        Debug.Log("Enemy Dead");

        // Dieアニメーション再生
        anim.SetTrigger("Die");

        // 当たり判定を消す
        GetComponent<Collider2D>().enabled = false;

        // スクリプト停止
        enabled = false;

        if (boss != null)
        {
            Debug.Log("Boss Spawn");
        //    boss.SetActive(true);
        }
        else
        {
            Debug.Log("Boss が設定されていません");
        }

        // 1秒後に削除
        Destroy(gameObject, 0.5f);
    }
    private void OnTriggerEnter2D(Collider2D collison)
    {
        if (collison.CompareTag("Player"))
        {
          //  Debug.Log("プレイヤーにヒット");

            PlayerControl player = FindFirstObjectByType<PlayerControl>();
          //  player.PlayerDamage();
          //  player.SpeGaugeIncrease();
          //  player.KnockBack(transform.position);
        }
    }
}