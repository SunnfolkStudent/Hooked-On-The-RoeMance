using System.Collections.Generic;
using UnityEngine;

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
    private bool _isInOcean, _isInDeepsea, _isInTropical, _isInColdwater;
    
    [Header("Fishing Lists/Arrays")] 
    public List<ScriptableObject> fishingInOcean;
    public List<ScriptableObject> fishingInDeepsea;
    public List<ScriptableObject> fishingInTropical;
    public List<ScriptableObject> fishingInColdwater;
    
    [Header("Fishing")] 
    private float _fishingCooldown = 1f;
    private float _fishingCooldownTimer;
    private bool _fishingIdle;
    private bool _fishingThrow;
    private bool _fishingReelIn;

    public static bool _playerStatic;
    
    private void Start()
    {
        _input = GetComponent<InputActions>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        for (int i = 0; i < fishingInTropical.Count; i++)
        {
            Debug.Log(fishingInTropical[i].name);
        }
    }
    
    private void Update()
    {
        if (!_playerStatic)
        {
            AdjustPlayerFacingDirection();
        }
        

        if (_input.interact && Time.time > _fishingCooldownTimer)
        {
            _fishingCooldownTimer = Time.time + _fishingCooldown;
            FishingMechanic();
        }
            
        
        _animator.SetFloat("moveY", _input.movement.y);
        _animator.SetFloat("moveX", _input.movement.x);
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
                break;
            case "Deepsea":
                _isInOcean = false;
                _isInColdwater = false;
                _isInDeepsea = true;
                _isInTropical = false;
                break;
            case "Tropical":
                _isInOcean = false;
                _isInColdwater = false;
                _isInDeepsea = false;
                _isInTropical = true;
                break;
            case "Coldwater":
                _isInOcean = false;
                _isInColdwater = true;
                _isInDeepsea = false;
                _isInTropical = false;
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isInOcean = false;
        _isInColdwater = false;
        _isInDeepsea = false;
        _isInTropical = false;
    }
    
    private void FishingMechanic()
    {
        if (_isInOcean)
        {
            // TODO: Make a button saying to press E pop up
            // TODO: Make player unable to move when both fishing and dialogue is on screen
            _playerStatic = true;
            _animator.SetBool("playerStatic", true);
            _rigidbody2D.bodyType = RigidbodyType2D.Static;
            _animator.SetBool("fishingThrow", true);
            // Tropical is placeholder for testing, should be ocean
            var chosenOceanArrayEntry = fishingInTropical[Random.Range(0, fishingInTropical.Count)];
            Invoke("FishingIdle", 1);
            print("FishingOcean");
            
        }
        if (_isInDeepsea)
        {
            print("FishingDeepsea");
        }
        if (_isInColdwater)
        {
            print("FishingColdwater");
        }
        if (_isInTropical)
        {
            print("FishingTropical");
        }
    }

    private void FishingIdle()
    {
        _animator.SetBool("fishingThrow", false);
        _animator.SetBool("fishingIdle", true);
        // TODO: Random time interval for ! pop-up when fishing, 3-7 seconds maybe
        Invoke("FishingOnHook", 1);
    }

    private void FishingOnHook()
    {
        _animator.SetBool("fishingIdle", false);
        _animator.SetBool("fishingOnHook", true);
        Invoke("FishingOnHookLoop", 1);
    }

    // One of these two might get dropped, the idea is for an exclamation mark to pop up when a fish bites
    // and for the player to press E to catch the fish (quick time event)
    
    private void FishingOnHookLoop()
    {
        _animator.SetBool("fishingOnHook", false);
        _animator.SetBool("fishingOnHookLoop", true);
        Invoke("FishingReelIn", 1);
    }
    
    // Might have to use a coroutine rather than Invoke on the functions before FishingReelIn
    // has to do with taking player input (E) to reel in the catch
    
    private void FishingReelIn()
    {
        _animator.SetBool("fishingOnHookLoop", false);
        _animator.SetBool("fishingReelIn", true);
        // Might want to use a coroutine to wait for animation to finish before running pseudo-code
        Invoke("FishingAfterReelInForTesting", 1);
        /*
         Pseudo-code:
         Randomly get one of the scriptable object items belonging to one of the specific biomes
         then Kasper's script will make use of the Scrob that is chosen
         */
    }

    private void FishingAfterReelInForTesting()
    {
        _animator.SetBool("fishingReelIn", false);
        _playerStatic = false;
        _animator.SetBool("playerStatic", false);
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        print("Success");
        // Run Kasper's dialogue mechanic after this, Rigidbody and _playerStatic should be set after his mechanic is done
        // Kasper's script will need the randomly chosen item from the respective fish biome array
    }
}
