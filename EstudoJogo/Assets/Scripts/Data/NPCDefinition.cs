using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Race
{
    Alien,
    Human,
    Orc,
    [InspectorName("Any [For romance only]")]
    Any
}

[Serializable]
public class PortraitsCollection
{
    public Texture2D normal;
    public Texture2D surprised;
    public Texture2D confused;
    public Texture2D angry;
}

[CreateAssetMenu(menuName ="Data/NPC Character")]
public class NPCDefinition : ScriptableObject
{
    public string Name = "Nameless";
    public Race race = Race.Alien;

    public PortraitsCollection portraits;

    public GameObject characterPrefab;

    [Header("Interaction")]
    public bool canBeRomanced;
    public Race romanceableRace;

    public List<Item> itemLikes;
    public List<Item> itemDislikes;

    [Header("Schedule")]
    public string schedule;
}
