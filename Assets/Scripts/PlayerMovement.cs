using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField] BallBehaviour _ball;
    SpriteRenderer _spriteRenderer;
    float _inputHorizontal;
    float _inputVertical;
    float _walkSpeed;
    float _runSpeed;
    float _kickForce;

    Color _originalColor;

    AudioSource _kicksound;


    void Start()
    {

        
        _rb = GetComponent<Rigidbody2D>();
        _walkSpeed = 1.0f;
        _runSpeed = 2.0f;
        _kickForce = 6.0f;

        _spriteRenderer = GetComponent<SpriteRenderer>();

        if(_spriteRenderer != null )
        {
            _originalColor = _spriteRenderer.color;
        }

        _kicksound = GetComponent<AudioSource>();

    }

    private void OnCollisionEnter2D(Collision2D collisionbp)                      //Kicking system.
    {
        

        if (collisionbp.gameObject.tag == "Ball" && Input.GetMouseButton(0))        
        {

            if(_kicksound != null && _kicksound.clip != null)
            {
                _kicksound.PlayOneShot(_kicksound.clip);
            }

            Vector2 kickDirection = _ball.transform.position - transform.position; //We create a vector called kick direction. It takes the position of the ball - position of the object.
            kickDirection.Normalize();  //it then normalizes it to a vector with lenght 1.


            _ball.ApplyKickForce(kickDirection * _kickForce); //it then recall AppliKickForce.
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        _inputHorizontal = Input.GetAxisRaw("Horizontal");
        _inputVertical = Input.GetAxisRaw("Vertical");

        if(Input.GetMouseButton(0) && _spriteRenderer != null )                     //Change color when kick status.
        {
            _spriteRenderer.color = Color.magenta;
        }
        else
        {
            if(_spriteRenderer != null)
            {
                _spriteRenderer.color = _originalColor;
            }
        }
       

        if (_inputHorizontal != 0 || _inputVertical != 0)
        {
            _rb.AddForce(new Vector2(_inputHorizontal * _walkSpeed, 0f));
            _rb.AddForce(new Vector2(0f, _inputVertical * _walkSpeed));
        }

        if(Input.GetKey("space") && (_inputHorizontal !=0 || _inputVertical != 0))         //Speed
        {
             
            _rb.AddForce(new Vector2(_inputHorizontal * _runSpeed, 0f));
            _rb.AddForce(new Vector2(0f, _inputVertical * _runSpeed));
        }

        
    }
}
