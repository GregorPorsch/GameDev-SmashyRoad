using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Globals : MonoBehaviour
{
    public static float musicVolume;
    public static float sfxVolume;
    public static int highscore;
    public static int money;
    public static int time;
    public static int carSelected;
    public static bool[] carsUnlocked = { true, true, true };
    public static bool musicChanged = false;
    public static bool sfxChanged = false;
    public static bool informationRead = false;

    public static int bags = 0;
    public static int minutes;
    public static int seconds;
    public static bool isRunning;

    public static void SaveData()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SfxVolume", sfxVolume);
        PlayerPrefs.SetInt("Highscore", highscore);
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.SetInt("CarSelected", carSelected);
        PlayerPrefs.SetInt("CarsUnlocked0", boolToInt(carsUnlocked[0]));
        PlayerPrefs.SetInt("CarsUnlocked1", boolToInt(carsUnlocked[1]));
        PlayerPrefs.SetInt("CarsUnlocked2", boolToInt(carsUnlocked[2]));
        PlayerPrefs.SetInt("MusicChanged", boolToInt(musicChanged));
        PlayerPrefs.SetInt("SFXChanged", boolToInt(sfxChanged));
        PlayerPrefs.SetInt("InformationRead", boolToInt(informationRead));
    }

    public static void LoadData()
    {
        musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        sfxVolume = PlayerPrefs.GetFloat("SfxVolume");
        highscore = PlayerPrefs.GetInt("Highscore");
        money = PlayerPrefs.GetInt("Money");
        carSelected = PlayerPrefs.GetInt("CarSelected");
        carsUnlocked[0] = intToBool(PlayerPrefs.GetInt("CarsUnlocked0"));
        carsUnlocked[1] = intToBool(PlayerPrefs.GetInt("CarsUnlocked1"));
        carsUnlocked[2] = intToBool(PlayerPrefs.GetInt("CarsUnlocked2"));
        musicChanged = intToBool(PlayerPrefs.GetInt("MusicChanged"));
        sfxChanged = intToBool(PlayerPrefs.GetInt("SFXChanged"));
        informationRead = intToBool(PlayerPrefs.GetInt("InformationRead"));
        if (!musicChanged) musicVolume = 0.8f;
        if (!sfxChanged) sfxVolume = 0.8f;
    }

    public static int boolToInt(bool x)
    {
        if (x) return 1;
        else return 0;
    }

    public static bool intToBool(int x)
    {
        if (x == 0) return false;
        else return true;
    }
}
