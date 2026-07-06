using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class enemy3 : MonoBehaviour
{
    public Transform player;
    public float detectRange = 5.0f;    // 索敵範囲
    public float moveSpeed = 2.0f;     // スピード
    public int EnemyHp = 1;   // HP

    private Animator animator;

    public AudioSource audioSource;

    public AudioClip shotSE;
    public AudioClip deathSE;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Shoot()
    {
        audioSource.PlayOneShot(shotSE);

        animator.SetTrigger("Attack");
    }

    public void EnemyDamage(int damage)
    {
        audioSource.PlayOneShot(deathSE);

        EnemyHp -= damage;

        Debug.Log("Enemy HP : " + EnemyHp);

        if (EnemyHp <= 0) 
        {
            Die();
        }
    }

    void Die()
    {

        animator.SetTrigger("Die");

        GetComponent<Collider2D>().enabled = false;

        enabled = false;

        Destroy(gameObject, 1f);
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        // プレイヤーが範囲内なら追尾
        if (distance <= detectRange)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                player.position,
                moveSpeed * Time.deltaTime
            );
              


        }
        // 範囲外なら何もしない（その場で待機）
    }
}
 