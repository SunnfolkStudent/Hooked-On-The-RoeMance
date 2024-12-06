using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")] 
    private InputActions _input;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    [Header("Movement")] 
    [SerializeField] private float moveSpeed = 5f;
    
    [Header("Biomes")]
    private bool _isInOcean, _isInDeepsea, _isInTropical, _isInColdwater;

    [Header("Fishing Arrays")] 
    public List<ScriptableObject> fishingInTropical;

    [Header("Fishing")] 
    private float _fishingCooldown = 1f;
    private float _fishingCooldownTimer;
    private bool _fishingIdle;
    private bool _fishingThrow;
    private bool _fishingReel;
    
    private void Start()
    {
        _input = GetComponent<InputActions>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void Update()
    {
        AdjustPlayerFacingDirection();

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
            _animator.SetBool("fishingThrow", true);
            Invoke("FishingIdle", 1);
            //_rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
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
        
        /*
         Pseudo-code:
         Randomly get one of the scriptable object items belonging to one of the specific biomes
         then Kasper's script will make use of the Scrob that is chosen
         */
        
    }

    private void FishingIdle()
    {
        _animator.SetBool("fishingThrow", false);
        _animator.SetBool("fishingIdle", true);
    }
}
