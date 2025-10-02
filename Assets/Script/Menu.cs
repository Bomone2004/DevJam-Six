using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
