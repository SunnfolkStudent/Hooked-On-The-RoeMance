using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class TypewriterTest : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
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

    [SerializeField]
    private GameObject FishSlapObject;

    private Schlawg _FMOD;
    
    // CHANGE HERE
    public JarkData[] allTheFish;
    public JarkData TestJarkDialogue;
    private int _testFishSelected = 0;
    private JarkData currentSCROB;
    
    public Sprite[] _fishSprites;
    private SpriteRenderer _rizzSprite;

    private int _erikNumber;

    public List<int> caughtFishes = new List<int>();
    
    
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

    public Sprite[] weightSprites;
    private SpriteRenderer uiSprite;
    
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
    private float sendDoneDelay = 0.25f;
    
    public Fish currentFish;
    #endregion
    
    private void Start()
    {
        Debug.Log("Fuck Y'all");
        // Prototyping
        currentFish = Fish.Shark;
        // Getting the entire canvas
        _dialogueCanvas = GameObject.Find("Canvas");
        _dialogueCanvas.SetActive(false);
<<<<<<< HEAD
        _FMOD = GameObject.Find("AudioManager").GetComponent<FMOD_Controller>();
        
=======
        _FMOD = GameObject.Find("AudioManager").GetComponent<Schlawg>();
>>>>>>> main
    }
    
    public void DecideFish(int erik)
    {
        _erikNumber = erik;
        // This is where all logic is calculated before calling for RizzMode()
        switch (erik)
        {
            case 0:
                currentFish = Fish.Shark;
<<<<<<< HEAD
                _FMOD.FishingMode(2);
                _FMOD.CharacterTheme(4);
                break;
            case 1:
                currentFish = Fish.Macarel;
                _FMOD.FishingMode(2);
                _FMOD.CharacterTheme(8);
                break;
            case 2:
                currentFish = Fish.Jellyfish;
                _FMOD.FishingMode(2);
                _FMOD.CharacterTheme(5);
                break;
            case 3:
                currentFish = Fish.Halibut;
                _FMOD.FishingMode(2);
                _FMOD.CharacterTheme(7);
                break;
            case 4:
                currentFish = Fish.Angler;
                _FMOD.FishingMode(2);
                _FMOD.CharacterTheme(0);
                break;
            case 5:
                currentFish = Fish.Blobfish;
                _FMOD.FishingMode(2);
                _FMOD.CharacterTheme(1);
                break;
            case 6:
                currentFish = Fish.Macarel;
                _FMOD.FishingMode(2);
                _FMOD.CharacterTheme(8);
                break;
            case 7:
                currentFish = Fish.Halibut;
                _FMOD.FishingMode(2);
                _FMOD.CharacterTheme(7);
                break;
            case 8:
                currentFish = Fish.Halibut;
                _FMOD.FishingMode(2);
                _FMOD.CharacterTheme(7);
                break;
            case 9:
                currentFish = Fish.Koi;
                _FMOD.FishingMode(2);
                _FMOD.CharacterTheme(6);
                break;
            case 10:
                currentFish = Fish.Macarel;
                _FMOD.FishingMode(2);
                _FMOD.CharacterTheme(8);
                break;    
            case 11:
                currentFish = Fish.Clown;
                _FMOD.FishingMode(2);
                _FMOD.CharacterTheme(3);
                break;
            case 12:
                currentFish = Fish.Octopus;
                _FMOD.FishingMode(2);
                _FMOD.CharacterTheme(9);
                break;
            case 13:
                currentFish = Fish.Clam;
                _FMOD.FishingMode(2);
                _FMOD.CharacterTheme(2);
                break;
            case 14:
                currentFish = Fish.Macarel;
                _FMOD.FishingMode(2);
                _FMOD.CharacterTheme(8);
                break;
            case 15:
                currentFish = Fish.Halibut;
                _FMOD.FishingMode(2);
                _FMOD.CharacterTheme(7);
                break;
            case 16:
                currentFish = Fish.Trash;
                _FMOD.FishingMode(2);
                _FMOD.CharacterTheme(10);
                break;
        }
        
        // Debug.Log("enum is " + currentFish);
        // Debug.Log("currentdialogue is "+ _currentDialogue);
        // Debug.Log("ErikNumber is" + erik);
        
=======
                // _FMOD.FishingMode(2);
                // _FMOD.CharacterTheme(4);
                break;
            case 1:
                currentFish = Fish.Macarel;
                // _FMOD.FishingMode(2);
                // _FMOD.CharacterTheme(8);
                break;
            case 2:
                currentFish = Fish.Jellyfish;
                // _FMOD.FishingMode(2);
                // _FMOD.CharacterTheme(5);
                break;
            case 3:
                currentFish = Fish.Halibut;
                // _FMOD.FishingMode(2);
                // _FMOD.CharacterTheme(7);
                break;
            case 4:
                currentFish = Fish.Angler;
                // _FMOD.FishingMode(2);
                // _FMOD.CharacterTheme(0);
                break;
            case 5:
                currentFish = Fish.Blobfish;
                // _FMOD.FishingMode(2);
                // _FMOD.CharacterTheme(1);
                break;
            case 6:
                currentFish = Fish.Macarel;
                // _FMOD.FishingMode(2);
                // _FMOD.CharacterTheme(8);
                break;
            case 7:
                currentFish = Fish.Halibut;
                // _FMOD.FishingMode(2);
                // _FMOD.CharacterTheme(7);
                break;
            case 8:
                currentFish = Fish.Halibut;
                // _FMOD.FishingMode(2);
                // _FMOD.CharacterTheme(7);
                break;
            case 9:
                currentFish = Fish.Koi;
                // _FMOD.FishingMode(2);
                // _FMOD.CharacterTheme(6);
                break;
            case 10:
                currentFish = Fish.Macarel;
                // _FMOD.FishingMode(2);
                // _FMOD.CharacterTheme(8);
                break;    
            case 11:
                currentFish = Fish.Clown;
                // _FMOD.FishingMode(2);
                // _FMOD.CharacterTheme(3);
                break;
            case 12:
                currentFish = Fish.Octopus;
                // _FMOD.FishingMode(2);
                // _FMOD.CharacterTheme(9);
                break;
            case 13:
                currentFish = Fish.Clam;
                // _FMOD.FishingMode(2);
                // _FMOD.CharacterTheme(2);
                break;
            case 14:
                currentFish = Fish.Macarel;
                // _FMOD.FishingMode(2);
                // _FMOD.CharacterTheme(8);
                break;
            case 15:
                currentFish = Fish.Halibut;
                // _FMOD.FishingMode(2);
                // _FMOD.CharacterTheme(7);
                break;
            case 16:
                currentFish = Fish.Trash;
                // _FMOD.FishingMode(2);
                // _FMOD.CharacterTheme(10);
                break;
        }
        // Debug.Log("enum is " + currentFish);
        // Debug.Log("currentdialogue is "+ _currentDialogue);
        // Debug.Log("ErikNumber is" + erik);
