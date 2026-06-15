using UnityEngine;
using UnityEngine.InputSystem;

public class enemy2 : MonoBehaviour
{
    public int EnemyHp = 3;

    private Animator anim;

    private Vector3 baseScale;

    public GameObject bulletPrefab;
    public Transform firePoint;

    public float shotInterval = 2f;

    private float shotTimer;

    public Transform player;

    public float attackRange = 8f;

    public AudioSource audioSource;

    public AudioClip shotSE;
    public AudioClip damageSE;
    public AudioClip deathSE;

    public void EnemyDamage(int damage)
    {
        audioSource.PlayOneShot(damageSE);

        EnemyHp -= damage;

        anim.SetTrigger("Damage");

        Debug.Log("Enemy HP : " + EnemyHp);

        if (EnemyHp <= 0)
        {
            Die();
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        GameObject p = GameObject.FindWithTag("Player");

        if (p != null)
        {
            player = p.transform;
        }

        baseScale = transform.localScale;

        anim = GetComponent<Animator>();

        shotTimer = shotInterval;
    }

    // Update is called once per frame
    void Update()
    {/*
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            EnemyDamage(1);
        }
        */
        LookPlayer();

        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            shotTimer -= Time.deltaTime;

            if (shotTimer <= 0)
            {
                Shoot();
                shotTimer = shotInterval;
            }
        }
    }

    void Shoot()
    {
        audioSource.PlayOneShot(shotSE);

        anim.SetTrigger("Attack");

        // 0.3秒後に弾発射
        Invoke(nameof(CreateBullet), 0.3f);
    }

    void LookPlayer()
    {
        if (player == null) return;

        float dx = player.position.x - transform.position.x;

        if (dx > 0)
        {
            transform.localScale = new Vector3(baseScale.x, baseScale.y, baseScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-baseScale.x, baseScale.y, baseScale.z);
        }
    }

    void CreateBullet()
    {
        if (player == null) return;

        GameObject bullet = Instantiate( bulletPrefab, firePoint.position, Quaternion.identity);

        Vector2 dir = (player.position - firePoint.position).normalized;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        rb.linearVelocity = dir * 5f;

        // 弾の向きをプレイヤー方向にする
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
    }


    void Die()
    {
        audioSource.PlayOneShot(deathSE);

        anim.SetTrigger("Die");

        GetComponent<Collider2D>().enabled = false;

        enabled = false;

        Destroy(gameObject, 1f);
    }
}
