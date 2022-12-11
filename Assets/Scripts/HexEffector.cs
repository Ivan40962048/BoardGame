using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class HexEffector : MonoBehaviour
{
    private event Action<Player, PlayersStatistic> OnWater;
    private event Action<Player, PlayersStatistic> OnPasture;
    private event Action<Player, PlayersStatistic> OnLake;
    private event Action<Player, PlayersStatistic> OnRiver;
    private event Action<Player, PlayersStatistic> OnStone;
    private event Action<Player, PlayersStatistic> OnFinish;
    private void Start()
    {
        OnWater += (player, playersStatistic) => 
        {
            playersStatistic.statistic[player.nickName].Score += 5;
            player.needSkipMove = true;
        };
        OnPasture += (player, playersStatistic) => playersStatistic.statistic[player.nickName].Score += DiceRoll.DiceValue;
        OnLake += (player, playersStatistic) => 
        {
            playersStatistic.statistic[player.nickName].Score = Mathf.RoundToInt(playersStatistic.statistic[player.nickName].Score * 1.4f);
        };
        OnRiver += (player, playersStatistic) => 
        {
            playersStatistic.statistic[player.nickName].RiversVisited++;
            PlayersQueue.queue.Remove(player.number);
            PlayersQueue.queue.AddFirst(player.number);
        };
        OnStone += (player, playersStatistic) => 
        {
            playersStatistic.statistic[player.nickName].Score++;
            player.debuff = 3;
        };
        OnFinish += (player, playersStatistic) => playersStatistic.statistic[player.nickName].Score += 7;
    }

    public void ApplyHexEffect(Player player, GameObject hex, PlayersStatistic statistic)
    {
        if (hex.name != "Start")
            hex.transform.Find("HexTop").GetComponent<MeshRenderer>().material.color -= new Color(0.6f, 0.6f, 0.6f);
        else
            return;
        statistic.statistic[player.nickName].MovesCount++;
        if (hex.name.Contains("Water"))
            OnWater.Invoke(player, statistic);
        if (hex.name.Contains("Pasture"))
            OnPasture.Invoke(player, statistic);
        if (hex.name.Contains("Lake"))
            OnLake.Invoke(player, statistic);
        if (hex.name.Contains("River"))
            OnRiver.Invoke(player, statistic);
        if (hex.name.Contains("Stone"))
            OnStone.Invoke(player, statistic);
        if (hex.name.Contains("Finish"))
            OnFinish.Invoke(player, statistic);
    }
}
