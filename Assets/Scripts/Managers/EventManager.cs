using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static event UnityAction PlayerDeath;
    public static void OnPlayerDeath() => PlayerDeath?.Invoke();
    //subscribe in OnEnable in class with EventManager.PlayerDeath += Method;

    public static event UnityAction<Vector3> CoinCollected;
    public static void OnCoinCollected(Vector3 position) => CoinCollected?.Invoke(position);

    
}
