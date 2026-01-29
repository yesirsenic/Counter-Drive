using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    List<LevelVehicleBasket> levelVehicleBaskets;

    [SerializeField]
    VehicleSpawner vehicleSpawner;

    [SerializeField]
    VehicleBasket vehicleBasket;

    private int currentLevel = 1;
    private int LevelNum = 1;

    private int minSpawnNum = 10;
    private int maxSpawnNum = 20;


    private float baseInterval = 2.0f;

    private LevelVehicleBasket currentLevelBasket;

    private void Start()
    {
        LevelSet();
    }

    private void __Init__()
    {
        vehicleBasket.vehicleEntrys.Clear();
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

        foreach(var v in currentLevelBasket.VehicleList)
        {
            int number = Random.Range(minSpawnNum, maxSpawnNum+1);

            VehicleEntry ve = new VehicleEntry();

            ve.prefab = v;
            ve.number = number;


            vehicleBasket.vehicleEntrys.Add(ve);

        }

        float interval = Mathf.Max(0.5f, baseInterval * Mathf.Pow(0.85f, currentLevel - 1));
        vehicleBasket.RepeatInterval = interval;

        vehicleSpawner.vehicleBasket = vehicleBasket;

        vehicleSpawner.StartRepeatSpawn();


    }

}