>>>>>>> main
        RemoveListeners();
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
            case Fish.Macarel:
                if (_currentDialogue >= 0)
                {
                    _button1.onClick.AddListener(CorrectDialogue);
                    _button2.onClick.AddListener(CorrectDialogue);
                    _button3.onClick.AddListener(CorrectDialogue);
                    _button4.onClick.AddListener(CorrectDialogue);
                    ChangeTextBoxes();
                    
                }
                if (_currentDialogue == 1)
                {
                    RemoveListeners();
                    _button1.onClick.AddListener(WrongDialogue);
                    _button2.onClick.AddListener(FinishDialogue);
                    _button3.onClick.AddListener(WrongDialogue);
                    _button4.onClick.AddListener(FinishDialogue);
                    ChangeTextBoxes();
                }
                break;
            case Fish.Angler: 
                if (_currentDialogue >= 0)
                {
                    _button1.onClick.AddListener(WrongDialogue);
                    _button2.onClick.AddListener(CorrectDialogue);
                    _button3.onClick.AddListener(CorrectDialogue);
                    _button4.onClick.AddListener(WrongDialogue);
                    ChangeTextBoxes();
                    
                }
                if (_currentDialogue == 1)
                {
                    RemoveListeners();
                    _button1.onClick.AddListener(WrongDialogue);
                    _button2.onClick.AddListener(FinishDialogue);
                    _button3.onClick.AddListener(WrongDialogue);
                    _button4.onClick.AddListener(FinishDialogue);
                    ChangeTextBoxes();
                }
                break;
            case Fish.Octopus: 
                if (_currentDialogue >= 0)
                {
                    _button1.onClick.AddListener(WrongDialogue);
                    _button2.onClick.AddListener(CorrectDialogue);
                    _button3.onClick.AddListener(CorrectDialogue);
                    _button4.onClick.AddListener(CorrectDialogue);
                    ChangeTextBoxes();
                    
                }
                if (_currentDialogue == 1)
                {
                    RemoveListeners();
                    _button1.onClick.AddListener(FinishDialogue);
                    _button2.onClick.AddListener(FinishDialogue);
                    _button3.onClick.AddListener(WrongDialogue);
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
            case Fish.Trash:
                if (_currentDialogue >= 0)
                {
                    _button1.onClick.AddListener(FinishDialogue);
                    _button2.onClick.AddListener(FinishDialogue);
                    _button3.onClick.AddListener(FinishDialogue);
                    _button4.onClick.AddListener(FinishDialogue);
                    ChangeTextBoxes();
                }
                break;
        }
        #endregion
    }

    public void CorrectDialogue()
    {
        _currentDialogue++;
        if (_currentDialogue >= 2)
            _currentDialogue = 1;
        print("Uhm?");
        NextDialogue();
    }

    public void WrongDialogue()
    {
        Debug.Log("Wrong Dialogue");
        RemoveListeners();
        NoRizz();
        ScoreManager.CurrentScore--;
        if (_slider.value >= 1f) 
            _slider.value -= 1f;
        Invoke("FishSlapScreen", 0);
    }

    private void FishSlapScreen()
    {
        FishSlapObject.SetActive(true);
        Invoke("FishSlapScreenFinished", 1.18f);
    }

    private void FishSlapScreenFinished()
    {
        FishSlapObject.SetActive(false);
        _animator.SetBool("fishingSlap", true);
        Invoke("FishingSlapFinished", 2f);
    }

    private void FishingSlapFinished()
    {
        _animator.SetBool("fishingSlap", false);
        // TODO: move player being able to move again down here
    }

    public void ChangeTextBoxes()
    {
        Debug.Log("After change: " + currentFish);
        Debug.Log("After change, erik: " + _erikNumber);
        Debug.Log("After change, dia: " + _currentDialogue);
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
        if (_erikNumber == 16)
            return;
        
        ScoreManager.CurrentScore++;
        _slider.value += 1f;
        caughtFishes.Add(_erikNumber);

    }

    public void NoRizz()
    {
        // TODO Hide entire canvas? Don't think there's much more
        _currentDialogue = 0;
        DisableOptions();
        _dialogueCanvas.SetActive(false);
        // TODO: Add disabling the fish here
        
        _FMOD.FishingMode(0);

        PlayerController._playerStatic = false;
        PlayerController._rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        PlayerController._isFishing = true;
        _animator.SetBool("playerStatic", false);
        PlayerController.fishingCanBeInterrupted = false;
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

    
