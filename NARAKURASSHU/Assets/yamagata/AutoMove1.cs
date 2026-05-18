using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    public float speed = 1.0f;
    private int direction = 1;

    void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("当たる");

        // wall タグに当たった時だけ反転

        if (collision.gameObject.CompareTag("wall"))
        {
            direction *= -1;
        }



        // キャラの向きを反転

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;



    // プレイヤーの攻撃に当たったら消える
    if(collision.gameObject.CompareTag("PlayerAttack"))
       {
          Destroy(gameObject);
       }
   }
}