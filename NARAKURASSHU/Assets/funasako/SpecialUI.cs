using UnityEngine;
using UnityEngine.UI;

public class SpecialUI : MonoBehaviour
{
    GameObject _specialGauge;

    void Start()
    {
        _specialGauge = GameObject.Find("Gauge");
    }

    public void increaseSpeGauge()
    {
        _specialGauge.GetComponent<Image>().fillAmount += 0.2f;
    }

    public void ressetSpeGauge()
    {
        _specialGauge.GetComponent<Image>().fillAmount = 0;
    }
}
