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
    private int direction = 1;

    void Start()
    {
        startpos = transform.position;

        Debug.Log("Enemy Start");
    }

    void Update()
    {
        // Pキーでダメージ
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            TakeDamage(1);
        }

        // 左右移動
        transform.Translate(
            Vector2.right * direction * Bspeed * Time.deltaTime);

        // 一定距離で反転
        if (Mathf.Abs(transform.position.x - startpos.x) > Brange)
        {
            direction *= -1;
        }
    }

    // ダメージ
    public void TakeDamage(int damage)
    {
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
        Debug.Log("Enemy Dead");

        if (boss != null)
        {
            Debug.Log("Boss Spawn");
            boss.SetActive(true);
        }
        else
        {
            Debug.Log("Boss が設定されていません");
        }

        Destroy(gameObject);
    }
}