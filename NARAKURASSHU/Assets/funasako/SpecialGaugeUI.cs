using UnityEngine;
using UnityEngine.UI;

public class SpecialGaugeUI : MonoBehaviour
{
    public Image nowSprite;
    public Sprite empty;
    public Sprite one;
    public Sprite two;
    public Sprite three;
    public Sprite four;
    public Sprite full;

    public PlayerControl playerControl;

    public void SpeGaugeUpdate(float speGauge)
    {
        if (speGauge >= 5)
        {
            nowSprite.sprite = full;
        }
        switch (speGauge)
        {
            case 0:
                nowSprite.sprite = empty;
                break;
            case 1:
                nowSprite.sprite = one;
                break;
            case 2:
                nowSprite.sprite = two;
                break;
            case 3:
                nowSprite.sprite = three;
                break;
            case 4:
                nowSprite.sprite = four;
                break;
        }
    }
}
