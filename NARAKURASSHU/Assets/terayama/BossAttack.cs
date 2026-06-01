using UnityEngine;

public class BossAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("プレイヤーにヒット");

//            other.GetComponent<Player>().TakeDamage(10);
        }
    }
}