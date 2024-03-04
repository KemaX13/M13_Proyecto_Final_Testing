using System.Collections.Generic;
using UnityEngine;
using RyuSeiEngine.GameManager;

[CreateAssetMenu(menuName = "GameManager/ObjectList", fileName = "ObjectGameStateList")]
internal class ObjectGameStateList : ScriptableObject {
    internal Dictionary<GameState[], MonoBehaviour[]> ControllerList = new Dictionary<GameState[], MonoBehaviour[]>();

    private void OnDisable() => ControllerList.Clear();
}

