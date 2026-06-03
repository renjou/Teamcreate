using UnityEngine;

public class BossAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collison)
    {
        if (collison.CompareTag("Player"))
        {
            Debug.Log("プレイヤーにヒット");

            PlayerControl player = FindFirstObjectByType<PlayerControl>();
            player.PlayerDamage();
            player.SpeGaugeIncrease();
            player.KnockBack(transform.position);
        }
    }
}