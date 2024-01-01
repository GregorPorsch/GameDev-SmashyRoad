using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class CarSelection : MonoBehaviour
{
    [Header ("Navigation Buttons")]
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;

    [Header("Play/Buy Buttons")]
    [SerializeField] private Button play;
    [SerializeField] private Button buy;
    [SerializeField] private Text priceText;

    [Header("Car Attributes")]
    [SerializeField] private int[] carPrices;
    private int currentCar;

    [Header("Sound")]
    [SerializeField] private AudioClip purchase;
    private AudioSource source;
    public GameObject musicPlayer;

    private void Start()
    {
        musicPlayer.GetComponent<AudioSource>().volume = Globals.musicVolume;
        source = GetComponent<AudioSource>();
        currentCar = Globals.carSelected;
        GameManager.playerCarNumber = Globals.carSelected;
        SelectCar(currentCar);
        Globals.SaveData();
        UpdateUI();

        if (currentCar == 0)
        {
            play.gameObject.SetActive(true);
            buy.gameObject.SetActive(false);
        }
    }

    private void SelectCar(int _index)
    {
        if (currentCar == 0)
        {
            play.gameObject.SetActive(true);
            buy.gameObject.SetActive(false);
        }
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(i == _index);

        UpdateUI();
        Globals.SaveData();
    }
    private void UpdateUI()
    {
        if (currentCar == 0)
        {
            play.gameObject.SetActive(true);
            buy.gameObject.SetActive(false);
        }
        //If current car unlocked show the play button
        if (Globals.carsUnlocked[currentCar])
        {
            play.gameObject.SetActive(true);
            buy.gameObject.SetActive(false);
        }
        //If not show the buy button and set the price
        else
        {
            play.gameObject.SetActive(false);
            buy.gameObject.SetActive(true);
            priceText.text = carPrices[currentCar] + "$";
        }
        Globals.SaveData();
    }

    private void Update()
    {
        if (currentCar == 0)
        {
            play.gameObject.SetActive(true);
            buy.gameObject.SetActive(false);
        }
        //Check if we have enough money
        if (buy.gameObject.activeInHierarchy)
            buy.interactable = (Globals.money >= carPrices[currentCar]);
    }

    public void ChangeCar(int _change)
    {
        if (currentCar == 0)
        {
            play.gameObject.SetActive(true);
            buy.gameObject.SetActive(false);
        }
        currentCar += _change;

        if (currentCar > transform.childCount - 1)
        {
            currentCar = 0;
        }
        else if (currentCar < 0)
        {
            currentCar = transform.childCount - 1;
        }

        Globals.carSelected = currentCar;
        SelectCar(currentCar);
        GameManager.playerCarNumber = currentCar;
        Globals.SaveData();
    }
    public void BuyCar()
    {
        Globals.money -= carPrices[currentCar];
        Globals.carsUnlocked[currentCar] = true;
        source.PlayOneShot(purchase);
        UpdateUI();
        Globals.SaveData();
    }
}