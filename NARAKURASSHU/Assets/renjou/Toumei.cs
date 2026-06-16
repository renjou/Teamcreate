using UnityEngine;

public class Toumei : MonoBehaviour
{
    void Start()
    {
        // 描画機能をオフにする
        if (TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer))
        {
            meshRenderer.enabled = false;
        }
    }
}