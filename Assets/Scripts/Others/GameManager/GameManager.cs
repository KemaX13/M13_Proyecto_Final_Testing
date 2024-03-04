using UnityEngine;
using RyuSeiEngine.GameManager;
using System.Collections.Generic;
using System.Linq;

namespace RyuSeiEngine {
    namespace GameManager {
        public enum GameState {
            MainMenu,
            Gameplay,
            Pause,
            SubMenu,
            Cinematic
        };
    }
}

internal class GameManager : MonoBehaviour {
    internal static GameManager instance { get; private set; }
    [SerializeField] private ObjectGameStateList SO;

    internal GameState previousState { private get; set; } = GameState.MainMenu;

    void Awake() {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    public void EnableScripts(GameState state) {
        var controllerList = SO.ControllerList;

        foreach(var controller in controllerList) {
            bool isEnabled = controller.Key.Any(key => key == state);

            SetIsEnabled(controller, isEnabled);
        }
    }

    void SetIsEnabled(KeyValuePair<GameState[], MonoBehaviour[]> controller, bool isEnabled) {
        for(int i = 0; i < controller.Value.Length; i++)
            controller.Value[i].enabled = isEnabled;
    }

    public void SetBackPreviousState() => EnableScripts(previousState);
}
