using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[Serializable]
public class ItemConvertorData
{
    public ItemSlot itemSlot;
    public int timer;
    public bool working = false;

    public ItemConvertorData()
    {
        itemSlot = new ItemSlot();
    }
}

[RequireComponent(typeof(TimeAgent))]
public class ItemConverterInteract : Interactable, IPersistent
{
    [SerializeField] Item convertableItem;
    [SerializeField] Item producedItem;
    [SerializeField] int producedItemCount = 1;
    [SerializeField] Light2D luzDoForninho;

    [SerializeField] int timeToProcess = 5;

    ItemConvertorData data;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        TimeAgent timeAgent = GetComponent<TimeAgent>();
        timeAgent.onTimeTick += ItemConverterProcess;

        if (data == null)
            data = new ItemConvertorData();
        else
        {
            WorkingVisual(data.working);
        }
    }

    private void ItemConverterProcess(DayTimeController dayTimeController)
    {
        if (data.itemSlot == null)
            return;

        if (data.timer > 0f)
        {
            data.timer -= 1;
            if (data.timer <= 0f)
            {
                CompleteItemConversion();
            }
        }
    }

    public override void Interact(Character character)
    {
        if (data.itemSlot.item == null)
        {
            if (GameManager.instance.dragAndDropController.Check(convertableItem))
            {
                StartItemProcessing(GameManager.instance.dragAndDropController.itemSlot);
                return;
            }

            ToolBarController toolBarController = character.GetComponent<ToolBarController>();
            if (toolBarController == null)
                return;

            ItemSlot itemSlot = toolBarController.GetItemSlot;

            if (itemSlot.item == convertableItem)
            {
                StartItemProcessing(itemSlot);
                return;
            }


        }

        if (data.itemSlot.item != null && data.timer <= 0)
        {
            GameManager.instance.inventoryContainer.Add(data.itemSlot.item, data.itemSlot.count);
            data.itemSlot.Clear();
        }

    }

    private void StartItemProcessing(ItemSlot toProcess)
    {
        WorkingVisual(true);

        data.itemSlot.Copy(GameManager.instance.dragAndDropController.itemSlot);
        data.itemSlot.count = 1;

        if (toProcess.item.stackable)
        {
            toProcess.count -= 1;
            if (toProcess.count < 0)
            {
                toProcess.Clear();
            }
        }
        else
        {
            toProcess.Clear();
        }

        data.timer = timeToProcess;
    }

    private void CompleteItemConversion()
    {
        WorkingVisual(false);

        data.itemSlot.Clear();
        data.itemSlot.Set(producedItem, producedItemCount);
    }

    private void WorkingVisual(bool state)
    {
        animator.SetBool("Burning", state);
        luzDoForninho.transform.gameObject.SetActive(state);
        data.working = state;
    }

    public string Read()
    {
        return JsonUtility.ToJson(data);
    }

    public void Load(string jsonString)
    {
        data = JsonUtility.FromJson<ItemConvertorData>(jsonString);
    }
}
