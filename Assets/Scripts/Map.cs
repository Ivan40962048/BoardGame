using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Map : MonoBehaviour
{
    private Dictionary<GameObject, bool> _availableHexes;
    
    private void Start()
    {
        _availableHexes = new Dictionary<GameObject, bool>();
        foreach (var hex in GameObject.FindGameObjectsWithTag("Hex"))
            _availableHexes.Add(hex, true);
    }

    public bool CheckAvailabilityHex(GameObject hex)
        {
            return _availableHexes[hex];
        }

    public void MakeHexUnavailable(GameObject hex)
    {
        _availableHexes[_availableHexes.FirstOrDefault(availableHex => availableHex.Key == hex).Key] = false;
    }
}
