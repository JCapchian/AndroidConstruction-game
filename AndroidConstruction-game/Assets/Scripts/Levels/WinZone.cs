using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WinZone : Interactable
{
    [SerializeField]
    LevelLoader levelLoader;
    //[SerializeField]
    //Scene sceneActual;

    [SerializeField]
    bool needKey;
    [SerializeField]
    string canExitText;
    [SerializeField]
    string cantExitText;

    protected override void Awake()
    {
        base.Awake();

        levelLoader = FindObjectOfType<LevelLoader>();
        //sceneActual = SceneManager.GetActiveScene();
    }

    public override void Interact(GameObject playerReference)
    {
        Debug.Log("Pasar de nivel");

        ResetPlayer();

        gUIManager.InteractuablesTextPopUp(" ",false);

        if(SceneManager.GetActiveScene().buildIndex == 4)
            gUIManager.ShowWinScreen();

        base.Interact(playerReference);
    }

    void ResetPlayer()
    {
        playerManager.TurnDontDestroyOnLoad(true);

        inventoryManager.SaveInventory();
        inventoryManager.ResetKeys();

        gUIManager.ResetKeys();

        levelLoader.LoadNextLevel();
    }

    protected override void OnTriggerEnter2D(Collider2D other) {
        base.OnTriggerEnter2D(other);

        if(other.GetComponent<InventoryManager>())
        {
            if(needKey)
            {
                if(inventoryManager.hasAllKeys)
                    gUIManager.InteractuablesTextPopUp(canExitText,true);
                else
                    gUIManager.InteractuablesTextPopUp(cantExitText,true);
            }
            else
                gUIManager.InteractuablesTextPopUp(canExitText,true);
        }
    }

    protected override void OnTriggerExit2D(Collider2D other) {
        base.OnTriggerExit2D(other);

        if(other.GetComponent<InventoryManager>())
            gUIManager.InteractuablesTextPopUp(" ",false);
    }
}
