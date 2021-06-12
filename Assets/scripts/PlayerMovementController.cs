using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private Vector2 velocity = Vector2.zero;
    private float acceleration = 45.0f;
    private float maxSpeed = 15.0f;
    private float dampening = 10.0f;

    // Update is called once per frame
    void Update()
    {
        updateVelocity();
        transform.Translate(velocity * Time.deltaTime);
        forceInsideBounds();
    }

    public void setVelocity(Vector3 newVelocity)
    {
        velocity = newVelocity;
    }

    private void updateVelocity()
    {
        Vector2 direction = Vector2.zero;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            direction.y += 1.0f;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            direction.x -= 1.0f;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            direction.y -= 1.0f;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            direction.x += 1.0f;
        }

        if (direction.x * velocity.x <= 0)
        {
            velocity.x = velocity.x - (velocity.x * dampening * Time.deltaTime);
        }

        if (direction.y * velocity.y <= 0)
        {
            velocity.y = velocity.y - (velocity.y * dampening * Time.deltaTime);
        }


        direction = direction.normalized * acceleration * Time.deltaTime;
        velocity = velocity + direction;

        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }
    }

    private void forceInsideBounds()
    {
        var newX = transform.position.x;
        var newY = transform.position.y;

        var left = Camera.main.ViewportToWorldPoint(Vector3.zero).x;
        var right = Camera.main.ViewportToWorldPoint(Vector3.one).x;
        var top = Camera.main.ViewportToWorldPoint(Vector3.zero).y;
        var bottom = Camera.main.ViewportToWorldPoint(Vector3.one).y;

        if (transform.position.x > right)
        {
            newX = right;
            velocity.x = 0;
        }
        if (transform.position.y > bottom)
        {
            newY = bottom;
            velocity.y = 0;
        }
        if (transform.position.x < left)
        {
            newX = left;
            velocity.x = 0;
        }
        if (transform.position.y < top)
        {
            newY = top;
            velocity.y = 0;
        }

        transform.position = new Vector3(newX, newY, 0.0f);
    }
}
