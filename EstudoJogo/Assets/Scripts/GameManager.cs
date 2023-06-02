using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
        // SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Additive);
    }

    public GameObject player;
    public ItemContainer inventoryContainer;
    public ItemDragAndDropController dragAndDropController;
    public DayTimeController timeController;
    public DialogueSystem dialogueSystem;
    public PlaceableObjectsReferenceManager placeableObjects;
    public ItemList ItemDB;
    public OnScreenMessageSystem messageSystem;
    public ScreenTint screenTint;
}
