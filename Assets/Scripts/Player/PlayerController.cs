using UnityEngine;
using UnityEngine.Timeline;

public class PlayerController : MonoBehaviour
{
    [Header("Components")] 
    private InputActions _input;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;

    [Header("Movement")] 
    [SerializeField] private float moveSpeed = 5f;
    
    private void Start()
    {
        _input = GetComponent<InputActions>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void Update()
    {
        AdjustPlayerFacingDirection();
        
        /*
        if (!PauseMenu._menuActive)
        {
            _animator.SetFloat("moveY", _input.movement.y);
            _animator.SetFloat("moveX", _input.movement.x);
        }
        */
    }
    
    private void FixedUpdate()
    {
        _rigidbody2D.linearVelocity = _input.movement * moveSpeed;
    }

    private void AdjustPlayerFacingDirection()
    {
        if (_input.movement.x < 0)
        {
            _spriteRenderer.flipX = true;
            //PlayerFlipX = true;   Might not be needed
        }
        
        if (_input.movement.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
    }
    
}
