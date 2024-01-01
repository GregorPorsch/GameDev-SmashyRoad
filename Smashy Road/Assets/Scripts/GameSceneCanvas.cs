using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneCanvas : MonoBehaviour
{
    public GameObject gameScreen;
    public GameObject pauseScreen;
    public GameObject deathScreen;
    public GameObject winScreen;
    public GameObject MiniMap;
    public GameObject respawnArea;
    public Text BagsTextWin;
    public Text MoneyTextWin;
    public Text BagsTextDeath;
    public Text MoneyTextDeath;
    public Text timeText;
    public Text scoreText;
    public Slider healthBarSlider;
    public float timeStart;

    Stopwatch stopwatch;
    GameObject[] player;
    bool moneyAdded = false;

    private void Start()
    {
        gameScreen.SetActive(true);
        pauseScreen.SetActive(false);
        deathScreen.SetActive(false);
        winScreen.SetActive(false);

        stopwatch = this.GetComponent<Stopwatch>();
        stopwatch.Begin();
        Time.timeScale = 1;
        timeStart = Time.time;

        player = GameObject.FindGameObjectsWithTag("player");
        Globals.bags = 0;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.P)) PauseButtonClicked();
        if (Input.GetKey(KeyCode.R)) RespawnButtonClicked();

        if (Globals.isRunning) Time.timeScale = 1;
        else Time.timeScale = 0;
        if ((stopwatch.GetSeconds() - stopwatch.GetMinutes() * 60) < 10 && stopwatch.GetMinutes() < 10)
        {
            timeText.text = "0" + stopwatch.GetMinutes().ToString() + ":" + "0" + (stopwatch.GetSeconds() - stopwatch.GetMinutes() * 60).ToString();
        }
        else if (stopwatch.GetSeconds() < 10) timeText.text = stopwatch.GetMinutes().ToString() + ":" + "0" + (stopwatch.GetSeconds() - stopwatch.GetMinutes() * 60).ToString();
        else if (stopwatch.GetMinutes() < 10) timeText.text = "0" + stopwatch.GetMinutes().ToString() + ":" + (stopwatch.GetSeconds() - stopwatch.GetMinutes() * 60).ToString();
        else timeText.text = stopwatch.GetMinutes().ToString() + ":" + stopwatch.GetSeconds().ToString();
        scoreText.text = Globals.bags.ToString() + "/10";

        Globals.minutes = stopwatch.GetMinutes();
        Globals.seconds = stopwatch.GetSeconds();

        if (Input.GetKeyDown(KeyCode.E))
        {
            RespawnButtonClicked();
        }
    }

    public void dead()
    {
        MoneyTextDeath.text = (Globals.bags * 10).ToString() + "$";
        MoneyTextWin.text = (Globals.bags * 10).ToString() + "$";
        BagsTextDeath.text = Globals.bags.ToString() + "/10";
        BagsTextWin.text = Globals.bags.ToString() + "/10";
        Debug.Log(Globals.bags);
        if (!moneyAdded)
        {
            Globals.money += Globals.bags * 10;
            moneyAdded = true;
        }
        Time.timeScale = 0;
        Globals.isRunning = false;
        gameScreen.SetActive(false);
        pauseScreen.SetActive(false);
        deathScreen.SetActive(true);
        MiniMap.SetActive(false);
        Globals.SaveData();
    }

    public void PauseButtonClicked()
    {
        Time.timeScale = 0;
        Globals.isRunning = false;
        gameScreen.SetActive(false);
        pauseScreen.SetActive(true);
        deathScreen.SetActive(false);
        MiniMap.SetActive(false);
    }

    public void ResumeButtonClicked()
    {
        Time.timeScale = 1;
        Globals.isRunning = true;
        gameScreen.SetActive(true);
        pauseScreen.SetActive(false);
        deathScreen.SetActive(false);
        MiniMap.SetActive(true);
    }

    public void RestartButtonClicked()
    {
        moneyAdded = false;
        Globals.isRunning = true;
        SceneManager.LoadScene(1);
    }

    public void MenuButtonClicked()
    {
        SceneManager.LoadScene(0);
    }

    public void RespawnButtonClicked()
    {
        Debug.Log(player[0].transform.position);
        for (int i = 0; i < player.Length; i++)
        {
            player[i].transform.position = respawnArea.transform.position;
            player[i].transform.rotation = Quaternion.identity;
        }
        player[0].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }

    public void setHealth(float health)
    {
        healthBarSlider.value = health;
    }
}
