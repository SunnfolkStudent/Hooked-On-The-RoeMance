using System.Collections;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    
    private void Awake()
    {
        StartCoroutine(Combat());
    }

    public void TriggerDialogue()
    {
        Debug.Log("Triggering Dialogue here");
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
    }




    IEnumerator Combat()
    {
        yield return new WaitForSeconds(3f);
        // Start dialogue function()
    }
}
