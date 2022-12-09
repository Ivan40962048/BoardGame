using System.Collections;
using UnityEngine;

public class DiceMovement : MonoBehaviour 
{
    private Rigidbody _rb;
    private bool _needToDrag;
    [SerializeField] private float _cubeLiftingHeight = 2f;
    [SerializeField] private float _longClickTime = 0.2f;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            StopAllCoroutines();
            _needToDrag = false;
        }
    }

    private void OnMouseDown()
    {
        StartCoroutine(LongClickDelay());
    }
    
    private void OnMouseDrag()
    {
        if (_needToDrag)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
                transform.position = new Vector3(hit.point.x, _cubeLiftingHeight, hit.point.z);
            _rb.velocity = Vector3.zero;
        }
    }
    private IEnumerator LongClickDelay()
    {
        yield return new WaitForSeconds(_longClickTime);
        _needToDrag = true;
    }
}