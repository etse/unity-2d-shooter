using UnityEngine;
using System.Collections;

public class PlayerWeaponController: MonoBehaviour
{
    public Transform firepoint_left;
    public Transform firepoint_right;
    public GameObject bullet;
    public AudioSource sfx;
    
    private float cooldown = 0.3f;
    private float cooldownTil = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            shoot();
        }
    }

    private void shoot()
    {
        if (Time.time > cooldownTil)
        {
            cooldownTil = Time.time + cooldown;
            sfx.Play();
            Instantiate(bullet, firepoint_left.position, firepoint_left.rotation);
            Instantiate(bullet, firepoint_right.position, firepoint_right.rotation);
        }
    }
}
