using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public int hp = 1;
    public float speed = 1.0f;
    private int direction = 1;


    void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
    }

    // ダメージを受ける関数
    public void TakeDamage(int damage)
    {
        hp -= damage;


        Debug.Log("Player HP : " + hp);


        // HP0で消える

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}