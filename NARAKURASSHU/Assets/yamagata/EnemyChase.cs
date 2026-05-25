using UnityEngine;

public class Tuibikyara : MonoBehaviour
{

    // プレイヤー
    public Transform player;

    // 移動速度
    public float speed = 2.0f;

    // 追尾する範囲
    public float chaseRange = 4f;


    void Update()
    {
        // プレイヤーとの距離
        float distance = Vector2.Distance(transform.position, player.position);

        // 範囲内なら追尾
        if (distance < chaseRange )
        {
            // プレイヤー方向
            Vector2 direction = (player.position - transform.position).normalized;

            // 移動
            transform.position += (Vector3)(direction * Time.deltaTime);

            // 向き変更
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        // 範囲外なら停止
        else
        {
            // 何もしない
        }
    }
}
