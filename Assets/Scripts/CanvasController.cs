using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    
    public GameObject playPanel, settingPanel, helpPanel, creditPanel, levelPanel;
    [Header("AudioButton")]
    public Button audioButton;
    public AudioSource audioSource;
    public Slider audioSlider;
    void Start()
    {
        HidelAllPanel();
        audioSource.volume = 0.5f;
        audioSlider.value = audioSource.volume;
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void AuidoSetting()
    {
        if (audioSource.volume > 0)
        {
            audioSource.volume = 0;
            audioButton.GetComponent<Image>().color = Color.blue;
            audioButton.transform.GetChild(0).GetComponent<Text>().text = "CLOSE";
        }
        else
        {
            audioSource.volume = audioSlider.value;
            audioButton.GetComponent<Image>().color = Color.white;
            audioButton.transform.GetChild(0).GetComponent<Text>().text = "OPEN";
        }
    }

    public void AudioSlider()
    {
        audioSource.volume = audioSlider.value;
    }

    public void PlayGame()
    {
        HidelAllPanel();
        levelPanel.SetActive(true);
    }

    public void Setting()
    {
        HidelAllPanel();
        settingPanel.SetActive(true);
    }

    public void Help()
    {
        HidelAllPanel();
        helpPanel.SetActive(true);
    }
    public void Credit()
    {
        HidelAllPanel();
        creditPanel.SetActive(true);
    }
    public void HidelAllPanel()
    {
        GameObject[] panls = { playPanel, settingPanel, helpPanel, creditPanel, levelPanel };
        foreach (GameObject panel in panls)
        {
            panel.SetActive(false);
        }

        playPanel.SetActive(true);
    }

    void Update()
    {

    }
}
