using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; // 最大体力
    private int currentHealth;

    private PlayerRespawn playerRespawn;

    void Start()
    {
        // ゲーム開始時に現在の体力を満タンにする
        currentHealth = maxHealth;

        // 同じオブジェクトからPlayerRespawnスクリプトを探して紐付ける
        playerRespawn = GetComponent<PlayerRespawn>();

        if (playerRespawn == null)
        {
            Debug.LogError("プレイヤーに 'PlayerRespawn' スクリプトがアタッチされていません！");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("プレイヤーがダメージを受けました。残り体力: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("体力が0になりました。チェックポイントからリスポーンします。");

        if (playerRespawn != null)
        {
            // PlayerRespawnの死亡・復活処理を呼び出す
            playerRespawn.TriggerDeath();

            // リスポーンしたら体力を全回復させる
            currentHealth = maxHealth;
        }
        else
        {
            // 万が一PlayerRespawnが見つからない時のバックアップ（シーンリロード）
            Debug.LogWarning("PlayerRespawnスクリプトが見つかりません！シーンをリロードします。");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}