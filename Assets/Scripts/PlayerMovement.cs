using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rb;
    private Player _player;
    private Map _map;
    private PossibleHexChecker _possibleHexChecker;
    [SerializeField] private AnimationCurve _motionPath;
    [SerializeField] private float _figureMovementSpeed = 0.1f;

    public event Action<Player, GameObject> OnPlayerMoved;
    private void Start()
    {
        _player = GetComponent<Player>();
        _rb = GetComponent<Rigidbody>();
        _possibleHexChecker = GameObject.FindObjectOfType<PossibleHexChecker>();
        _map = GameObject.FindObjectOfType<Map>();
    }

    internal void MovePlayer(GameObject targetHex)
    {
        if (_possibleHexChecker.CheckPossibleHex(_player, targetHex))
            StartCoroutine(MovePlayerFigure(targetHex));
    }
    private IEnumerator MovePlayerFigure(GameObject targetHex)
    {
        var timeStart = Time.time;
        transform.position = new Vector3(transform.position.x, 4, transform.position.z);
        while (true)
        {
            _rb.velocity = Vector3.zero;
            var targetHexPosition = targetHex.transform.position;
            var nextPoint = Vector3.MoveTowards(transform.position, targetHexPosition, _figureMovementSpeed);
            transform.position = (new Vector3(nextPoint.x, _motionPath.Evaluate((Time.time - timeStart) * 20), nextPoint.z));
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), 
                                 new Vector2(targetHexPosition.x, targetHexPosition.z)) < 0.01f)
                yield break;
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Hex" && _map.CheckAvailabilityHex(other.gameObject))
            OnPlayerMoved?.Invoke(gameObject.GetComponent<Player>(), other.gameObject);
    }
}
