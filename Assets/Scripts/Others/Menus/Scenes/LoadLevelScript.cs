using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

internal class LoadLevelScript : MonoBehaviour {
    [SerializeField] GameObject UI_Screen;
    [SerializeField] Slider UI_Bar;
    
    float target;
    float loadingStartTime;

    public async void LoadScene(string sceneName) {
        target = 0;
        UI_Bar.value = 0;

        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        UI_Screen.SetActive(true);

        loadingStartTime = Time.time;

        float totalLoadingTime = 4f;

        do {
            await Task.Delay(100);
            target = scene.progress;

            float elapsedLoadingTime = Time.time - loadingStartTime;
            float progressFactor = Mathf.Clamp01(elapsedLoadingTime / totalLoadingTime);

            float loadingSpeed = Mathf.Lerp(1f, 5f, progressFactor);

            UI_Bar.value = Mathf.MoveTowards(UI_Bar.value, target, loadingSpeed * Time.deltaTime);

        } while(scene.progress < 0.9f);

        scene.allowSceneActivation = true;
    }

    void Update() => UI_Bar.value = Mathf.MoveTowards(UI_Bar.value, target, 3 * Time.deltaTime);
}
