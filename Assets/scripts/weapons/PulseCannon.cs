using UnityEngine;

public class PulseCannon : IWeapon
{
    private float cooldown = 0.3f;
    private float cooldownTil = 0;
    public GameObject basicBullet;
    public new static string name = "Pulse Cannon";
    public AudioSource shootSFX;

    private Vector3 posFarLeft;
    private Vector3 posFarRight;
    private Vector3 posCenter;
    private Vector3 posLeft;
    private Vector3 posRight;

    public void Start()
    {
        updatePositionVectors();
    }

    public override void shoot()
    {
        if (Time.time > cooldownTil)
        {
            cooldownTil = Time.time + cooldown;
            updatePositionVectors();

            if (shootSFX != null)
            {
                shootSFX.Play();
            }

            switch (ShipUpgrades.getInstance().getWeaponUpgradeLevel())
            {
                case 1:
                    shootLevel1();
                    break;
                case 2:
                    shootLevel2();
                    break;
                case 3:
                    shootLevel3();
                    break;
                case 4:
                    shootLevel4();
                    break;
                case 5:
                    shootLevel5();
                    break;
                case 6:
                    shootLevel6();
                    break;
                default:
                    shootLevel1();
                    break;
            }
        }

    }

    private void updatePositionVectors()
    {
        posFarLeft = gameObject.transform.position + new Vector3(-0.22f, 0.5f, 0f);
        posFarRight = gameObject.transform.position + new Vector3(0.25f, 0.5f, 0f);
        posLeft = gameObject.transform.position + new Vector3(-0.05f, 0.55f, 0f);
        posRight = gameObject.transform.position + new Vector3(0.06f, 0.55f, 0f);
        posCenter = gameObject.transform.position + new Vector3(0f, 0.5f, 0f);
    }

    private void shootLevel1()
    {
        spawnBullet(posCenter);
    }

    private void shootLevel2()
    {
        cooldownTil = Time.time + cooldown;
        spawnBullet(posLeft);
        spawnBullet(posRight);
    }

    private void shootLevel3()
    {
        cooldownTil = Time.time + cooldown;
        spawnBullet(posLeft);
        spawnBullet(posRight);
        spawnBullet(posFarLeft);
        spawnBullet(posFarRight);
    }

    private void shootLevel4()
    {
        cooldownTil = Time.time + cooldown;
        spawnBullet(posRight, 1.4f, 2f);
        spawnBullet(posLeft, 1.4f, 2f);
        spawnBulletRotated(posFarLeft, 10);
        spawnBulletRotated(posFarRight, -10);
    }

    private void shootLevel5()
    {
        cooldownTil = Time.time + cooldown;
        spawnBullet(posRight, 1.4f, 2f);
        spawnBullet(posLeft, 1.4f, 2f);
        spawnBulletRotated(posFarLeft, 5);
        spawnBulletRotated(posFarRight, -5);
        spawnBulletRotated(posFarLeft, 10);
        spawnBulletRotated(posFarRight, -10);
    }

    private void shootLevel6()
    {
        cooldownTil = Time.time + cooldown;
        spawnBullet(posRight, 1.6f, 2.5f);
        spawnBullet(posLeft, 1.6f, 2.5f);

        spawnBulletRotated(posFarLeft, 3f, 1.4f, 2f);
        spawnBulletRotated(posFarRight, -3f, 1.4f, 2f);

        spawnBulletRotated(posFarLeft, 7);
        spawnBulletRotated(posFarRight, -7);

        spawnBulletRotated(posFarLeft, 10);
        spawnBulletRotated(posFarRight, -10f);
    }

    private void spawnBullet(Vector3 position)
    {
        Instantiate(basicBullet, position, Quaternion.Euler(Vector3.forward));
    }

    private void spawnBullet(Vector3 position, float scale, float damageFactor)
    {
        var bullet = Instantiate(basicBullet, position, Quaternion.Euler(Vector3.forward));
        bullet.transform.localScale = new Vector3(bullet.transform.localScale.x * scale, bullet.transform.localScale.y * scale, bullet.transform.localScale.z);
        bullet.GetComponent<BasicBullet>().damage = Mathf.RoundToInt(bullet.GetComponent<BasicBullet>().damage * damageFactor);
    }

    private void spawnBulletRotated(Vector3 position, float rotation)
    {
        var bullet = Instantiate(basicBullet, position, Quaternion.Euler(Vector3.forward * rotation));
        var radians = Mathf.Deg2Rad * (rotation * -1);
        var rotationVector = new Vector3(Mathf.Sin(radians), Mathf.Cos(radians), 0f);
        bullet.GetComponent<BasicBullet>().direction = rotationVector;
    }

    private void spawnBulletRotated(Vector3 position, float rotation, float scale, float damageFactor)
    {
        var bullet = Instantiate(basicBullet, position, Quaternion.Euler(Vector3.forward * rotation));
        bullet.transform.localScale = new Vector3(bullet.transform.localScale.x * scale, bullet.transform.localScale.y * scale, bullet.transform.localScale.z);

        var radians = Mathf.Deg2Rad * (rotation * -1);
        var rotationVector = new Vector3(Mathf.Sin(radians), Mathf.Cos(radians), 0f);
        BasicBullet bulletComponent = bullet.GetComponent<BasicBullet>();
        bulletComponent.direction = rotationVector;
        bulletComponent.damage = Mathf.RoundToInt(bullet.GetComponent<BasicBullet>().damage * damageFactor);
    }
}
