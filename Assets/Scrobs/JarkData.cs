using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Fish", menuName = "ScriptableObjects/FishVariants/JarkTheShark", order = 1)]
public class JarkData : ScriptableObject
{
    public int randomNumber;    // Likely to be intended for the weight (score) of the fish
    public Sprite sprite;
    public Animator animation;
    public string fishName;
    public Habitat habitat;
    
    [TextArea(5, 12)] public string Dialogue;
    [TextArea(5, 12)] public string Option1;
    [TextArea(5, 12)] public string Option2;
    [TextArea(5, 12)] public string Option3;
    [TextArea(5, 12)] public string Option4;
    
    
    
    public enum Habitat
    {
        Tropical,
        Deepsea,
        Coldwater,
        Ocean,
    }
    
}