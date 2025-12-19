using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class LocationManager : MonoBehaviour
{
    public static LocationManager Instance;

    [System.Serializable]
    public struct LocationEntry
    {
        public string id;
        public Transform targetTransform;
    }

    public List<LocationEntry> locations;

    void Awake()
    {
        Instance = this;
    }

    public Transform GetLocation(string id)
    {
        var loc = locations.FirstOrDefault(x => x.id == id);
        return loc.targetTransform;
    }
}