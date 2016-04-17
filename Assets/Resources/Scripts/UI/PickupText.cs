using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PickupText : MonoBehaviour
{
    public static PickupText instance;
    public Text text;
    public int value;

    Pickups pickup;

    string prefix;

    bool show;
    float fadeTime;


    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        FadeTimer();
        if (show)
            ValueText();
    }

    public void AddValue(Pickups pickup, int value, string prefix)
    {
        show = true;
        fadeTime = 1;

        if (this.pickup == pickup)
            this.value += value;
        else
            this.value = value;

        this.pickup = pickup;
        this.prefix = prefix;
    }

    void FadeTimer()
    {
        fadeTime -= Time.smoothDeltaTime;

        if (fadeTime < 0)
        {
            value = 0;
            text.color = new Color(0, 0, 0, 0);
            show = false;
        }
    }

    void ValueText()
    {
        switch (pickup)
        {
            case Pickups.Health:
                text.color = Color.magenta;
                text.text = prefix + value;
                break;
            case Pickups.Money:
                text.color = value >= 0 ? Color.green : Color.red;
                text.text = prefix + value;
                break;
            case Pickups.Score:
                text.color = Color.yellow;
                text.text = prefix + value;
                break;
        }
    }
}