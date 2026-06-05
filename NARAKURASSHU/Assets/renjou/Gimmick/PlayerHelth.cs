using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    public int health = 1; // 1なら即死、3なら3回耐えられる

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("プレイヤーがダメージを受けました。残り体力: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("ゲームオーバー！シーンを再読み込みします。");

        // 現在アクティブなシーンの名前を取得
        string currentSceneName = SceneManager.GetActiveScene().name;

        // そのシーンをロード（リスタート）
        SceneManager.LoadScene(currentSceneName);
    }
}