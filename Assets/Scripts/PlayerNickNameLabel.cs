using System.Collections;
using UnityEngine;

public class PlayerNickNameLabel : MonoBehaviour
{
    [SerializeField] private GameObject _nickNameLabel;
    [SerializeField] private Player _player;
    private void Start()
    {
        var mouseClickChecker = GameObject.FindObjectOfType<MouseChecker>();
        mouseClickChecker.OnCursorOnPlayer += (hit) => 
        {
            var currentPlayer = hit.collider.gameObject.GetComponent<Player>();
            if (_nickNameLabel != null)
            {
                if (currentPlayer.nickName == _player.nickName)
                {
                    _nickNameLabel.SetActive(true);
                    if (PlayersQueue.CheckQueue(_player.number))
                    {
                        _nickNameLabel.GetComponent<TMPro.TextMeshPro>().color = Color.green;
                    }
                    else
                        _nickNameLabel.GetComponent<TMPro.TextMeshPro>().color = Color.white;
                }  
                else
                    _nickNameLabel.SetActive(false);
            }
        };
        mouseClickChecker.OnCursorOnHex += (hit) => 
        {
            if (_nickNameLabel != null)
                _nickNameLabel.SetActive(false);
        };
        StartCoroutine(WaitPlayersInitialization());
    }
    private IEnumerator WaitPlayersInitialization()
    {
        yield return new WaitForSeconds(0.01f);
        _nickNameLabel.GetComponent<TMPro.TextMeshPro>().text = _player.nickName;
        _nickNameLabel.SetActive(false);
        yield break;
    }
}
