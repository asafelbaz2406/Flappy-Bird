using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWalls : MonoBehaviour
{
    [SerializeField] private float movingWallForce = 2f;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        if(rb != null )
        {
            rb.velocity = new Vector2(-movingWallForce, 0f);
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.gameOver)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if (this.transform.position.x < -25f)
        {
           // Debug.Log("Wall is outside the screen from the left!, Destroy()");
            Destroy(gameObject);
        }
    }
}
