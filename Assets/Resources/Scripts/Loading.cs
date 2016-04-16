using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Loading : MonoBehaviour
{
    public Slider slider;
    AsyncOperation syncOperation;
    string levelName = "";

    void Start()
    {
        levelName = PlayerPrefs.GetString("LoadLevel");
        SetOperation();
    }

    void SetOperation()
    {
        if (levelName != null && levelName != string.Empty)
            syncOperation = Application.LoadLevelAsync(levelName);
    }

    void Update()
    {
        Load();
    }

    void Load()
    {
        if (syncOperation.progress < 0.9f)
            slider.value = syncOperation.progress;
        else
            Application.LoadLevel(levelName);
    }
}
