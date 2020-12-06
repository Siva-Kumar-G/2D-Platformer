using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Text scoreText;

    public Rigidbody2D rb;
    public float movementSpeed = 20f;
    public float movementSmooth = 0.23f;
    private float _direction;

    private bool _isFlipped;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private static readonly int Walk = Animator.StringToHash("Walk");

    public Transform groundCheckPoint;


    public float jumpForce = 200f;
    public float groundCheckRadius = 0.5f;


    private bool _isGrounded;
    private static readonly int Jump = Animator.StringToHash("Jump");


    public LayerMask groundCheckLayer;
    private static readonly int DoJump = Animator.StringToHash("DoJump");


    private Vector3 _velocity = Vector3.zero;


    private int _score;


    public AudioClip jumpAudio, coinCollectAudio, dieAudio;

    public AudioSource _audioSource;


    public GameObject gameOverPanel, winPanel;


    private bool _isGameOver;


    public GameObject pauseBtn; //todo need to find a place to switch this

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // if (groundedObjects != null)
        // {
        //     Debug.Log(groundedObjects.tag);
        //     //todo Need to add the ground check layer...
        //     // if (groundedObjects.tag.Equals("Platform"))
        //     // {
        //     _isGrounded = true;
        //  
        //     // }
        //
        //     // foreach (var VARIABLE in groundedObjects)
        //     // {
        //     //     if (VARIABLE.tag.Equals("Platform"))
        //     //     {
        //     //         _isGrounded = true;
        //     //         _animator.SetBool(Jump, false);
        //     //     }
        //     // }
        // }
#if UNITY_EDITOR
        _direction = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnClick_Jump();
        }
#endif
    }

    private void FixedUpdate()
    {
        if (_isGameOver)
            return;

        _isGrounded = false;
        var groundedObjects =
            Physics2D.OverlapCircleAll(groundCheckPoint.position, groundCheckRadius, groundCheckLayer);

        foreach (var VARIABLE in groundedObjects)
        {
            if (VARIABLE.gameObject != gameObject)
            {
                _isGrounded = true;
                _animator.SetBool(Jump, false);
            }
        }

        Vector3 targetVelocity = new Vector2((_direction * movementSpeed * Time.fixedDeltaTime) * 10, rb.velocity.y);

        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref _velocity, movementSmooth);


        if (Mathf.Abs(_direction) > 0)
        {
            //Need to play Walk Anim
            _animator.SetBool(Walk, true);
        }
        else
        {
            //Need to Play Idle Anim
            _animator.SetBool(Walk, false);
        }

        if (_direction > 0 && _isFlipped)
        {
            Flip();
        }
        else if (_direction < 0 && !_isFlipped)
        {
            Flip();
        }

        //  rb.velocity = new Vector2(_direction * movementSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    private void Flip()
    {
        _isFlipped = !_isFlipped;

        //_spriteRenderer.flipX = _isFlipped;


        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


    public void OnMovePressed(int value)
    {
        _direction = value;
    }

    public void OnMoveReleased()
    {
        _direction = 0;
    }

    public void OnClick_Jump()
    {
        if (_isGrounded && !_isGameOver && Time.timeScale != 0)
        {
            _isGrounded = false;

            rb.AddForce(new Vector2(0, jumpForce));
            Debug.Log("Jumped");

            //Todo need to play Jump Anim..
            _animator.SetBool(Jump, true);
            _animator.SetTrigger(DoJump);

            _audioSource.PlayOneShot(jumpAudio);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(groundCheckPoint.position, groundCheckRadius);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag.Equals("Egg"))
        {
            Debug.Log("Egg Captured");
            Destroy(other.gameObject);
            _score = _score + 1;
            scoreText.text = _score.ToString();

            _audioSource.PlayOneShot(coinCollectAudio);
        }
        else if (other.gameObject.tag.Equals("Die"))
        {
            //todo need to show Died panel 
            _audioSource.PlayOneShot(dieAudio);
            gameOverPanel.SetActive(true);

            _isGameOver = true;
            
            pauseBtn.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("GameOver"))
        {
            //todo need to show Lvl Complete Screen
            winPanel.SetActive(true);

            _isGameOver = true;
            
            pauseBtn.SetActive(false);
        }
    }
}