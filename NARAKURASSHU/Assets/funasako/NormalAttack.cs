using UnityEngine;

public class NormalAttack : MonoBehaviour
{
    public Transform player;
    public Transform attackPoint;
    PlayerControl playerControl;
    Boss boss;

    BoxCollider2D attackCollider;

    void Start()
    {
        Debug.Log("NormalAttack Start");
        attackCollider = GetComponent<BoxCollider2D>();
        playerControl = FindFirstObjectByType<PlayerControl>();
        boss = FindFirstObjectByType<Boss>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(playerControl);
        Debug.Log(boss);
        if (collision.CompareTag("enemy"))
        {
            Debug.Log("ヒット");
            playerControl.SpeGaugeIncrease();
            boss.TakeDamage(10);
        }

    }
     

    public void AttackOn()
    {
        attackCollider.enabled = true;
    }

    public void AttackOff()
    {
        attackCollider.enabled = false;
    }
}
