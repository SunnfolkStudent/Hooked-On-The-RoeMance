using System.Collections;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;


[RequireComponent(typeof(TMP_Text))]
public class TypewriterTest : MonoBehaviour
{
    private TMP_Text _textBox;
    
    
    // Prototyping
    [Header("Typewriter Test")] [SerializeField]
    private string testText;
    
    
    // basic functionality 
    private int _currentVisibleCharacterIndex;
    private Coroutine _typewriterCoroutine;
    
    private WaitForSeconds _simpleDelay;
    private WaitForSeconds _interpunctuationDelay;
    
    [Header("Typewriter Settings")]
    [SerializeField] private float charactersPerSecond = 48;
    [SerializeField] private float interpunctuationDelay = 0.5f;
    
    // Skipping functionality
    public bool currentlySkipping {get; private set;}
    private WaitForSeconds _skipDelay;
    
    [Header("Skip Options")]
    [SerializeField] private bool quickSkip;

    [SerializeField] [Min(1)] private int skipSpeedup = 5;
    
    // Event functionality
    private WaitForSeconds _textBoxFullEventDelay;
    [SerializeField] [Range(0.1f, 0.5f)] private float sendDoneDelay = 0.25f;

    //public static event Action CompleteTextRevealed;
    //public static event 
    


    private void Awake()
    {
        _textBox = GetComponent<TMP_Text>();
        
        _simpleDelay = new WaitForSeconds(1 / charactersPerSecond);
        _interpunctuationDelay = new WaitForSeconds(interpunctuationDelay);
        
        _skipDelay = new WaitForSeconds(1 / (charactersPerSecond * skipSpeedup));
    }

    private void Start()
    {
        SetText(testText);
    }

    private void Update()
    {
        // Nothing for now :D
    }

    private void OnAttack()
    {
        if (_textBox.maxVisibleCharacters != _textBox.textInfo.characterCount - 1)
            Skip(); 
    }

    public void SetText(string text)
    {
        if (_typewriterCoroutine != null)
            StopCoroutine(_typewriterCoroutine);
        
        
        _textBox.text = text;
        _textBox.maxVisibleCharacters = 0;
        _currentVisibleCharacterIndex = 0;

        _typewriterCoroutine = StartCoroutine(Typewriter());
    }


    private IEnumerator Typewriter()
    {
        TMP_TextInfo textInfo = _textBox.textInfo;
        
        while (_currentVisibleCharacterIndex < textInfo.characterCount +1)
        {
            
            
            
            
            
            
            char character = textInfo.characterInfo[_currentVisibleCharacterIndex].character;
            
            _textBox.maxVisibleCharacters++;

            if (!currentlySkipping && 
                character is '?' or '.' or ',' or ':' or ';' or '!' or '-')
            {
                yield return interpunctuationDelay;
            }
            else
            {
                yield return currentlySkipping ? _skipDelay : _simpleDelay;
            }
            
            
            
            _currentVisibleCharacterIndex++;
            
        }
    }

    void Skip()
    {
        if (currentlySkipping)
            return;
        currentlySkipping = true;

        if (!quickSkip)
        {
            StartCoroutine(SkipSpeedupReset());
            return;
        }
        
        //StopCoroutine(_typewriterCoroutine());
        _textBox.maxVisibleCharacters = _textBox.textInfo.characterCount;
    }


    private IEnumerator SkipSpeedupReset()
    {
        yield return new WaitUntil(() => _textBox.maxVisibleCharacters == _textBox.textInfo.characterCount -1);
        currentlySkipping = false;
    }
}
