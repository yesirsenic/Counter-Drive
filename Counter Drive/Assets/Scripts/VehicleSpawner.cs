using System.Collections;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
    [Header("Refs")]
    public VehicleBasket vehicleBasket;

    [Header("Spawn Points (4)")]
    public Transform[] spawnPoints;

    [SerializeField]
    private GameObject SpawnComp;

    public bool SpawnAt(int index)
    {
        if (index < 0 || index >= spawnPoints.Length)
            return false;

        VehicleEntry entry = vehicleBasket.GetRandomAvailableVehicle();
        if (entry == null)
            return false;

        GameObject spawnCar = Instantiate(entry.prefab,
                    spawnPoints[index].position,
                    Quaternion.identity);

        spawnCar.transform.SetParent(SpawnComp.transform);

        vehicleBasket.Consume(entry);

        if (vehicleBasket.IsEmpty())
        {
            Debug.Log("차량 바구니가 모두 비었습니다!");
        }

        GameManager.Instance.aliveCount++;

        return true;
    }

    public void StartRepeatSpawn()
    {
        StartCoroutine(RepeatSpawn());
    }

    IEnumerator RepeatSpawn()
    {

        while(!vehicleBasket.IsEmpty())
        {
            int randomNum = Random.Range(0, 4);

            SpawnAt(randomNum);

            yield return new WaitForSeconds(vehicleBasket.RepeatInterval);
        }
  
    }


    
}
