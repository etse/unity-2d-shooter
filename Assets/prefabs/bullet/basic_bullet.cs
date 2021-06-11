using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basic_bullet : MonoBehaviour
{
    private float speed = 20.0f;
    public Rigidbody2D rigidBody;
    public AudioSource hitSound;
    private bool isDead = false;
    private float destroyAt;

    private void Start()
    {
        rigidBody.velocity = transform.up * speed;
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
        if (transform.position.y - GetComponent<SpriteRenderer>().bounds.size.y > topOfScreen)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<IEnemy>();
        if (enemy != null && isDead == false)
        {
            enemy.takeDamage(10);
            hitSound.Play();
            isDead = true;
            destroyAt = Time.time + 0.7f;
            rigidBody.velocity = new Vector2(0, 0);
            GetComponent<SpriteRenderer>().forceRenderingOff = true;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
