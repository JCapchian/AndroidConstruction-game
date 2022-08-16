using UnityEngine;

public abstract class PlayerBaseState
{
    public abstract void EnterState(PlayerStateManagment player);

    public abstract void UpdateState(PlayerStateManagment player);

    public abstract void OnCollisionEnter(PlayerStateManagment player);

    //public abstract void PlayerDeathState(PlayerStateManagment player);
}
