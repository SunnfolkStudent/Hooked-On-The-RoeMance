using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class TypewriterTest : MonoBehaviour
{
    #region README about order of scrobs
    /*  INFORMATION REGARDING THE ORDER OF THE FISH
     * 1. Posh Halibut
     * 2. Gold Digger Koi
     * 3. Emo Clownfish
     * 4. Jock Shark
     * 5. Markus Macarel
     * 6. Clam Samurai
     * 7. Influencer Angler
     * 8. Hopeless Romance Octopus
     * 9. Shy Jellyfish
     * 10. Melancholic Blobfish
     */
    #endregion
    
    // CHANGE HERE
    public JarkData[] allTheFish;
    public JarkData TestJarkDialogue;
    private int _testFishSelected = 0;
    private JarkData currentSCROB;
    
    public Sprite[] _fishSprites;
    private SpriteRenderer _rizzSprite;

    private int _erikNumber;
    
    
    // Used for the two stages
    private int _currentDialogue = 0;

    #region Lots of elements
    // All Buttons (for listeners)
    //private Button _button1;
    private Button _button1;
    private Button _button2;
    private Button _button3;
    private Button _button4;
    // All Button texts
    private TMP_Text _button1ObjectText;
    private TMP_Text _button2ObjectText;
    private TMP_Text _button3ObjectText;
    private TMP_Text _button4ObjectText;
    
    // All Button GameObjects
    private GameObject _button1Object;
    private GameObject _button2Object;
    private GameObject _button3Object;
    private GameObject _button4Object;
    
    // The 5 fields
    private string _scrobDialogue;
    private string _scrobOption1;
    private string _scrobOption2;
    private string _scrobOption3;
    private string _scrobOption4;
    
    // Slider
    private Slider _slider;

    private PlayerController _yeah;
    
    // The actual fish dialogue
    private TMP_Text _textBox;
    
    // The actual canvas
    private GameObject _dialogueCanvas;
    
    // Used to set what dialogue options are correct
    private bool _firstDialogue;
    private bool _secondDialogue;

    private WaitForSeconds _simpleDelay;
    private WaitForSeconds _interpunctuationDelay;

    private float charactersPerSecond = 48;
    private float interpunctuationDelay = 0.5f;
    #endregion

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
        Clam,
        Blobfish,
        Jellyfish,
        Trash
    }
    
    // Basic Typewriter Functionality
    private int _currentVisibleCharacterIndex;
    private Coroutine _typewriterCoroutine;

    private GameObject[] _spriteButtons;

    
    
    
    // Skipping Functionality
    public bool CurrentlySkipping { get; private set; }
    private WaitForSeconds _skipDelay;

    [Header("Skip options")] 
    [SerializeField] private bool quickSkip;
    // How much skipping speeds up dialogue
    [SerializeField] [Min(1)] private int skipSpeedup = 5;


    // Event Functionality
    private WaitForSeconds _textBoxFullEventDelay;
    [SerializeField] [Range(0.1f, 0.5f)] private float sendDoneDelay = 0.25f;
    
    
    private void Start()
    {
        // Prototyping
        currentFish = Fish.Shark;
        // Getting the entire canvas
        _dialogueCanvas = GameObject.Find("Canvas");
        _dialogueCanvas.SetActive(false);
    }
    
    
    public void DecideFish()
    {
        
        // This is where all logic is calculated before calling for RizzMode()
        switch (_erikNumber)
        {
            case 0:
                currentFish = Fish.Shark;
                break;
            case 1:
                currentFish = Fish.Macarel;
                break;
            case 2:
                currentFish = Fish.Jellyfish;
                break;
            case 3:
                currentFish = Fish.Halibut;
                break;
            case 4:
                currentFish = Fish.Angler;
                break;
            case 5:
                currentFish = Fish.Blobfish;
                break;
            case 6:
                currentFish = Fish.Macarel;
                break;
            case 7:
                currentFish = Fish.Halibut;
                break;
            case 8:
                currentFish = Fish.Halibut;
                break;
            case 9:
                currentFish = Fish.Koi;
                break;
            case 10:
                currentFish = Fish.Macarel;
                break;    
            case 11:
                currentFish = Fish.Clown;
                break;
            case 12:
                currentFish = Fish.Clam;
                break;
            case 13:
                currentFish = Fish.Macarel;
                break;
            case 14:
                currentFish = Fish.Halibut;
                break;
        }
        
        RemoveListeners();
        DecideDialogue();
        RizzMode();
    }

    public void RizzMode()
    {
        _erikNumber = PlayerController._kasperNumber;
        // Enabling the different stuff
         _dialogueCanvas.SetActive(true);
         _rizzSprite.sprite = _fishSprites[_erikNumber];
         SetText(allTheFish[_erikNumber].Dialogue[_currentDialogue]);
         
        // TODO: add listeners to buttons based on which options of the 4 are correct based on the fish scrob "reeled in"
        // TODO: get dialogue and options from PlayerController script
    }

    public void DecideDialogue()
    {
        #region Huge Switch Case
        switch (currentFish)
        {
            case Fish.Shark:
                if (_currentDialogue >= 0)
                {
                    _button1.onClick.AddListener(CorrectDialogue);
                    _button2.onClick.AddListener(WrongDialogue);
                    _button3.onClick.AddListener(CorrectDialogue);
                    _button4.onClick.AddListener(CorrectDialogue);
                    ChangeTextBoxes();
                    
                }
                if (_currentDialogue == 1)
                {
                    RemoveListeners();
                    _button1.onClick.AddListener(FinishDialogue);
                    _button2.onClick.AddListener(WrongDialogue);
                    _button3.onClick.AddListener(FinishDialogue);
                    _button4.onClick.AddListener(FinishDialogue);
                    ChangeTextBoxes();
                }
                break;
            case Fish.Koi:
                if (_currentDialogue >= 0)
                {
                    _button1.onClick.AddListener(WrongDialogue);
                    _button2.onClick.AddListener(WrongDialogue);
                    _button3.onClick.AddListener(CorrectDialogue);
                    _button4.onClick.AddListener(WrongDialogue);
                    ChangeTextBoxes();
                    
                }
                if (_currentDialogue == 1)
                {
                    RemoveListeners();
                    _button1.onClick.AddListener(FinishDialogue);
                    _button2.onClick.AddListener(WrongDialogue);
                    _button3.onClick.AddListener(WrongDialogue);
                    _button4.onClick.AddListener(WrongDialogue);
                    ChangeTextBoxes();
                }
                break;
            case Fish.Macarel: // TO BE COMPLETED
                if (_currentDialogue >= 0)
                {
                    _button1.onClick.AddListener(CorrectDialogue);
                    _button2.onClick.AddListener(WrongDialogue);
                    _button3.onClick.AddListener(CorrectDialogue);
                    _button4.onClick.AddListener(CorrectDialogue);
                    ChangeTextBoxes();
                    
                }
                if (_currentDialogue == 1)
                {
                    RemoveListeners();
                    _button1.onClick.AddListener(FinishDialogue);
                    _button2.onClick.AddListener(WrongDialogue);
                    _button3.onClick.AddListener(FinishDialogue);
                    _button4.onClick.AddListener(FinishDialogue);
                    ChangeTextBoxes();
                }
                break;
            case Fish.Angler: // TO BE COMPLETED
                if (_currentDialogue >= 0)
                {
                    _button1.onClick.AddListener(CorrectDialogue);
                    _button2.onClick.AddListener(WrongDialogue);
                    _button3.onClick.AddListener(CorrectDialogue);
                    _button4.onClick.AddListener(CorrectDialogue);
                    ChangeTextBoxes();
                    
                }
                if (_currentDialogue == 1)
                {
                    RemoveListeners();
                    _button1.onClick.AddListener(FinishDialogue);
                    _button2.onClick.AddListener(WrongDialogue);
                    _button3.onClick.AddListener(FinishDialogue);
                    _button4.onClick.AddListener(FinishDialogue);
                    ChangeTextBoxes();
                }
                break;
            case Fish.Octopus: // TO BE COMPLETED
                if (_currentDialogue >= 0)
                {
                    _button1.onClick.AddListener(CorrectDialogue);
                    _button2.onClick.AddListener(WrongDialogue);
                    _button3.onClick.AddListener(CorrectDialogue);
                    _button4.onClick.AddListener(CorrectDialogue);
                    ChangeTextBoxes();
                    
                }
                if (_currentDialogue == 1)
                {
                    RemoveListeners();
                    _button1.onClick.AddListener(FinishDialogue);
                    _button2.onClick.AddListener(WrongDialogue);
                    _button3.onClick.AddListener(FinishDialogue);
                    _button4.onClick.AddListener(FinishDialogue);
                    ChangeTextBoxes();
                }
                break;
            case Fish.Clown: 
                if (_currentDialogue >= 0)
                {
                    _button1.onClick.AddListener(WrongDialogue);
                    _button2.onClick.AddListener(WrongDialogue);
                    _button3.onClick.AddListener(CorrectDialogue);
                    _button4.onClick.AddListener(WrongDialogue);
                    ChangeTextBoxes();
                    
                }
                if (_currentDialogue == 1)
                {
                    RemoveListeners();
                    _button1.onClick.AddListener(WrongDialogue);
                    _button2.onClick.AddListener(WrongDialogue);
                    _button3.onClick.AddListener(WrongDialogue);
                    _button4.onClick.AddListener(FinishDialogue);
                    ChangeTextBoxes();
                }
                break;
            case Fish.Halibut:
                if (_currentDialogue >= 0)
                {
                    _button1.onClick.AddListener(WrongDialogue);
                    _button2.onClick.AddListener(CorrectDialogue);
                    _button3.onClick.AddListener(WrongDialogue);
                    _button4.onClick.AddListener(WrongDialogue);
                    ChangeTextBoxes();
                    
                }
                if (_currentDialogue == 1)
                {
                    RemoveListeners();
                    _button1.onClick.AddListener(FinishDialogue);
                    _button2.onClick.AddListener(WrongDialogue);
                    _button3.onClick.AddListener(WrongDialogue);
                    _button4.onClick.AddListener(WrongDialogue);
                    ChangeTextBoxes();
                }
                break;
            case Fish.Clam:
                if (_currentDialogue >= 0)
                {
                    _button1.onClick.AddListener(WrongDialogue);
                    _button2.onClick.AddListener(WrongDialogue);
                    _button3.onClick.AddListener(WrongDialogue);
                    _button4.onClick.AddListener(CorrectDialogue);
                    ChangeTextBoxes();
                    
                }
                if (_currentDialogue == 1)
                {
                    RemoveListeners();
                    _button1.onClick.AddListener(FinishDialogue);
                    _button2.onClick.AddListener(WrongDialogue);
                    _button3.onClick.AddListener(WrongDialogue);
                    _button4.onClick.AddListener(WrongDialogue);
                    ChangeTextBoxes();
                }
                break;
            case Fish.Blobfish:
                if (_currentDialogue >= 0)
                {
                    _button1.onClick.AddListener(CorrectDialogue);
                    _button2.onClick.AddListener(WrongDialogue);
                    _button3.onClick.AddListener(CorrectDialogue);
                    _button4.onClick.AddListener(WrongDialogue);
                    ChangeTextBoxes();
                    
                }
                if (_currentDialogue == 1)
                {
                    RemoveListeners();
                    _button1.onClick.AddListener(WrongDialogue);
                    _button2.onClick.AddListener(FinishDialogue);
                    _button3.onClick.AddListener(FinishDialogue);
                    _button4.onClick.AddListener(WrongDialogue);
                    ChangeTextBoxes();
                }
                break;
            case Fish.Jellyfish:
                if (_currentDialogue >= 0)
                {
                    _button1.onClick.AddListener(CorrectDialogue);
                    _button2.onClick.AddListener(WrongDialogue);
                    _button3.onClick.AddListener(WrongDialogue);
                    _button4.onClick.AddListener(CorrectDialogue);
                    ChangeTextBoxes();
                    
                }
                if (_currentDialogue == 1)
                {
                    RemoveListeners();
                    _button1.onClick.AddListener(FinishDialogue);
                    _button2.onClick.AddListener(WrongDialogue);
                    _button3.onClick.AddListener(FinishDialogue);
                    _button4.onClick.AddListener(WrongDialogue);
                    ChangeTextBoxes();
                }
                break;
        }
        #endregion
    }

    public void CorrectDialogue()
    {
        _currentDialogue++;
        print("Uhm?");
        NextDialogue();
    }

    public void WrongDialogue()
    {
        Debug.Log("You dumbass bitch");
    }

    public void ChangeTextBoxes()
    {
        _scrobDialogue = allTheFish[_erikNumber].Dialogue[_currentDialogue];
        _scrobOption1 = allTheFish[_erikNumber].Option1[_currentDialogue];
        _scrobOption2 = allTheFish[_erikNumber].Option2[_currentDialogue];
        _scrobOption3 = allTheFish[_erikNumber].Option3[_currentDialogue];
        _scrobOption4 = allTheFish[_erikNumber].Option4[_currentDialogue];

        _button1ObjectText.text = allTheFish[_erikNumber].Option1[_currentDialogue];
        _button2ObjectText.text = allTheFish[_erikNumber].Option2[_currentDialogue];
        _button3ObjectText.text = allTheFish[_erikNumber].Option3[_currentDialogue];
        _button4ObjectText.text = allTheFish[_erikNumber].Option4[_currentDialogue];
        
    }

    public void FinishDialogue()
    {
        Debug.Log("currentDialogue burde være 0, den er: " + _currentDialogue);
        RemoveListeners();
        NoRizz();
        ScoreManager.CurrentScore++;

        _slider.value += 1f;

    }

    public void NoRizz()
    {
        // TODO Hide entire canvas? Don't think there's much more
        _currentDialogue = 0;
        DisableOptions();
        _dialogueCanvas.SetActive(false);
        // TODO: Add disabling the fish here

        PlayerController._playerStatic = false;
        PlayerController._rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        PlayerController._isFishing = true;
        print("Does this run more than once once it has been called");
    }

    public void NextDialogue()
    {
        DisableOptions();

        SetText(allTheFish[_erikNumber].Dialogue[_currentDialogue]);
    }

    
    
    private void Awake()
    {
        #region Getting Objects and stuff
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
        
        _spriteButtons = GameObject.FindGameObjectsWithTag("DialogueButton");
        
        // Getting the main fish dialogue
        _textBox = GameObject.Find("FishText").GetComponent<TMP_Text>();
        _rizzSprite = GameObject.Find("FishSprite").GetComponent<SpriteRenderer>();
        _slider = GameObject.Find("Slider").GetComponent<Slider>();
        
        
        #endregion
        RemoveListeners();
        
        // Still prototyping
        
        
        // Delay for normal characters
        _simpleDelay = new WaitForSeconds(1 / charactersPerSecond);
        // Delay for interpunctuations
        _interpunctuationDelay = new WaitForSeconds(interpunctuationDelay);
        
        // Delay for the skipping
        _skipDelay = new WaitForSeconds(1 / (charactersPerSecond * skipSpeedup));
        
        _textBoxFullEventDelay = new WaitForSeconds(sendDoneDelay);
        
        DisableOptions();
    }

    public void RemoveListeners()
    {
        // Removing all previous listeners to prepare for new dialogue
        _button1.onClick.RemoveAllListeners();
        _button2.onClick.RemoveAllListeners();
        _button3.onClick.RemoveAllListeners();
        _button4.onClick.RemoveAllListeners();
    }
    public void EnableOptions()
    {
        _button1Object.SetActive(true);
        _button2Object.SetActive(true);
        _button3Object.SetActive(true);
        _button4Object.SetActive(true);
        
        foreach (GameObject gameObject in _spriteButtons)
        {
            gameObject.SetActive(true);
        }
        
        //TODO update with relevant text
    }
    public void DisableOptions()
    {
        // _button1Object.SetActive(false);
        // _button2Object.SetActive(false);
        // _button3Object.SetActive(false);
        // _button4Object.SetActive(false);
        foreach (GameObject gameObject in _spriteButtons)
        {
            gameObject.SetActive(false);
        }
    }

    private void SetText(string text)
    {
        if (_typewriterCoroutine != null)
            StopCoroutine(_typewriterCoroutine);
        // Sets variable to imported text from Start
        _textBox.text = text;
        
        // Resets the current visible characters and index to 0 to be ready for new text
        _textBox.maxVisibleCharacters = 0;
        _currentVisibleCharacterIndex = 0;
        
        // Starts THE HOLY GRAIL coroutine for making the effect
        _typewriterCoroutine = StartCoroutine(TypeWriter());
    }
    private IEnumerator TypeWriter()
    {
        // Uses TMP textinfo parameters to check characters and length
        TMP_TextInfo textInfo = _textBox.textInfo;

        // Runs only when there are less characters displayed than the total the box has
        while (_currentVisibleCharacterIndex < textInfo.characterCount + 1)
        {
            var lastCharacterIndex = textInfo.characterCount - 1;

            if (_currentVisibleCharacterIndex == lastCharacterIndex)
            {
                _textBox.maxVisibleCharacters++;
                yield return _textBoxFullEventDelay;
                
                // THIS IS WHERE THE TEXT IS DONE!!!
                // THIS IS WHERE THE TEXT IS DONE!!!
                // THIS IS WHERE THE TEXT IS DONE!!!
                EnableOptions();
                DecideDialogue();
                yield break;
            }
            
            
            // Sets current iteration's character as parsed character
            char character = textInfo.characterInfo[_currentVisibleCharacterIndex].character;
            
            // Adds 1 to the visible characters
            
            _textBox.maxVisibleCharacters ++;

            
            // Checks for "special" signs or if it's currentlyskipping
            if (character is '?' or '.' or ',' or ':' or ';' or '!' or '-')
            {
                // Returns with a bigger delay for these signs
                yield return _interpunctuationDelay;
            }
            else
            {
                // Else normal letter delay
                yield return _simpleDelay;
            }
            
            // Adds 1 to the characters being parsed
            _currentVisibleCharacterIndex ++;
        }
    }
}

    
