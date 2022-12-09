using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.Linq;

public class PlayersStatistic : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _statisticScreenText;
    public Dictionary<string, PlayerStatistic> statistic { get; private set; }

    public void Initialize(Player[] players)
    {
        statistic = new Dictionary<string, PlayerStatistic>();
        foreach (var player in players)
        {
            statistic.Add(player.nickName, new PlayerStatistic());
            player.playerMovement.OnPlayerMoved += (player, hex) => 
            {
                UpdateStatisticScreen();
                if (hex.name == "Finish")
                {
                    gameObject.SetActive(true);
                    string winnerNickName = statistic.OrderByDescending(x => x.Value.Score).First().Key;
                    _statisticScreenText.text = "Победил игрок " + winnerNickName + "\n\n" + _statisticScreenText.text;
                }
            };
        }
    }

    public void UpdateStatisticScreen()
    {
        var newStatisticScreenText = new StringBuilder();
        foreach (var playerStatistic in statistic)
        {
            newStatisticScreenText.Append(playerStatistic.Key + ":");
            newStatisticScreenText.Append("\n");
            newStatisticScreenText.Append("ходов: " + playerStatistic.Value.MovesCount + "; ");
            newStatisticScreenText.Append("очков: " + playerStatistic.Value.Score + "; ");
            newStatisticScreenText.Append("рек: " + playerStatistic.Value.RiversVisited + ".");
            newStatisticScreenText.Append("\n\n");
        }
        _statisticScreenText.text = newStatisticScreenText.ToString();
    }
}