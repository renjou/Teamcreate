using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;

    private Vector2 direction;

    // 発射方向設定
    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        // 弾移動
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // プレイヤーに当たった
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

        // 壁に当たった
        if(collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

    }

}
