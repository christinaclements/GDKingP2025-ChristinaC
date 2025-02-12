using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public void goToGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }
    public void goToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    public void goToPinSelection()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("PinSelection");
    }
}
