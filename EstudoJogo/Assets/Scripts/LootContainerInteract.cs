using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootContainerInteract : Interactable
{
    [SerializeField] GameObject closedChest;
    [SerializeField] GameObject openedChest;
    [SerializeField] bool opened;
    [SerializeField] AudioClip onOpenAudio;
    [SerializeField] ItemContainer itemContainer;
    public override void Interact(Character character)
    {
        if (!opened)
        {
            Open(character);
        }
        else
        {
            Close(character);
        }               
    }
    public void Open(Character character)
    {
        opened = true;
        closedChest.SetActive(false);
        openedChest.SetActive(true);

        AudioManager.instance.Play(onOpenAudio);

        character.GetComponent<ItemContainerInteractController>().Open(itemContainer, transform);
    }

    public void Close(Character character)
    {
        opened = false;
        closedChest.SetActive(true);
        openedChest.SetActive(false);

        character.GetComponent<ItemContainerInteractController>().Close();
    }
}
