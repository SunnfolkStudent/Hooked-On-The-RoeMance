using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueUI;
    public TMP_Text dialogueText;
    
    private Story currentStory;
    
    private bool dialogueIsPlaying;
    
    private static DialogueManager _instance;

    private void Awake()
    {
        if (_instance != null)
            Debug.LogWarning("More than one Dialogue Manager in scene!");
        _instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return _instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialogueUI.SetActive(false);
    }

    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }
        
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialogueUI.SetActive(true);
        
        ContinueStory();
        
    }
    private void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialogueUI.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
        }
        else
        {
            ExitDialogueMode();
        }
    }
}
