using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    [SerializeField]
    List<LevelVehicleBasket> levelVehicleBaskets;

    [SerializeField]
    VehicleSpawner vehicleSpawner;

    [SerializeField]
    VehicleBasket vehicleBasket;

    [SerializeField]
    GameObject userCar;

    [SerializeField]
    GameObject Explosion;

    [SerializeField]
    GameObject GameOverPopup;

    [SerializeField]
    Text levelText;

    private int currentLevel = 1;
    private int LevelNum = 1;

    private int minSpawnNum = 2;
    private int maxSpawnNum = 5;

    private float baseInterval = 1.0f;

    [SerializeField]
    Vector3 carSpawnPos;

    private LevelVehicleBasket currentLevelBasket;

    public int aliveCount = 0;

    public bool isControl;

    public bool isGameOver;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        if(PlayerPrefs.GetInt("Level") == 0)
        {
            PlayerPrefs.SetInt("Level", 1);
        }

        currentLevel = PlayerPrefs.GetInt("Level");
        levelText.text = "LEVEL " + currentLevel;
        Time.timeScale = 1f;
        isControl = true;
        isGameOver = false;
        userCar.SetActive(true);
        LevelSet();
        Debug.Log("Current Level: " + currentLevel);
    }

    
    private void LevelSet()
    {
        int temp = currentLevel / 20;

        switch (temp)
        {
            case 0:
                LevelNum = 1;
                break;
            case 1:
                LevelNum = 2;
                break;
            case 2:
                LevelNum = 3;
                break;
            case 3:
                LevelNum = 4;
                break;
            default:
                LevelNum = 5;
                break;


        }


        currentLevelBasket = levelVehicleBaskets[LevelNum - 1];

        CarNumberSet();

    }

    private void CarNumberSet()
    {
        if (currentLevelBasket == null)
            return;

        foreach (var v in currentLevelBasket.VehicleList)
        {
            int number = Random.Range(minSpawnNum + LevelNum * 2, maxSpawnNum + 1 + LevelNum * 2);

            VehicleEntry ve = new VehicleEntry();

            ve.prefab = v;
            ve.number = number;


            vehicleBasket.vehicleEntrys.Add(ve);

        }

        float interval = Mathf.Max(0.35f, Mathf.Pow(0.9895f, currentLevel - 1));

        vehicleBasket.RepeatInterval = interval;

        vehicleSpawner.vehicleBasket = vehicleBasket;

        vehicleSpawner.StartRepeatSpawn();


    }

    private void GameClear()
    {
        currentLevel++;
        PlayerPrefs.SetInt("Level", currentLevel);
    }

    public void __Init__()
    {
        currentLevel = PlayerPrefs.GetInt("Level");
        levelText.text = "LEVEL " + currentLevel;
        Time.timeScale = 1f;
        isControl = true;
        isGameOver = false;
        vehicleBasket.vehicleEntrys.Clear();
        aliveCount = 0;
        userCar.SetActive(true);
        userCar.transform.position = carSpawnPos;
        LevelSet();

        Debug.Log("Current Level: " + currentLevel);
    }

    public void CheckClear()
    {
        if(aliveCount == 0 && vehicleBasket.IsEmpty() && !isGameOver)
        {
            Debug.Log("Clear 하였습니다.");
            isControl = false;
            GameClear();
        }
    }

    public void GameOver()
    {
        StartCoroutine(GameOverLine());
    }

    IEnumerator GameOverLine()
    {
        isControl = false;
        isGameOver = true;

        userCar.SetActive(false);

        Vector3 spawnPos = userCar.transform.position + new Vector3(0, 1.2f, 0);

        Instantiate(Explosion, spawnPos, Quaternion.identity);

        yield return new WaitForSeconds(1f);

        Time.timeScale = 0f;

        GameOverPopup.SetActive(true);


    }

}
