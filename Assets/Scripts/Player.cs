using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public int number;
    public PlayerMovement playerMovement;
    public bool isSelected;
    public bool needSkipMove;
    public int debuff;
    public string nickName;

    private void Start()
    {
        var mouseClickChecker = GameObject.FindObjectOfType<MouseChecker>();
        mouseClickChecker.OnMouseClickPlayer += (hit) => 
        {
            if (hit.collider.gameObject.GetComponent<Player>().number == number)
                if (PlayersQueue.CheckQueue(number)) 
                    isSelected = true;
            else
                isSelected = false;
        };
        mouseClickChecker.OnMouseClickHex += (hit) =>
        {
            if (isSelected && PlayersQueue.CheckQueue(number))
            {
                playerMovement.MovePlayer(hit.collider.gameObject);
                debuff = 0;
            }
            isSelected = false;
        };
    }
}