using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f; // Adjust the speed as needed
    public float moveDistance = 5f; // Adjust the move distance as needed
    private bool isMovingRight = true;
    private SpriteRenderer spriteRenderer;
    private float distanceTraveled = 0f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent < SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
    }

    void MoveEnemy()
    {
        float movement = speed * Time.deltaTime;

        if (isMovingRight)
        {
            transform.Translate(Vector2.right * movement);
        }
        else
        {
            transform.Translate(Vector2.left * movement);
        }

        // Update the distance traveled
        distanceTraveled += Mathf.Abs(movement);

        // Check if the enemy has moved the desired distance, and if so, change direction
        if (distanceTraveled >= moveDistance)
        {
            isMovingRight = !isMovingRight;
            FlipSprite();

            // Reset the distance traveled
            distanceTraveled = 0f;
        }
    }

    void FlipSprite()
    {
        // Flip the sprite horizontally
        spriteRenderer.flipX = isMovingRight;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ContactPoint2D contact = collision.GetContact(0);

            if (contact.normal.y < 0)
            {
                Destroy(gameObject);
            }
            else
            {
                collision.gameObject.GetComponent<PlayerController>().Die();
            }
        }
    }
}
