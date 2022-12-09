using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private HexEffector _hexEffector;
    private Map _map;
    private Player[] _players;
    private LinkedList<int> _queue;
    private PlayersStatistic _statistic;
    private GameOptions _gameOptions;

    private void Awake()
    {
        _gameOptions = (GameOptions)GameObject.FindObjectOfType<GameOptions>();
        _gameOptions.gameObject.SetActive(false);
    }

    private void Start()
    {
        _hexEffector = GetComponent<HexEffector>();
        _map = GameObject.FindObjectOfType<Map>();
        var playerGameObjects = GameObject.FindGameObjectsWithTag("Player");
        new PlayersQueue();
        _players = new Player[_gameOptions.players.Count];
        _queue = PlayersQueue.queue;

        InitializePlayers(playerGameObjects);

        _statistic = GameObject.Find("StatisticScreen").GetComponent<PlayersStatistic>();
        _statistic.Initialize(_players);
    }

    private void InitializePlayers(GameObject[] playerGameObjects)
    {
        for (int i = 0; i < playerGameObjects.Length; i++)
        {
            if (i < _gameOptions.players.Count)
            {
                _players[i] = playerGameObjects[i].GetComponent<Player>();
                _players[i].number = i;
                _players[i].nickName = _gameOptions.players[i].Item2;
                _queue.AddLast(i);
            }
            else
                Destroy(playerGameObjects[i].gameObject);
        }
        var que = "";
                            foreach(var q in _queue)
                                que += q;
                            Debug.Log(que);
        foreach (var player in _players)
        {
            player.playerMovement.OnPlayerMoved += (playerComponent, hex) => 
            {
                _queue.Remove(player.number);
                _queue.AddLast(player.number);
                _hexEffector.ApplyHexEffect(playerComponent, hex, _statistic);
                _map.MakeHexUnavailable(hex);
                var queueCopy = new LinkedList<int>(_queue);
                foreach (var playerNumber in queueCopy)
                    if (_players[playerNumber].needSkeepMove)
                        {
                            _queue.AddLast(playerNumber);
                            _queue.RemoveFirst();
                            _players[playerNumber].needSkeepMove = false;
                        }
                    else
                        break;
            };
        }
    }
}
