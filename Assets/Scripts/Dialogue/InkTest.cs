using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class InkTest : MonoBehaviour
{
    
    // From Don't Get Stabbed, gonna use for dialogue arrays probably
    [TextArea(5,7)]
    public string[] dialogue;
    [TextArea(5,7)]
    public string[] butt1;
    [TextArea(5,7)]
    public string[] butt2;
    [TextArea(5,7)]
    public string[] butt3;
    
    // Just for testing
    public TMP_Text textBox;
    
    // Prototyping
    [Header("Test String")] 
    [SerializeField] private string testText;

    // Basic Typewriter Functionality
    private int _currentVisibleCharacterIndex;
    private Coroutine _typewriterCoroutine;
    private bool _readyForNewText = true;

    private WaitForSeconds _simpleDelay;
    private WaitForSeconds _interpunctuationDelay;

    [Header("Typewriter Settings")] 
    [SerializeField] private float charactersPerSecond = 48;
    [SerializeField] private float interpunctuationDelay = 0.5f;
    

    // Skipping Functionality
    public bool CurrentlySkipping { get; private set; }
    private WaitForSeconds _skipDelay;

    [Header("Skip options")] 
    [SerializeField] private bool quickSkip;
    // How much skipping speeds up dialogue
    [SerializeField] [Min(1)] private int skipSpeedup = 5;


    // Event Functionality
    private WaitForSeconds textBoxFullEventDelay;
    [SerializeField] [Range(0.1f, 0.5f)] private float sendDoneDelay = 0.25f;

    // Event for when the text is done
    


    private void Awake()
    {
        
        // Delay for normal characters
        _simpleDelay = new WaitForSeconds(1 / charactersPerSecond);
        // Delay for interpunctuations
        _interpunctuationDelay = new WaitForSeconds(interpunctuationDelay);
        
        // Delay for the skipping
        _skipDelay = new WaitForSeconds(1 / (charactersPerSecond * skipSpeedup));
        
        textBoxFullEventDelay = new WaitForSeconds(sendDoneDelay);
        
        
        
        
    }

    public void RizzMode()
    {
        Debug.Log("RizzMode");
        //TODO enable entire canvas
        //TODO replace dialogue with relevant one
        //TODO set dialogue progress to 0 (REMEMBER BOTH VARIABLES)
    }

    



    private void Start()
    {
        
        
        
        // Calls SetText() function with test text as parameter
        SetText(testText);
        print("Start func " + testText);
        
    }


    private void SetText(string text)
    {
        // Sets variable to imported text from Start
        textBox.text = text;
        
        // Resets the current visible characters and index to 0 to be ready for new text
        textBox.maxVisibleCharacters = 0;
        _currentVisibleCharacterIndex = 0;
        
        // Starts THE HOLY GRAIL coroutine for making the effect
        _typewriterCoroutine = StartCoroutine(TypeWriter());
    }
    
    

    private IEnumerator TypeWriter()
    {
        // Uses TMP textinfo parameters to check characters and length
        TMP_TextInfo textInfo = textBox.textInfo;

        // Runs only when there are less characters displayed than the total the box has
        while (_currentVisibleCharacterIndex < textInfo.characterCount + 1)
        {
            var lastCharacterIndex = textInfo.characterCount - 1;

            if (_currentVisibleCharacterIndex == lastCharacterIndex)
            {
                textBox.maxVisibleCharacters++;
                yield return textBoxFullEventDelay;
                
                // THIS IS WHERE THE TEXT IS DONE
                // THIS IS WHERE THE TEXT IS DONE
                // THIS IS WHERE THE TEXT IS DONE
                Debug.Log("2nd try at text done");
                yield break;
            }
            
            
            // Sets current iteration's character as parsed character
            char character = textInfo.characterInfo[_currentVisibleCharacterIndex].character;
            
            // Adds 1 to the visible characters
            textBox.maxVisibleCharacters++;

            
            // Checks for "special" signs or if it's currentlyskipping
            if (character is '?' or '.' or ',' or ':' or ';' or '!' or '-')
            {
                // Returns with a bigger delay for these signs
                yield return _interpunctuationDelay;
            }
            else
            {
                // Else normal letter delay
                yield return CurrentlySkipping ? _skipDelay : _simpleDelay;
            }
            
            // Adds 1 to the characters being parsed
            _currentVisibleCharacterIndex++;
        }
    }
}

    
