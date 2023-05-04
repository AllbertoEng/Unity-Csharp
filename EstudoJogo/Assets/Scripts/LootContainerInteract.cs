using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootContainerInteract : Interactable
{
    [SerializeField] GameObject closedChest;
    [SerializeField] GameObject openedChest;
    [SerializeField] bool opened;
    [SerializeField] AudioClip onOpenAudio;
    public override void Interact(Character character)
    {
        if (!opened)
        {
            opened = true;
            closedChest.SetActive(false);
            openedChest.SetActive(true);

            AudioManager.instance.Play(onOpenAudio);
        }
    }
}
