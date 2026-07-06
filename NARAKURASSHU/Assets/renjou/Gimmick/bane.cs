using UnityEngine;

public class bane : MonoBehaviour
{
    [Header("ばねの跳ね返す力")]
    public float bounceForce = 15f;

    [SerializeField] private AudioClip soundToPlay;

    private AudioSource aoudioSource;



    private void Start()
    {
        aoudioSource = GetComponent<AudioSource>();
        if (aoudioSource != null)
        {
            aoudioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                Vector2 currentVelocity = playerRb.linearVelocity;
                currentVelocity.y = 0;
                playerRb.linearVelocity = currentVelocity;

                playerRb.AddForce(transform.up * bounceForce, ForceMode2D.Impulse);

                
            }

            if (soundToPlay != null && aoudioSource != null)
            {
                aoudioSource.PlayOneShot(soundToPlay);
            }

        }
    }
}