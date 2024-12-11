using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Components")] 
    private InputActions _input;
    public static Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    
    [Header("Movement")] 
    [SerializeField] private float moveSpeed = 5f;
    
    [Header("Biomes")]
    public static bool _isInOcean, _isInDeepsea, _isInTropical, _isInColdwater;
    
    [Header("Biome entry numbers")] 
    public static int OceanEntryNumber, DeepseaEntryNumber, TropicalEntryNumber, ColdwaterEntryNumber;
    
    [Header("Fishing")] 
    private float _fishingCooldown = 3f;
    private float _fishingCooldownTimer;
    private bool _fishingIdle;
    private bool _fishingThrow;
    private bool _fishingReelIn;
    
    public static bool _playerStatic;
    public static bool _isFishing = true;
    
    [SerializeField] 
    private SpriteRenderer uiESpriteRenderer;
    [SerializeField]
    private SpriteRenderer uiExclamationmarkSpriteRenderer;
    
    private float _uiCooldown = 15f;
    private float _uiCooldownTimer;
    private bool isDone = false;
    private bool isDone2 = false;
    public static bool fishingCanBeInterrupted = false;
    
    public TypewriterTest MyScript;
    public static int _kasperNumber;
    
    private void Start()
    {
        _input = GetComponent<InputActions>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void Update()
    {
        if (!_playerStatic)
        {
            AdjustPlayerFacingDirection();
        }
        
        if (_input.interact && Time.time > _fishingCooldownTimer && _isFishing)
        {
            _isFishing = false;
            _fishingCooldownTimer = Time.time + _fishingCooldown;
            _playerStatic = true;
            _animator.SetBool("playerStatic", true);
            _rigidbody2D.bodyType = RigidbodyType2D.Static;
            FishingMechanic();
        }
        
        if (_input.interact && fishingCanBeInterrupted && isDone2 == false)
        {
            CancelInvoke("QuickTimeEvent2");
            Invoke("FishingFailed", 0);
            print("Test");
        }
        
        if (_input.interact && isDone2)
        {
            Invoke("QuickTimeEvent3", 0);
            print("Invoking QuickTimeEvent3");
        }
        
        _animator.SetFloat("moveY", _input.movement.y);
        _animator.SetFloat("moveX", _input.movement.x);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    
    private void FixedUpdate()
    {
        _rigidbody2D.linearVelocity = _input.movement * moveSpeed;
    }

    private void AdjustPlayerFacingDirection()
    {
        if (_input.movement.x < 0)
        {
            _spriteRenderer.flipX = false;
        }
        
        if (_input.movement.x > 0)
        {
            _spriteRenderer.flipX = true;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ocean":
                _isInOcean = true;
                _isInColdwater = false;
                _isInDeepsea = false;
                _isInTropical = false;
                if (!isDone)
                {
                    // Can add time to the function invoke to fine tune when the UI element appears
                    Invoke("Activate_E_UI_Element", 0);
                    isDone = true;
                }
                break;
            case "Deepsea":
                _isInOcean = false;
                _isInColdwater = false;
                _isInDeepsea = true;
                _isInTropical = false;
                if (!isDone)
                {
                    Invoke("Activate_E_UI_Element", 0);
                    isDone = true;
                }
                break;
            case "Tropical":
                _isInOcean = false;
                _isInColdwater = false;
                _isInDeepsea = false;
                _isInTropical = true;
                if (!isDone)
                {
                    Invoke("Activate_E_UI_Element", 0);
                    isDone = true;
                }
                break;
            case "Coldwater":
                _isInOcean = false;
                _isInColdwater = true;
                _isInDeepsea = false;
                _isInTropical = false;
                if (!isDone)
                {
                    Invoke("Activate_E_UI_Element", 0);
                    isDone = true;
                }
                break;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        _isInOcean = false;
        _isInColdwater = false;
        _isInDeepsea = false;
        _isInTropical = false;
        // This if statement fixes a bug in unity where TriggerEnter and TriggerExit is run when an object
        // changes rigidbody type
        if (_rigidbody2D.bodyType == RigidbodyType2D.Dynamic)
        {
            isDone = false;
        }
    }
    
    private void FishingMechanic()
    {
        // Robin fiske musikk
        if (_isInOcean)
        {
            //_playerStatic = true;
            //_animator.SetBool("playerStatic", true);
            //_rigidbody2D.bodyType = RigidbodyType2D.Static;
            _animator.SetBool("fishingThrow", true);
            OceanEntryNumber = Random.Range(0, 3);
            Invoke("FishingIdle", 1);
            print("FishingOcean");
            _kasperNumber = OceanEntryNumber;
            if (MyScript.caughtFishes.Contains(_kasperNumber))
                _kasperNumber = 16;
            Debug.Log("Kasper sitt nummer er " + OceanEntryNumber);
        }
        if (_isInDeepsea)
        {
            _playerStatic = true;
            _animator.SetBool("playerStatic", true);
            _rigidbody2D.bodyType = RigidbodyType2D.Static;
            _animator.SetBool("fishingThrow", true);
            DeepseaEntryNumber = Random.Range(4, 7);
            Invoke("FishingIdle", 1);
            print("FishingDeepsea");
            _kasperNumber = DeepseaEntryNumber;
            if (MyScript.caughtFishes.Contains(_kasperNumber))
                _kasperNumber = 16;
            Debug.Log("Kasper sitt nummer er " + DeepseaEntryNumber);
        }
        if (_isInTropical)
        {
            _playerStatic = true;
            _animator.SetBool("playerStatic", true);
            _rigidbody2D.bodyType = RigidbodyType2D.Static;
            _animator.SetBool("fishingThrow", true);
            TropicalEntryNumber = Random.Range(8, 11);
            Invoke("FishingIdle", 1);
            print("FishingTropical");
            _kasperNumber = TropicalEntryNumber;
            if (MyScript.caughtFishes.Contains(_kasperNumber))
                _kasperNumber = 16;
            Debug.Log("Kasper sitt nummer er " + TropicalEntryNumber);
        }
        if (_isInColdwater)
        {
            _playerStatic = true;
            _animator.SetBool("playerStatic", true);
            _rigidbody2D.bodyType = RigidbodyType2D.Static;
            _animator.SetBool("fishingThrow", true);
            ColdwaterEntryNumber = Random.Range(12, 15);
            Invoke("FishingIdle", 1);
            print("FishingColdwater");
            _kasperNumber = ColdwaterEntryNumber;
            if (MyScript.caughtFishes.Contains(_kasperNumber))
                _kasperNumber = 16;
            Debug.Log("Kasper sitt nummer er " + ColdwaterEntryNumber);
        }
    }

    private void FishingIdle()
    {
        _animator.SetBool("fishingFailed", false);
        _animator.SetBool("fishingThrow", false);
        _animator.SetBool("fishingIdle", true);
        Invoke("FishingOnHook", 1);
        Debug.Log("FishingIdle");
    }

    private void FishingOnHook()
    {
        _animator.SetBool("fishingIdle", false);
        _animator.SetBool("fishingOnHook", true);
        Invoke("FishingOnHookLoop", 1);
        Debug.Log("FishingOnHook");
    }
    
    private void FishingOnHookLoop()
    {
        Invoke("QuickTimeEvent", 0);
        _animator.SetBool("fishingOnHook", false);
        _animator.SetBool("fishingOnHookLoop", true);
        fishingCanBeInterrupted = true;
        Debug.Log("FishingOnHookLoop");
    }
    
    private void FishingReelIn()
    {
        _animator.SetBool("fishingOnHookLoop", false);
        _animator.SetBool("fishingReelIn", true);
        Invoke("FishingAfterReelInForTesting", 1);
        Debug.Log("FishingReelIn");
    }
    
    private void FishingAfterReelInForTesting()
    {
        _animator.SetBool("fishingReelIn", false);
        //_animator.SetBool("playerStatic", false);
        MyScript.RizzMode();
        //CancelInvoke("FishingFailed");
        print("Success");
    }
    
    private void Activate_E_UI_Element()
    {
        uiESpriteRenderer.enabled = true;
        Invoke("Deactivate_E_UI_Element", 2);
    }
    
    private void Deactivate_E_UI_Element()
    {
        uiESpriteRenderer.enabled = false;
    }
    
    private void QuickTimeEvent()
    {
        var randomQuickTimeEventTimer = Random.Range(1, 5);
        // Debug.Log(randomQuickTimeEventTimer);
        Invoke("QuickTimeEvent2", randomQuickTimeEventTimer);
    }

    private void QuickTimeEvent2()
    {
        uiExclamationmarkSpriteRenderer.enabled = true;
        isDone2 = true;
        Invoke("FishingFailed", 2);
    }

    private void QuickTimeEvent3()
    {
        CancelInvoke("FishingFailed");
        uiExclamationmarkSpriteRenderer.enabled = false;
        isDone2 = false;
        Invoke("FishingReelIn", 0);
    }

    private void FishingFailed()
    {
        fishingCanBeInterrupted = false;
        isDone2 = false;
        uiExclamationmarkSpriteRenderer.enabled = false;
        _playerStatic = false;
        _animator.SetBool("playerStatic", false);
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        _animator.SetBool("fishingOnHookLoop", false);
        _animator.SetBool("fishingFailed", true);
        _isFishing = true;
        Debug.Log("FishingFailed, we'll get em' next time");
    }
}
