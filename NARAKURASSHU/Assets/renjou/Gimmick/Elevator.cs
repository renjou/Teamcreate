using UnityEngine;

public class Elevator : MonoBehaviour
{
    [Header("移動の設定")]
    public Transform[] waypoints; // エレベーターが移動するポイント（2箇所以上）
    public float speed = 3f;      // 移動速度
    public float delayTime = 1f;  // 到着時の待ち時間

    private int currentWaypointIndex = 0;
    private float timer = 0f;
    private bool isWaiting = false;

    void Update()
    {
        if (waypoints.Length == 0) return;

        if (isWaiting)
        {
            // 到着後の待機カウント
            timer += Time.deltaTime;
            if (timer >= delayTime)
            {
                isWaiting = false;
                timer = 0f;
                // 次の目的地を設定（ループ）
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            }
            return;
        }

        // 目的地に向かって移動
        Transform target = waypoints[currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // 目的地に到着したか判定
        if (Vector3.Distance(transform.position, target.position) < 0.01f)
        {
            isWaiting = true;
        }
    }

    // --- プレイヤーをガタつかせずに一緒に動かす処理 ---
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // プレイヤーが上に乗った場合（タグが "Player" の場合）
        if (collision.gameObject.CompareTag("Player"))
        {
            // プレイヤーをエレベーターの子要素にする
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // プレイヤーがエレベーターから離れた場合
        if (collision.gameObject.CompareTag("Player"))
        {
            // 子要素を解除する
            collision.transform.SetParent(null);
        }
    }
}