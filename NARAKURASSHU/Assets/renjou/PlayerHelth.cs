using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; // 最大体力
    private int currentHealth;

    private PlayerRespawn playerRespawn;
    public int health = 1; // 1なら即死、3なら3回耐えられる

    void Start()
    {
        // ゲーム開始時に現在の体力を満タンにする
        currentHealth = maxHealth;

        // 【重要】同じオブジェクトからPlayerRespawnスクリプトを探して紐付ける
        playerRespawn = GetComponent<PlayerRespawn>();

        if (playerRespawn == null)
        {
            Debug.LogError("プレイヤーに 'PlayerRespawn' スクリプトがアタッチされていません！");
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("プレイヤーがダメージを受けました。残り体力: " + health);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("。チェックポイントからリスポーンします。");

        if (playerRespawn != null)
        {
            // シーンリロードではなく、位置を戻す関数を呼ぶ
            playerRespawn.Respawn();

            // リスポーンしたら体力を全回復させる
            currentHealth = maxHealth;
        }
        else
        {
            // 万が一PlayerRespawnが見つからない時のバックアップ（シーンリロード）
            Debug.LogWarning("PlayerRespawnスクリプトが見つかりません！");
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
            );
        }
    }
}