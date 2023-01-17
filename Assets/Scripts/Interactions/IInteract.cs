
using UnityEngine;
using UnityEngine.Events;

public interface IInteract
{
    public bool CanUse { get; }
    public bool InUse { get; }
    public bool BeginInteract();
    public bool CancelInteract();
    public void InteractorEnter();
    public void InteractorExit();
    public Transform SelectorTransform { get; }
    public float SelectorSize { get; }
    public Transform Transform { get; }

    public static event UnityAction<IInteract> InteractionComplete;
    public static void OnInteractionComplete(IInteract interaction) => InteractionComplete?.Invoke(interaction);
}
