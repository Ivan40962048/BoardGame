using UnityEngine;
using System;

public class MouseChecker : MonoBehaviour
{
    public event Action<RaycastHit> OnMouseClickPlayer;
    public event Action<RaycastHit> OnMouseClickHex;
    public event Action<RaycastHit> OnCursorOnHex;
    public event Action<RaycastHit> OnCursorOnPlayer;

    private void Update()
    {
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.collider.tag == "Player")
                        OnMouseClickPlayer.Invoke(hit);
                    if (hit.collider.tag == "Hex")
                        OnMouseClickHex.Invoke(hit);
                }
                if (hit.collider.tag == "Hex")
                        OnCursorOnHex?.Invoke(hit);
                if (hit.collider.tag == "Player")
                        OnCursorOnPlayer?.Invoke(hit);
            }
        }
    }
}
