using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowText : MonoBehaviour
{
    public static ShowText instance;
    public Text text;
    public int value;

    bool isMoney;
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

    public void AddValue(int value, bool isMoney, string prefix)
    {
        show = true;
        fadeTime = 1;

        if (!this.isMoney && isMoney)
            this.value = value;
        else
            this.value += value;

        this.isMoney = isMoney;
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
        if (isMoney)
        {
            text.color = value >= 0 ? Color.green : Color.red;
        }
        else
            text.color = Color.yellow;
        text.text = prefix + value;
    }
}