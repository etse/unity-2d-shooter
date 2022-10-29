using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    public float speed = 15.0f;
    public int damage = 10;
    public Vector3 direction = Vector3.up;
    public Rigidbody2D rigidBody;
    public AudioSource hitSound;
    public bool friendly = true;
    private bool isDead = false;
    private float destroyAt;

    private void Start()
    {
        rigidBody.velocity = direction * speed;
    }

    // Update is called once per frame
    void Update()
    {
        removeIfOutsideScreen();
        if (isDead && Time.time > destroyAt) {
            Destroy(gameObject);
        }
    }

    void removeIfOutsideScreen()
    {
        var topOfScreen = Camera.main.ViewportToWorldPoint(Vector3.one).y;
        var bottomOfScreen = Camera.main.ViewportToWorldPoint(Vector3.one * -1).y;
        if (transform.position.y - GetComponent<SpriteRenderer>().bounds.size.y > topOfScreen)
        {
            Destroy(gameObject);
        }

        if (transform.position.y - GetComponent<SpriteRenderer>().bounds.size.y < bottomOfScreen)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(friendly)
        {
            checkCollisionEnemy(collision);
        } else
        {
            checkCollisionPlayer(collision);
        }
    }

    private void checkCollisionEnemy(Collider2D collision)
    {
        var enemy = collision.GetComponent<BasicEnemy>();
        if (enemy != null && isDead == false)
        {
            enemy.takeDamage(damage);
            destroyBullet();
        }
    }

    private void checkCollisionPlayer(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerBasicsController>();
        if (player != null && isDead == false)
        {
            player.takeDamage(damage);
            destroyBullet();
        }
    }

    private void destroyBullet()
    {
        hitSound.Play();
        isDead = true;
        destroyAt = Time.time + 0.7f;
        rigidBody.velocity = new Vector2(0, 0);
        GetComponent<SpriteRenderer>().forceRenderingOff = true;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
