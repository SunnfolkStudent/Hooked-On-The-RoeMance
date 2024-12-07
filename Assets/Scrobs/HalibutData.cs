using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Fish", menuName = "ScriptableObjects/FishVariants/PoshHalibut", order = 1)]
public class HailbutData : ScriptableObject
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
        [SerializeField]
        [TextArea(5, 12)] public List<TMP_Text> Dialogue;
        [TextArea(5, 12)] public List<TMP_Text> Option1;
        [TextArea(5, 12)] public List<TMP_Text> Option2;
        [TextArea(5, 12)] public List<TMP_Text> Option3;
        [TextArea(5, 12)] public List<TMP_Text> Option4;
        
    }
    
}