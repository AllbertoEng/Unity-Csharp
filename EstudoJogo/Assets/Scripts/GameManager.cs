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

    [SerializeField] public GameObject player;
    [SerializeField] public ItemContainer inventoryContainer;
    [SerializeField] public ItemDragAndDropController dragAndDropController;
    [SerializeField] public DayTimeController timeController;
    [SerializeField] public DialogueSystem dialogueSystem;
    [SerializeField] public PlaceableObjectsReferenceManager placeableObjects;
    [SerializeField] public ItemList ItemDB;
    [SerializeField] public OnScreenMessageSystem messageSystem;
}
