using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public int health = 20;
    public int score = 50;

    public Material flashMaterial;
    public bool isDead = false;

    private Material defaultMaterial;
    private float flashUntil = 0f;
    private bool isFlashing = false;
    private CameraShaker cameraShaker;
    private PlayerState playerScore;
    private INPCBehaviour npcBehaviour;


    // Start is called before the first frame update
    protected void Start()
    {
        defaultMaterial = GetComponent<SpriteRenderer>().material;
        cameraShaker = CameraShaker.Instance;
        playerScore = PlayerState.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (npcBehaviour != null )
        {
            npcBehaviour.Update(this);
        }

        removeIfBelowScreen();

        if(isFlashing && Time.time > flashUntil)
        {
            this.isFlashing = false;
            GetComponent<SpriteRenderer>().material = defaultMaterial;
        }

        if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("isDead"))
        {
            Destroy(gameObject);
        }
    }

    void removeIfBelowScreen()
    {
        var bottomOfScreen = Camera.main.ViewportToWorldPoint(-Vector3.one).y;
        if (transform.position.y + GetComponent<SpriteRenderer>().bounds.size.y < bottomOfScreen)
        {
            Destroy(gameObject);
        }
    }

    public void takeDamage(int damage)
    {
        if (!isDead)
        {
            health -= damage;
            flash();
            if (health <= 0)
            {
                cameraShaker.Shake(0.3f, 0.07f);
                playerScore.addPoints(score);
                isDead = true;
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<AudioSource>().Play();
                GetComponent<Animator>().SetBool("isDead", true);
            }
        }
    }

    public void shoot()
    {
        if(!isDead)
        {
            GetComponent<IEnemyWeapon>().shoot(gameObject.transform);
        }
    }

    private void flash()
    {
        isFlashing = true;
        flashUntil = Time.time + 0.07f;
        GetComponent<SpriteRenderer>().material = flashMaterial;
    }

    public void setBehaviour(INPCBehaviour behaviour)
    {
        npcBehaviour = behaviour;
    }
}
