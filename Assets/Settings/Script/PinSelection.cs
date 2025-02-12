using UnityEngine;

public class PinSelection : MonoBehaviour{
    public void goToMenu(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
