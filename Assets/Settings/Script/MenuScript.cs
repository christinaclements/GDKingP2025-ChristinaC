using System.Collections;
using UnityEngine;

public class MenuScript : MonoBehaviour{
    public void goToGame() {
        StartCoroutine(WaitForSoundAndTransition("MainGame"));
    }
    private IEnumerator WaitForSoundAndTransition(string sceneName) {
        AudioSource source = GetComponentInChildren<AudioSource>();
        source.Play();
        yield return new WaitForSeconds(source.clip.length);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
    //public void GoToGameOver() {
        //UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    //} 
    //public void GoTo
}
