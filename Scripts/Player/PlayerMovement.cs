using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator birdAnimator;

    public float jumpforce;
    public float speed = 0.01f;
    public int score { get; private set; }

    private void Start()
    {
        birdAnimator = GetComponentInChildren<Animator>();
        if(birdAnimator == null )
        {
            Debug.Log("birdAnimator is NULL");
        }
        else
        {
            birdAnimator.enabled = true;
        }
        GameManager.Instance.OnScoreEvent += IncrementScore;
    }

    void Update()
    {
        if (GameManager.Instance.gameOver) 
        {
            return;
        }

        if (Input.GetKeyDown("space"))
        {
            rb.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Tree" || other.collider.tag == "Floor") 
        {
            //Debug.Log("Player's death!");
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
                birdAnimator.enabled = false;
            }
            GameManager.Instance.SetScore(score);
            GameManager.Instance.OnPlayerDeathEvent?.Invoke();
        }

        
       // Debug.Log(other.collider.name + "   " + other.gameObject.name);
    }

    public void IncrementScore()
    {
        score++;
    }
}
