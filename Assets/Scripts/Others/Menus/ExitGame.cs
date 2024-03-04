using UnityEngine;

internal class ExitGame : MonoBehaviour {
    public void QuitGame() {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
