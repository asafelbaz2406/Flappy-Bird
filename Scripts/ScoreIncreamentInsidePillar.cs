using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreIncreamentInsidePillar : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
         GameManager.Instance.UpdateScore(); // Player Collides with Pillar, Score++
    }
}