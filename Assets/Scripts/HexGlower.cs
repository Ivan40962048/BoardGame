using UnityEngine;

public class HexGlower : MonoBehaviour
{
    private PossibleHexChecker _possibleHexChecker;
    private MeshRenderer _glowingHexMeshRenderer;
    private Player _selectedPlayer;
    private void Start()
    {
        _possibleHexChecker = GameObject.FindObjectOfType<PossibleHexChecker>();
        var mouseChecker = GameObject.FindObjectOfType<MouseChecker>();
        mouseChecker.OnCursorOnHex += (hit) =>
        {
            var newHexRenderer = hit.collider.gameObject.transform.Find("HexTop").GetComponent<MeshRenderer>();

            if (_glowingHexMeshRenderer != newHexRenderer)
            {
                if (_glowingHexMeshRenderer != null)
                {
                    _glowingHexMeshRenderer.material.color -= Color.yellow;     
                    _glowingHexMeshRenderer = null;
                }                   
                if (_selectedPlayer != null && 
                    _selectedPlayer.isSelected && 
                    _possibleHexChecker.CheckPossibleHex(_selectedPlayer, hit.collider.gameObject))
                {
                    newHexRenderer.material.color += Color.yellow;
                    _glowingHexMeshRenderer = newHexRenderer;
                }
            }
        };
        mouseChecker.OnMouseClickPlayer += (hit) => _selectedPlayer = hit.collider.gameObject.GetComponent<Player>();
        mouseChecker.OnMouseClickHex += (hit) => _selectedPlayer = null;
    }
}