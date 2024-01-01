using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public GameObject loadScreen;
    public GameObject UI;

    private void Start()
    {
        loadScreen.SetActive(false);
    }
    public void LoadGameScene()
    {
        loadScreen.SetActive(true);
        Globals.SaveData();
        UI.SetActive(false);
        SceneManager.LoadScene(1);
    }

    public void LoadMenuScene()
    {
        UI.SetActive(false);
        if (!Globals.carsUnlocked[Globals.carSelected])
        {
            Globals.carSelected = 0;
            GameManager.playerCarNumber = 0;
        }
        Globals.SaveData();
        SceneManager.LoadScene(0);
    }
}