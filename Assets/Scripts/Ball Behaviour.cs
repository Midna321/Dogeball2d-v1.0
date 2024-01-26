using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BallBehaviour : MonoBehaviour
{


    Rigidbody2D _ball;
    AudioSource _goalsound;

    [SerializeField] Rigidbody2D Player1;
    [SerializeField] Rigidbody2D AI;
    private float _goalCooldown = 2f;
    bool _canScore = true;
   


    void Start()
    {
        
        _ball = GetComponent<Rigidbody2D>();
        _goalsound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Net") && _canScore == true)       //La palla entra perchè di base _canScore è settato su true.
        {
            
            ScoreScriptBlue._scoreBlueValue += 1;
            if (_goalsound != null && _goalsound.clip != null)
            {
                _goalsound.PlayOneShot(_goalsound.clip);
            }

            StartCoroutine(ResetAfterDelay());
            StartCoroutine(ResetScoreCooldown());               //Chiama la coroutine ResetScoreCooldown.
                                                              //StartCoroutine calls the coroutine(ResetAfterDelay).
        }



        if (other.CompareTag("NetDx") && _canScore == true)       //La palla entra perchè di base _canScore è settato su true.
        {
            
            ScoreScript.scoreValue += 1;
            if (_goalsound != null && _goalsound.clip != null)
            {
                _goalsound.PlayOneShot(_goalsound.clip);
            }

            StartCoroutine(ResetAfterDelay());
            StartCoroutine(ResetScoreCooldown());               //Chiama la coroutine ResetScoreCooldown.

              //StartCoroutine calls the coroutine(ResetAfterDelay).

        }

    }



    private IEnumerator ResetScoreCooldown()
    {
        _canScore = false;                      //Setta temporaneamente _canScore a falso e impedisce alla palla di fare goal.
        yield return new WaitForSeconds(_goalCooldown);  //Dopo _goalCooldown secondi ritorna al normale svolgimento della funzione.
        _canScore = true;
    }

    public void ApplyKickForce(Vector2 force)
    {
        if (_ball != null)
        {
            _ball.AddForce(force, ForceMode2D.Impulse);  //Object.Addforce(vector name, Enumeration that specifies the type of force to apply). In this case, impulse is applied instantly, as if it cames from a kick.
        }
    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(2.5f);
        ResetPositions();
    }

    private void ResetPositions()
    {
        

        _ball.position = new Vector2(0f, 0f);
        _ball.velocity = Vector2.zero;

        Player1.position = new Vector2(-3.5f, 0f);

        AI.position = new Vector2(3.5f, 0f);
        AI.velocity = Vector2.zero;
    }
}
