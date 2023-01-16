using UnityEngine;

public class PossibleHexChecker : MonoBehaviour
{
    private Map _hexes;

    private void Start()
    {
        _hexes = GameObject.FindObjectOfType<Map>();
    }
    public bool CheckPossibleHex(Player player, GameObject hex)
    {
        var possibleStepsCount = Mathf.Clamp(DiceRoll.DiceValue - player.debuff, 1, 6);
        var playerPosition = player.transform.position;
        if (!_hexes.CheckAvailabilityHex(hex))
            return false;
        var hexPosition = hex.transform.position;
        var xScore = Mathf.Round(Mathf.Abs(hexPosition.x - playerPosition.x) / 0.75f);
        if (xScore > possibleStepsCount)
            return false;
        var zScore = Mathf.Round(Mathf.Abs(hexPosition.z - playerPosition.z) / (Mathf.Sqrt(0.75f)/2));
        if (xScore == possibleStepsCount && zScore <= possibleStepsCount)
            return (true);
        if ((xScore + zScore)/2 <= possibleStepsCount && hex.name == "Finish")
            return true;
        return (xScore + zScore)/2 == possibleStepsCount;
    }
}
