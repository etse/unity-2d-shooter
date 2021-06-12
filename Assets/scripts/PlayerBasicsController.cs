using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerBasicsController : MonoBehaviour
{
    public int health = 100;
    private float invulUntil = 0f;
    public GameObject healthBar;
    public Material flashMaterial;
    private Material defaultMaterial;
    public AudioSource sfx;

    // Start is called before the first frame update
    void Start()
    {
        defaultMaterial = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.GetComponent<Slider>().value = health;
        if (Time.time < invulUntil)
        {
            GetComponent<SpriteRenderer>().material = flashMaterial;
        } else
        {
            GetComponent<SpriteRenderer>().material = defaultMaterial;
        }
    }

    private void takeDamage(int damage)
    {
        var currentTime = Time.time;
        if (currentTime > invulUntil)
        {
            sfx.Play();
            invulUntil = currentTime + 0.2f;
            health -= damage;

            if (health <= 0)
            {
                SceneManager.LoadScene("MenuScene");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<IEnemy>();
        if (enemy != null)
        {
            takeDamage(20);
            var vectorAway = gameObject.transform.position - collision.gameObject.transform.position;
            var forceVector = vectorAway.normalized;
            forceVector.x *= 5;
            forceVector.y *= 8;
            GetComponent<PlayerMovementController>().setVelocity(forceVector);
        }
    }
}
