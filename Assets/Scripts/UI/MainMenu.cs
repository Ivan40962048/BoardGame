using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void ExitGame() => Application.Quit();
    private void GoSetOptionsGame() => SceneManager.LoadScene("GameOptions");
    private void StartTutorial() => SceneManager.LoadScene("Tutorial");
}