using UnityEngine;

public class NormalAttack : MonoBehaviour
{
    public Transform player;
    public Transform attackPoint;
    PlayerControl playerControl;

    BoxCollider2D attackCollider;

    void Start()
    {
        attackCollider = GetComponent<BoxCollider2D>();
        playerControl = GetComponent<PlayerControl>();

    }

    void Update()
    {
        transform.position = attackPoint.position;
        Debug.Log(transform.position);
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
