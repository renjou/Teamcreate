using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

  public static CameraShake Instance { get; private set; }
    private Vector3  originalPosition;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
   private void OnEnable()
    {
        originalPosition = transform.localPosition;
    }
    private void Shake(float duration, float magnitude)
    {
        StopAllCoroutines();
        StartCoroutine(DoShake(duration, magnitude));
    }
    private IEnumerator DoShake(float duration, float magnitude)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            // ランダムな位置を計算（2DなのでZ軸は固定）
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;

            // 1フレーム待つ
            yield return null;
        }

       
        transform.localPosition = originalPosition;
        // Update is called once per frame
    }
}
