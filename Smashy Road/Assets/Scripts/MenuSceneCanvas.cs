using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuSceneCanvas : MonoBehaviour
{
    [SerializeField] public Slider musicSlider;
    [SerializeField] public Slider sfxSlider;

    public GameObject musicPlayer;
    public GameObject LoadingScreen;

    public Text highscoreText;
    public Text moneyText;
    public Animator soundPanelAnimator;

    bool soundPanelOpen = false;

    public void Awake()
    {
        Globals.LoadData();
    }

    private void Start()
    {
        Globals.isRunning = true;
        Time.timeScale = 1;
        LoadingScreen.SetActive(false);
        musicPlayer.GetComponent<AudioSource>().volume = Globals.musicVolume;
        musicSlider.value = Globals.musicVolume;
        sfxSlider.value = Globals.sfxVolume;

        musicSlider.onValueChanged.AddListener((v) =>
        {
            Globals.musicVolume = v;
            musicPlayer.GetComponent<AudioSource>().volume = v;
            Globals.musicChanged = true;
            Globals.SaveData();
        });

        sfxSlider.onValueChanged.AddListener((v) =>
        {
            Globals.sfxVolume = v;
            Globals.sfxChanged = true;
            Globals.SaveData();
        });
    }

    public void Update()
    {
        moneyText.text = Globals.money.ToString() + "$";
        if ((Globals.highscore / 60 < 10) && (Globals.highscore % 60 < 10)) highscoreText.text = "0" + (Globals.highscore / 60).ToString() + ":" + "0" + (Globals.highscore % 60).ToString();
        else if((Globals.highscore / 60 < 10)) highscoreText.text = "0" + (Globals.highscore / 60).ToString() + ":" + (Globals.highscore % 60).ToString();
        else if (Globals.highscore % 60 < 10) highscoreText.text = (Globals.highscore / 60).ToString() + ":" + "0" + (Globals.highscore % 60).ToString();
        else highscoreText.text = (Globals.highscore / 60).ToString() + ":" + (Globals.highscore % 60).ToString();
    }

    public void MenuButtonClicked()
    {
        SceneManager.LoadScene(0);
    }

    public void StartButtonClicked()
    {
        Globals.SaveData();
        LoadingScreen.SetActive(true);
        SceneManager.LoadScene(1);
    }

    public void GarageButtonClicked()
    {
        Globals.SaveData();
        // LoadingScreen.SetActive(true);
        SceneManager.LoadScene(2);
    }

    public void SoundPanelButtonClicked()
    {
        Debug.Log("Sound Button Clicked");
        if (!soundPanelOpen)
        {
            soundPanelAnimator.Play("SoundPanelOpen", 0, 0f);
            soundPanelOpen = true;
        }
        else
        {
            soundPanelAnimator.Play("SoundPanelClose", 0, 0f);
            soundPanelOpen = false;
        }
    }
}
