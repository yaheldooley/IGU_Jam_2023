
using UnityEngine;

public interface IMover
{
    public void EnableMovement();
    public void DisableMovement();
    public void SetMoverPosition(Vector3 pos);
}
