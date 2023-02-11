using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WinZone : Interactable
{
    LevelLoader levelLoader;
    Scene sceneActual;

    public string hasKeyText;
    public string hasNoKeyText;

    protected override void Awake()
    {
        base.Awake();

        levelLoader = FindObjectOfType<LevelLoader>();
        sceneActual = SceneManager.GetActiveScene();
    }

    public override void Interact(GameObject playerReference)
    {
        if(inventoryManager.hasAllKeys)
            levelLoader.LoadNextLevel();

        base.Interact(playerReference);
    }

    protected override void OnTriggerEnter2D(Collider2D other) {
        base.OnTriggerEnter2D(other);

        if(other.GetComponent<InventoryManager>())
        {
            if(inventoryManager.hasAllKeys)
            gUIManager.InteractuablesTextPopUp(hasKeyText,true);
        else
            gUIManager.InteractuablesTextPopUp(hasNoKeyText,true);
        }
    }

    protected override void OnTriggerExit2D(Collider2D other) {
        base.OnTriggerExit2D(other);

        if(other.GetComponent<InventoryManager>())
            gUIManager.InteractuablesTextPopUp(" ",false);
    }
}
