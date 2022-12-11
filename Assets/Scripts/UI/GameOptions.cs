using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOptions : MonoBehaviour
{
    private int _possibleNumberOfPlayers = 5;
    [SerializeField] private TMPro.TextMeshProUGUI _playersList;
    [SerializeField] private TMPro.TextMeshProUGUI _playerNameInput;
    public List<(int, string)> players { get; private set; }
    private void Start()
    {
        DontDestroyOnLoad(this);
        players = new List<(int, string)>();
    }

    private void AddPlayer()
    {
        if (players.Count < _possibleNumberOfPlayers && _playerNameInput.text.Length != 0)
            players.Add((players.Count, _playerNameInput.text));
    }

    private void DeletePlayer()
    {
        if (players.Count > 0)
            players.RemoveAt(players.Count - 1);
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    private void GoToMainMenu()
    {
        Destroy(GameObject.Find("Canvas"));;
        SceneManager.LoadScene("MainMenu");
    }

    private void RefreshPlayersNameList()
    {
        _playersList.text = "Список игроков:\n";
        foreach (var player in players)
            _playersList.text += player.Item2 + "\n";
    }
}