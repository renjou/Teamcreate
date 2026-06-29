using UnityEngine;
using UnityEngine.Audio;

public class Bat : MonoBehaviour
{
    public Transform player;
    public float detectRange = 5.0f;    // 索敵範囲
    public float moveSpeed = 2.0f;     // スピード
    public int EnemyHp = 1;   // HP
    private Animator anim;
    public void EnemyDamage(int damage)
    {
        EnemyHp -= damage;
        if (EnemyHp <= 0) 
        {
            Die();
        }
    }

    void Die()
    {

        anim.SetTrigger("Die");

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
 