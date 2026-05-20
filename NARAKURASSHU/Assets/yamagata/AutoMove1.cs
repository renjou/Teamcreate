using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    public float speed = 1.0f;
    private int direction = 1;

    // HP
    public int hp = 1;

    void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        

        // wall タグに当たった時だけ反転＆キャラの向きも反転

        if (collision.gameObject.CompareTag("wall"))
        {
            direction *= -1;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }




        // プレイヤーの攻撃に当たったら消える
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            hp--;


            Debug.Log("Enemy HP : " + hp);

            // HPが0以下なら消える
            if (hp <= 0)

            {
                Destroy(gameObject);

                //プレイヤーに当たった

                if (collision.gameObject.CompareTag("Player"))
                {
                    // PlayerHP取得

                    PlayerHP playerHp = collision.gameObject.GetComponent<PlayerHP>();

                    // ダメージ1

                    if (playerHp != null)
                    {
                        playerHp.TakeDamage(1);

                    }
                }
            }
        }
    }
}