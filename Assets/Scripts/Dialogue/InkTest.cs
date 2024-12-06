using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class InkTest : MonoBehaviour
{
    
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

    private Button _button1, _button2, _button3, _button4;
    private TMP_Text _button1Text, _button2Text, _button3Text, _button4Text;


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
        // Gets all 4 buttons
        _button1 = GameObject.Find("Button1").GetComponent<Button>();
        _button2 = GameObject.Find("Button2").GetComponent<Button>();
        _button3 = GameObject.Find("Button3").GetComponent<Button>();
        _button4 = GameObject.Find("Button4").GetComponent<Button>();
        
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
    }



    private void Start()
    {
        // Gets all 4 button' texts
        _button1Text = _button1.GetComponentInChildren<TMP_Text>();
        _button2Text = _button2.GetComponentInChildren<TMP_Text>();
        _button3Text = _button3.GetComponentInChildren<TMP_Text>();
        _button4Text = _button4.GetComponentInChildren<TMP_Text>();
        
        // Calls SetText() function with test text as parameter
        SetText(testText);
        Debug.Log("Start func " + testText);
        
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

    
