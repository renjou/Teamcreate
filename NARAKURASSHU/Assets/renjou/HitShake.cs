using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class HitShake : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float shakeDur = 0f;
    [SerializeField] private float shakeMag = 0f;
    [SerializeField] private string targetTag = "enemy";

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    // 攻撃対象（敵など）に当たったかチェック
    //    if (collision.CompareTag(targetTag))
    //    {
    //        if (CameraShake.Instance != null)
    //        {
    //            CameraShake.Instance.Shake(shakeDur, shakeMag);
    //        }
    //    }
    //}
}
