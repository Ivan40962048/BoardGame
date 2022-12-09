using UnityEngine;
using UnityEngine.SceneManagement;

public class StatisticScreen : MonoBehaviour
{
    private void GoToMenu()
    {
        var canvases = GameObject.FindObjectsOfType<Canvas>();
        foreach (var canvas in canvases)
            Destroy(canvas);
        SceneManager.LoadScene("MainMenu");
    }
}