using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2 : MonoBehaviour
{
    public Transform leftSpawn;
    public Transform centerSpawn;
    public Transform rightSpawn;
    public GameObject basicEnemy;
    public GameObject bigEnemy;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(runLevels());
    }

    private IEnumerator runLevels()
    {
        yield return StartCoroutine(createFifthWave());
        yield return new WaitForSeconds(5.0f);
        yield return createBossWave();


        yield return new WaitForSeconds(5.0f);
        PlayerState.Instance.currentLevel += 1;
        SceneManager.LoadScene("MenuScene");
    }

    private IEnumerator createFifthWave()
    {
        var spawnOrder = new List<Vector3> { centerSpawn.position, leftSpawn.position, centerSpawn.position, rightSpawn.position };

        for (var x = 0; x < 10; x++)
        {
            var behaviourLeft = new EnemyAccelerating()
                .withSpeed(new Vector3(0f, -1.5f, 0f))
                .withAcceleration(new Vector3(0.3f, -0.5f, 0f));

            var behaviourRight = new EnemyAccelerating()
                .withSpeed(new Vector3(0f, -1.5f, 0f))
                .withAcceleration(new Vector3(-0.3f, -0.5f, 0f));

            var behaviourDown = new EnemyAccelerating()
                .withSpeed(new Vector3(0f, -1.5f, 0f))
                .withAcceleration(new Vector3(-0.0f, -0.5f, 0f));

            var spawnPosition = spawnOrder[x % spawnOrder.Count];

            spawnBigWithBehaviour(spawnPosition + new Vector3(0f, 0.6f, 0f), behaviourDown);
            spawnWithBehaviour(spawnPosition + new Vector3(0.4f, 0f, 0f), behaviourLeft);
            spawnWithBehaviour(spawnPosition + new Vector3(-0.4f, 0f, 0f), behaviourRight);

            yield return new WaitForSeconds(2.0f);
        }
    }

    private IEnumerator createBossWave()
    {
        var enemy = Instantiate(bigEnemy, centerSpawn.position, Quaternion.identity);
        var enemyScript = enemy.GetComponent<BasicEnemy>();

        var shootAndSlide = new CombinedBehaviour()
            .addBehaviour(new EnemyShooting())
            .addBehaviour(new SlideSideToSide(6.5f, 4f));

        var behaviour = new SequenceBehaviour()
            .addBehaviour(new EnemySlideOverScreen(), 1f)
            .addBehaviour(shootAndSlide, 10f);

        enemy.transform.localScale = new Vector3(2.3f, 2.3f, 1f);

        enemyScript.health *= 20;
        enemyScript.setBehaviour(behaviour);

        while (enemyScript.health > 0)
        {
            yield return new WaitForSeconds(0.5f);
        }
    }

    private GameObject spawnWithBehaviour(Vector3 location, INPCBehaviour behaviour)
    {
        var enemy = Instantiate(basicEnemy, location, Quaternion.identity);
        var enemyScript = enemy.GetComponent<BasicEnemy>();
        enemyScript.setBehaviour(behaviour);
        return enemy;
    }

    private GameObject spawnBigWithBehaviour(Vector3 location, INPCBehaviour behaviour)
    {
        var enemy = Instantiate(bigEnemy, location, Quaternion.identity);
        var enemyScript = enemy.GetComponent<BasicEnemy>();
        enemyScript.setBehaviour(behaviour);
        return enemy;
    }
}
