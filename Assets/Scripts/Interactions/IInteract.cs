
using UnityEngine;

public interface IInteract
{
    public bool CanUse { get; }
    public bool InUse { get; }
    public bool BeginInteract();
    public bool CancelInteract();
    public void InteractorEnter();
    public void InteractorExit();
    public Transform Transform { get; }
}
