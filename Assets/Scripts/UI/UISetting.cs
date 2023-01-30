using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISetting : MonoBehaviour
{
    [SerializeField] private GameObject settingScreen;

    private void Awake()
    {
        settingScreen.SetActive(true);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ChangeSoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(20);
    }
    public void ChangeMusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(20);
    }
}
