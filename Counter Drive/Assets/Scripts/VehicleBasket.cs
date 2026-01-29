using UnityEngine;
using System.Collections.Generic;

public class VehicleBasket : MonoBehaviour
{
    public List<VehicleEntry> vehicleEntrys;
    public float RepeatInterval;

    public VehicleEntry GetRandomAvailableVehicle()
    {
        List<VehicleEntry> available = new List<VehicleEntry>();

        foreach (var entry in vehicleEntrys)
        {
            if (entry.number > 0)
                available.Add(entry);
        }

        if (available.Count == 0)
            return null;

        int rand = Random.Range(0, available.Count);
        return available[rand];
    }

    public void Consume(VehicleEntry entry)
    {
        entry.number = Mathf.Max(0, entry.number - 1);
    }

    public bool IsEmpty()
    {
        foreach (var entry in vehicleEntrys)
        {
            if (entry.number > 0)
                return false;
        }
        return true;
    }

}
