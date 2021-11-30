using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject mainPanel;
    public void OpenCloseSettings()
    {
        SonidoManager.Instance.Play("PopUI");
        SaveVolumeSettings();
        OpenCloseMainPanel();
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }
    public void Play()
    {
        SceneManager.LoadScene("MainScene");
        SonidoManager.Instance.Stop("FondoInicio");
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void OpenCloseMainPanel()
    {
        mainPanel.SetActive(!mainPanel.activeSelf);
    }
    public void OpenClose(GameObject go)
    {
        go.SetActive(!go.activeSelf);
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
      SonidoManager.Instance.Play("FondoInicio");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
