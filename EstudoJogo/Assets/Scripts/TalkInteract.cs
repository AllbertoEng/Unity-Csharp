using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkInteract : Interactable
{
    NPCDefinition npcDefinition;
    NPCCharacter npcCharacter;
    private void Awake()
    {
        npcCharacter = GetComponent<NPCCharacter>();
        npcDefinition = npcCharacter.character;
    }
    public override void Interact(Character character)
    {
        DialogContainer dialogContainer = npcDefinition.generalDialogues[Random.Range(0, npcDefinition.generalDialogues.Count)];
        npcCharacter.IncreaseRelationship(0.1f);
        GameManager.instance.dialogueSystem.Initialize(dialogContainer);
    }
}
