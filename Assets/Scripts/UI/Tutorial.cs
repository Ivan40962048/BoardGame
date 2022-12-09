using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public void GoToMenu() => SceneManager.LoadScene("MainMenu");
}