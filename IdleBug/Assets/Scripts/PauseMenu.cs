using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
   public void PlayUIHover()
    {
        SonidoManager.Instance.Play("BotonesUI");
    }
    public GameObject pausePanel;
  public void OpenPauseMenu()
    {
        SonidoManager.Instance.Play("PopUI");
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
    public void Play()
    { SaveVolumeSettings();
        Time.timeScale = 1;
        pausePanel.SetActive(false);
       

    }
    public void Exit()
    {
        SaveVolumeSettings();
        Application.Quit();
    }
    public void OpenCloseMainPanel()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);
    }
    public float MusicVolumeSave
    {
        set
        {
            value = Mathf.Clamp(value, 0.0001f, 1);
            PlayerPrefs.SetFloat("music", value);
            if (PlayerPrefs.HasKey("music"))
            { print(PlayerPrefs.GetFloat("music")); }
        }
    }

    public float SfxVolumeSave
    {
        set
        {
            value = Mathf.Clamp(value, 0.0001f, 1);
            PlayerPrefs.SetFloat("effects", value);
        }
    }


    public Slider musicSlider;
    public Slider effectsSlider;
    public void OnMusicChange()
    {
        SonidoManager.Instance.MusicVolume = musicSlider.value;
    }

    public void OnEffectsChange()
    {
        SonidoManager.Instance.SfxVolume = effectsSlider.value;
    }


    public void SaveVolumeSettings()
    {
        MusicVolumeSave = musicSlider.value;
        SfxVolumeSave = effectsSlider.value;
    }
    // Start is called before the first frame update
    void Start()
    {
        musicSlider.value = SonidoManager.Instance.MusicVolume;
        effectsSlider.value = SonidoManager.Instance.SfxVolume;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pausePanel.activeSelf) { OpenPauseMenu(); } else { Play(); }
        }
    }
}
