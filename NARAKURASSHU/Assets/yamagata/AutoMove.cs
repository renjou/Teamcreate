using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
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
        // 壁に当たったら反転
        if (collision.gameObject.CompareTag("Wall"))
        {
            direction *= -1;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        if (collision.gameObject.CompareTag("PlayerAttack"))
        {

            hp--;

            Debug.Log("Enemy HP : " + hp);

            // HPが0以下なら消える
            if (hp <= 0)

            {
                Destroy(gameObject);
            }
        }

    }
}

