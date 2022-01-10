﻿using UnityEngine;

public class EnemyX : MonoBehaviour
{
    private float speed = 2f;
    private Rigidbody _enemyRb;
    private GameObject _playerGoal;

    // Start is called before the first frame update
    private void Start()
    {
        _enemyRb = GetComponent<Rigidbody>();
        _playerGoal = GameObject.Find("Player Goal");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // Set enemy direction towards player goal and move there
        var lookDirection = (_playerGoal.transform.position - transform.position).normalized;
        _enemyRb.AddForce(lookDirection * speed);

    }

    private void OnCollisionEnter(Collision other)
    {
        // If enemy collides with either goal, destroy the enemy
        if (other.gameObject.name == "Enemy Goal")
        {
            Debug.Log("Hit enemy goal!");
            Destroy(gameObject);
        }
        else if (other.gameObject.name == "Player Goal")
        {
            Debug.Log("Hit enemy goal!");
            Destroy(gameObject);
        }

    }

}
