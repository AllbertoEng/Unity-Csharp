using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkInteract : Interactable
{
    [SerializeField] DialogContainer dialogue;
    public override void Interact(Character character)
    {
        Debug.Log("talk interact");
        GameManager.instance.dialogueSystem.Initialize(dialogue);
    }
}
