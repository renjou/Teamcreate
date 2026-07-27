using UnityEngine;

public class Elevator : MonoBehaviour
{
    [Header("移動の設定")]
    public Transform[] waypoints; // [0]を地上（スタート）、[1]を地下（目的地）
    public float speed = 3f;      // 移動速度
    public float delayTime = 1f;  // 到着時の待ち時間

    private int currentWaypointIndex = 0;
    private float timer = 0f;
    private bool isWaiting = false;
    private bool isPlayerOn = false; // プレイヤーが乗っているか
    [SerializeField] private AudioClip soundToPlay;
    private AudioSource audioSource;

    private void Start()
    {

        if (waypoints.Length >= 2)
        {
            // ゲーム開始時はすでに地上(0)にいるので、
            // 「到着して待機している状態（プレイヤーが乗るのを待つ状態）」からスタートさせる
            isWaiting = true;
            currentWaypointIndex = 1; // 次に目指すのは地下(1)
        }
        audioSource = GetComponent<AudioSource>();
        if(audioSource ==null)
        {
            audioSource=gameObject.AddComponent<AudioSource>();
        }
        if (audioSource != null)
        {

            audioSource.clip = soundToPlay;
            audioSource.loop = true;

        }
    }
    void Update()
    {
        if (waypoints.Length < 2) return;

        // ★【最重要：詰み防止ロジック】
        // 移動中にプレイヤーが離れた（落ちた）場合の処理
        if (!isPlayerOn && !isWaiting)
        {
            if (currentWaypointIndex == 0)
            {
                // 地下(1)から地上(0)へ昇っている途中に落ちたら、地上に行かせず「地下(1)」に引き返させる
                currentWaypointIndex = 1;
            }
            // ※ 地上(0)から地下(1)へ向かっている途中に落ちた場合は、
            // 何もしない（そのまま地下(1)まで進ませる）ことで、プレイヤーとエレベーターが地下で合流できます。
        }

        if (isWaiting)
        {
            // 到着後の最低待ち時間
            if (timer < delayTime)
            {
                timer += Time.deltaTime;
                return;
            }

            // 待ち時間が終わったあと、プレイヤーが乗ったら次のポイントへ発車
            if (isPlayerOn)
            {
                isWaiting = false;
                timer = 0f;
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;

                audioSource.Play();
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

            audioSource.Stop();
        }
    }

        // --- プレイヤーの接触判定と親子関係の切り替え ---
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
            isPlayerOn = true;

           
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.parent == transform)
            {
                collision.transform.SetParent(null);
            }

            isPlayerOn = false;
        }
    }
}