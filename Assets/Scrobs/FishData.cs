using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fish", menuName = "ScriptableObjects/FishVariant", order = 1)]
public class FishData : ScriptableObject
{
    public int randomNumber;    // Likely to be intended for the weight (score) of the fish
    public Sprite sprite;
    public Animator animation;
    public string fishName;
    public Habitat habitat;
    public List<Dialogues> dialogue;
    
    public enum Habitat
    {
        Tropical,
        Deepsea,
        Coldwater,
        Ocean,
    }

    [Serializable]
    public class Dialogues
    {
        public string name;
        public List<string> Dialogue;
    }
}