using UnityEngine;
using RyuSeiEngine.GameManager;

internal class ObjectGameState : MonoBehaviour {
    [SerializeField] ObjectGameStateList SO;

    [SerializeField] MonoBehaviour[] controllers;
    [SerializeField] GameState[] states;

    void Start() => SO.ControllerList.Add(states, controllers);
}
