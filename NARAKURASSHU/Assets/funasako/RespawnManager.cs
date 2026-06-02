using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    Dictionary<Transform, Vector3> respawnPoints = new Dictionary<Transform, Vector3>();

    // オブジェクト位置を登録
    public void Register(Transform obj)
    {
        Debug.Log("REBORN");
        if (!respawnPoints.ContainsKey(obj))
        {
            respawnPoints.Add(obj, obj.position);
        }
    }

    public void RespawnALL()
    {
        foreach (var pair in respawnPoints)
        {
            pair.Key.position = pair.Value;
        }
    }
}
