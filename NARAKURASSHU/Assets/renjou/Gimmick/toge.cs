using UnityEngine;

public class toge : MonoBehaviour
{
    private PlayerControl playerControl;
    public int damage = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
           
        }
    }

}
