using UnityEngine;

public class PlayerMovingState : PlayerBaseState
{
    public Vector2 notMoving = new Vector2 (0f, 0f);
    public Vector2 playerMovement;
    public Animator playerAnimator;

    public override void EnterState(PlayerStateManagment player){
        playerAnimator = player.GetComponent<PlayerScript>().MyAnimator;
    }

    public override void UpdateState(PlayerStateManagment player){
        playerMovement = player.GetComponent<PlayerScript>().movementInput;
        
        if(playerMovement == notMoving)
            player.SwitchState(player.IdleState);
        else{
            Debug.Log("Moviendose");
            playerAnimator.SetBool("isMoving", true);
        }
    }
    public override void OnCollisionEnter(PlayerStateManagment player){

    }

}
