using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int playerCarNumber = 0;
    public GameObject miniMap;
    public GameObject[] playerCars;
    public GameObject musicPlayer;
    public Button informationButton;
    public GameObject winScreen;
    public Text winText;
    public Text winTimeText;

    bool informationRead = true;
    bool moneyGiven = false;
    AudioSource policeSiren;

   void Start()
    {
        Debug.Log(Globals.informationRead);
        policeSiren = GetComponent<AudioSource>();
        policeSiren.volume = Globals.sfxVolume;
        Instantiate(playerCars[playerCarNumber], new Vector3(0, 0, 0), Quaternion.identity);
        musicPlayer.GetComponent<AudioSource>().volume = Globals.musicVolume;
        winScreen.SetActive(false);
        miniMap.SetActive(false);
        Globals.isRunning = false;
        moneyGiven = false;
        if (!Globals.informationRead) informationButton.gameObject.SetActive(true);
        else InformationButtonClicked();
    }

    private void Update()
    {
        if (Globals.isRunning)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
        if (!informationRead) Time.timeScale = 0;
        else Time.timeScale = 1;
        checkWin();
        /*if (Input.GetKeyDown(KeyCode.E))
        {
            Globals.bags = 10;
        }*/
    }

    public void InformationButtonClicked()
    {
        informationButton.gameObject.SetActive(false);
        informationRead = true;
        miniMap.SetActive(true);
        Globals.isRunning = true;
        Globals.informationRead = true;
        Globals.SaveData();
    }

    public void checkWin()
    {
        if (Globals.bags == 10)
        {
            endGame(true);
        }
    }

    public void endGame(bool win)
    {
        Globals.isRunning = false;
        miniMap.SetActive(false);
        if (win)
        {
            winText.text = "You won!";
        }
        else {
            winText.text = "You lost!";
        }

        Time.timeScale = 0;
        winScreen.SetActive(true);
        // winBagsText.text = Globals.bags.ToString() + "/10";

        if ((Globals.seconds - Globals.minutes * 60) < 10 && Globals.minutes < 10)
        {
            winTimeText.text = "0" + Globals.minutes.ToString() + ":" + "0" + (Globals.seconds - Globals.minutes * 60).ToString();
        }
        else if (Globals.seconds < 10) winTimeText.text = Globals.minutes.ToString() + ":" + "0" + (Globals.seconds - Globals.minutes * 60).ToString();
        else if (Globals.minutes < 10) winTimeText.text = "0" + Globals.minutes.ToString() + ":" + (Globals.seconds - Globals.minutes * 60).ToString();
        else winTimeText.text = Globals.minutes.ToString() + ":" + Globals.seconds.ToString();

        if (win && (Globals.seconds < Globals.highscore || Globals.highscore == 0)) Globals.highscore = Globals.seconds;
        Debug.Log("Seconds: " + Globals.seconds);
        Debug.Log(Globals.highscore);
        if (!moneyGiven)
        {
            Globals.money += Globals.bags * 10;
            moneyGiven = true;
        }
        Globals.SaveData();
    }
}
