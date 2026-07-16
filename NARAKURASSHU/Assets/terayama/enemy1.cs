using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


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

    private bool isKnockBack = false;

    private Animator anim;

    private Vector3 baseScale;

    private Rigidbody2D rb;

    private PlayerControl player;

    public AudioSource audioSource;
    public AudioClip deathSE;
    public AudioClip damageSE;

    [SerializeField] private GameObject die;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        audioSource = GetComponent<AudioSource>();

        // 始めた時の座標保存
        startpos = transform.position;
        // サイズ保存
        baseScale = transform.localScale;

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        /*
        // Pキーでダメージ
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            EnemyDamage(1);
        }
      */
        // 左右移動
        if (!isKnockBack)
        {
            transform.Translate(
                Vector2.right * direction * Bspeed * Time.deltaTime);
        }


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

        //    Debug.Log("Enemy HP : " + EnemyHp);

        if (player != null)
        {
            KnockBack(player.transform.position);
        }

        if (EnemyHp <= 0)
        {
            Die();
        }
    }

    // 死亡
    void Die()
    {
        audioSource.PlayOneShot(deathSE);

     //   Debug.Log("Enemy Dead");

        // Dieアニメーション再生
        anim.SetTrigger("Die");

        // 当たり判定を消す
        GetComponent<Collider2D>().enabled = false;

        // スクリプト停止
        enabled = false;

        if (boss != null)
        {
        //   Debug.Log("Boss Spawn");
        //    boss.SetActive(true);
        }
        else
        {
          //  Debug.Log("Boss が設定されていません");
        }

        // 1秒後に非表示
        StartCoroutine(Deth());
    }

    // 雑魚敵のhpがなくなった時の処理
    IEnumerator Deth()
    {
        yield return new WaitForSeconds(0.8f);

        gameObject.SetActive(false);
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
    public void Respawn()
    {
        // 元の位置に戻す
        transform.position = startpos;

        // HPを元に戻す
        EnemyHp = 3;

        // 当たり判定を戻す
        GetComponent<Collider2D>().enabled = true;

        // 向きを初期状態に戻す
        direction = -1;
        transform.localScale = baseScale;

        // Animatorを初期状態に戻す
        anim.Rebind();
        anim.Update(0f);

        // スクリプトを有効化
        enabled = true;

        // 表示
        gameObject.SetActive(true);
    }
    public void KnockBack(Vector3 playerPos)
    {
        isKnockBack = true;

        Vector2 dir = (transform.position - playerPos).normalized;

        rb.linearVelocity = Vector2.zero;

        rb.AddForce(dir * 5f, ForceMode2D.Impulse);

        StartCoroutine(EndKnockBack());
    }

    IEnumerator EndKnockBack()
    {
        yield return new WaitForSeconds(0.2f);

        isKnockBack = false;
    }
}