using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    private Vector2 velocity = Vector2.zero;
    private float acceleration = 35.0f;
    private float maxSpeed = 15.0f;
    private float maxSpeedShooting = 7.5f;
    private float dampening = 10.0f;
    PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
        if (!playerControls.Player.enabled)
        {
            playerControls.Player.Enable();
            playerControls.Cheat.Enable();

            playerControls.Cheat.IncreaseWeaponLevel.performed += increaseWeaponLevel;
            playerControls.Cheat.DecreaseWeaponLevel.performed += decreaseWeaponLevel;
        }
    }

    private void OnDestroy()
    {
        playerControls.Cheat.IncreaseWeaponLevel.performed -= increaseWeaponLevel;
        playerControls.Cheat.IncreaseWeaponLevel.performed -= decreaseWeaponLevel;
    }

    void increaseWeaponLevel(InputAction.CallbackContext ctx)
    {
        ShipUpgrades.getInstance().increaseWeaponLevel();
    }

    void decreaseWeaponLevel(InputAction.CallbackContext ctx)
    {
        ShipUpgrades.getInstance().decreaseWeaponLevel();
    }

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
        Vector2 direction = playerControls.Player.Movement.ReadValue<Vector2>();

        if (direction.x * velocity.x <= 0)
        {
            velocity.x = velocity.x - (velocity.x * dampening * Time.deltaTime);
        }

        if (direction.y * velocity.y <= 0)
        {
            velocity.y = velocity.y - (velocity.y * dampening * Time.deltaTime);
        }


        direction = direction * acceleration * Time.deltaTime;
        velocity = velocity + direction;

        var isShooting = playerControls.Player.Shoot.IsPressed();
        var maxSpeedToUse = isShooting ? maxSpeedShooting : maxSpeed;

        if (velocity.magnitude > maxSpeedToUse)
        {
            velocity = velocity.normalized * maxSpeedToUse;
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
