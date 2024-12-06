using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



    
public class TypewriterTest : MonoBehaviour
{
    
    // All Button GameObjects
    private GameObject _button1Object;
    private GameObject _button2Object;
    private GameObject _button3Object;
    private GameObject _button4Object;

    
    // All Buttons (for listeners)
    private Button _button1;
    private Button _button2;
    private Button _button3;
    private Button _button4;

    // All Button texts
    private TMP_Text _button1ObjectText;
    private TMP_Text _button2ObjectText;
    private TMP_Text _button3ObjectText;
    private TMP_Text _button4ObjectText;

    public Fish currentFish;
    public enum Fish
    {
        Shark,
        Koi,
        Macarel,
        Angler,
        Octopus,
        Clown,
        Halibut,
        Clam
    }
    
    
    
    // Just for testing
    public TMP_Text textBox;
    
    // Prototyping
    [Header("Test String"), TextArea(5, 12)] 
    [SerializeField] private string testText;
    [TextArea(5, 12)]
    [SerializeField] private string testText2;

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
    


    public void RizzMode()
    {
        Debug.Log("RizzMode");
        // TODO: Make dialogue windows popup
        // TODO: Replace texts in main text and set revealed letters to zero
        
        // TODO: have a switch case that contains enums for every fish
        // TODO: add listeners to buttons based on which options of the 4 are correct based on the fish scrob "reeled in"

        
        // Removing all previous listeners to prepare for new dialogue
        _button1.onClick.RemoveAllListeners();
        _button2.onClick.RemoveAllListeners();
        _button3.onClick.RemoveAllListeners();
        _button4.onClick.RemoveAllListeners();
        
        switch (currentFish)
        {
            case Fish.Shark:
                // Add listeners to buttons according to the correct choices
                Debug.Log("Shark");
                break;
            case Fish.Koi:
                // Add listeners to buttons according to the correct choices
                Debug.Log("Koi");
                break;
            case Fish.Macarel:
                // Add listeners to buttons according to the correct choices
                Debug.Log("Macarel");
                break;
            case Fish.Angler:
                // Add listeners to buttons according to the correct choices
                Debug.Log("Angler");
                break;
            case Fish.Octopus:
                // Add listeners to buttons according to the correct choices
                Debug.Log("Octopus");
                break;
            case Fish.Clown:
                // Add listeners to buttons according to the correct choices
                Debug.Log("Clown");
                break;
            case Fish.Halibut:
                // Add listeners to buttons according to the correct choices
                Debug.Log("Halibut");
                break;
            case Fish.Clam:
                // Add listeners to buttons according to the correct choices
                Debug.Log("Clam");
                break;
        }
    }

    public void NoRizz()
    {
        // TODO Hide entire canvas? Don't think there's much mure
    }

    public void ShitWorks()
    {
        Debug.Log("IT FUCKING WORKS");
        DisableOptions();
        SetText(testText2);
    }


    private void Start()
    {
        // Calls SetText() function with test text as parameter
        SetText(testText);
        Debug.Log("Start func " + testText);
        
    }
    private void Awake()
    {
        // Gets all 4 button GameObjects
        _button1Object = GameObject.Find("Dialogue1");
        _button2Object = GameObject.Find("Dialogue2");
        _button3Object = GameObject.Find("Dialogue3");
        _button4Object = GameObject.Find("Dialogue4");
        
        
        // gets all 4 buttons
        _button1 = GameObject.Find("Dialogue1").GetComponent<Button>();
        _button2 = GameObject.Find("Dialogue2").GetComponent<Button>();
        _button3 = GameObject.Find("Dialogue3").GetComponent<Button>();
        _button4 = GameObject.Find("Dialogue4").GetComponent<Button>();
        
        // Gets all 4 button' texts
        _button1ObjectText = _button1Object.GetComponentInChildren<TMP_Text>();
        _button2ObjectText = _button2Object.GetComponentInChildren<TMP_Text>();
        _button3ObjectText = _button3Object.GetComponentInChildren<TMP_Text>();
        _button4ObjectText = _button4Object.GetComponentInChildren<TMP_Text>();
        
        _button1.onClick.AddListener(ShitWorks);
        
        // Delay for normal characters
        _simpleDelay = new WaitForSeconds(1 / charactersPerSecond);
        // Delay for interpunctuations
        _interpunctuationDelay = new WaitForSeconds(interpunctuationDelay);
        
        // Delay for the skipping
        _skipDelay = new WaitForSeconds(1 / (charactersPerSecond * skipSpeedup));
        
        textBoxFullEventDelay = new WaitForSeconds(sendDoneDelay);
        
        DisableOptions();
    }
    
    

    public void EnableOptions()
    {
        _button1Object.SetActive(true);
        _button2Object.SetActive(true);
        _button3Object.SetActive(true);
        _button4Object.SetActive(true);
        
        //TODO update with relevant text
    }

    public void DisableOptions()
    {
        _button1Object.SetActive(false);
        _button2Object.SetActive(false);
        _button3Object.SetActive(false);
        _button4Object.SetActive(false);
        
        //TODO: disable all 4 buttons
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
                
                // GOTTA LEARN but should invoke the event?
                Debug.Log("2nd try at text done");
                EnableOptions();
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

    
