using System.Collections;
using UnityEngine;

public class DiceRoll : MonoBehaviour 
{
    public static int DiceValue { get; private set; }
    private Rigidbody _rb;
    private float _lastClickTime;
    [SerializeField] private float _maxDoubleClickTime = 0.2f;
    [SerializeField] private float _maxDiceTorque = 42f;
    [SerializeField] private Vector3 _jumpDiceForce = new Vector3(0, 7f, 0);

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        if (Time.time - _lastClickTime < _maxDoubleClickTime)
            RollDice();
        _lastClickTime = Time.time;
    }

    private void RollDice()
    {
        _rb.velocity += _jumpDiceForce;
        _rb.AddTorque(new Vector3
        (
            Random.Range(-_maxDiceTorque, _maxDiceTorque), 
            Random.Range(-_maxDiceTorque, _maxDiceTorque), 
            Random.Range(-_maxDiceTorque, _maxDiceTorque)
        ));
        StartCoroutine(WaitCubeStop());
    }

    private IEnumerator WaitCubeStop()
    {
        while (_rb.velocity != Vector3.zero)
            yield return null;
        GetDiceValue();
        yield break;
    }

    private int GetDiceValue()
    {
        var diceValue = 0;
        if (Vector3.Dot (transform.forward, Vector3.up) > 0.6f)
                diceValue = 4;
        if (Vector3.Dot (-transform.forward, Vector3.up) > 0.6f)
                diceValue = 3;
        if (Vector3.Dot (transform.up, Vector3.up) > 0.6f)
                diceValue = 6;
        if (Vector3.Dot (-transform.up, Vector3.up) > 0.6f)
                diceValue = 1;
        if (Vector3.Dot (transform.right, Vector3.up) > 0.6f)
                diceValue = 2;
        if (Vector3.Dot (-transform.right, Vector3.up) > 0.6f)
                diceValue = 5;
        return DiceValue = diceValue;
    }
}