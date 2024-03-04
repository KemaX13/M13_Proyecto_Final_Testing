using UnityEngine;
using RyuSeiEngine.GameManager;

internal class ChangeGameState : MonoBehaviour {
    [SerializeField] GameState state;

    public void ChangeState() => GameManager.instance.EnableScripts(state);

    public void SetPreviousState() => GameManager.instance.previousState = state;
}
