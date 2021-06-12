using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1 : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator runLevels()
    {
        yield return new WaitForSeconds(2.0f);
        yield return StartCoroutine(createFirstWave());
        yield return new WaitForSeconds(2.0f);
        yield return StartCoroutine(createSecondWave());
        yield return new WaitForSeconds(1.0f);
        yield return StartCoroutine(createThirdWave());
        yield return new WaitForSeconds(1.0f);
        yield return StartCoroutine(createFourthWave());


        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("MenuScene");
    }

    private IEnumerator createFirstWave()
    {
        var spawnOrder = new List<Vector3> { leftSpawn.position, centerSpawn.position, rightSpawn.position, centerSpawn.position };
        for(var x = 0; x < 6; x++)
        {
            spawnWithBehaviour(spawnOrder[x%spawnOrder.Count], new EnemySlideOverScreen());
            yield return new WaitForSeconds(1.0f);
        }
    }

    private IEnumerator createSecondWave()
    {
        var spawnOrder = new List<Vector3> { leftSpawn.position, rightSpawn.position };
        var acceleration = new List<Vector3> { new Vector3(0.8f, -0.35f, 0f), new Vector3(-0.8f, -0.35f, 0f) };
        for (var x = 0; x < 20; x++)
        {
            var behaviour = new EnemyAccelerating()
                .withSpeed(new Vector3(0f, -1.2f, 0f))
                .withAcceleration(acceleration[x % acceleration.Count]);

            spawnWithBehaviour(spawnOrder[x % spawnOrder.Count], behaviour);
            yield return new WaitForSeconds(1.0f);
        }
    }

    private IEnumerator createThirdWave()
    {
        var spawnOrder = new List<Vector3> { leftSpawn.position, centerSpawn.position, rightSpawn.position, centerSpawn.position };
        var offsetBackLeft = new Vector3(-0.8f, 0.6f, 0);
        var offsetBackRight = new Vector3(0.8f, 0.6f, 0);

        for (var x = 0; x < 12; x++)
        {
            var behaviour = new EnemyAccelerating()
                .withSpeed(new Vector3(0f, -0.5f, 0f))
                .withAcceleration(new Vector3(0f, -0.7f, 0f));

            var spawn = spawnOrder[x % spawnOrder.Count];

            spawnWithBehaviour(spawn, behaviour);
            spawnWithBehaviour(spawn + offsetBackLeft , behaviour);
            spawnWithBehaviour(spawn + offsetBackRight, behaviour);
            yield return new WaitForSeconds(2.0f);
        }
    }

    private IEnumerator createFourthWave()
    {
        var spawnOrder = new List<Vector3> { leftSpawn.position, rightSpawn.position };
        var acceleration = new List<Vector3> { new Vector3(0.9f, -0.35f, 0f), new Vector3(-0.9f, -0.35f, 0f) };
        for (var x = 0; x < 20; x++)
        {
            var behaviour = new EnemyAccelerating()
                .withSpeed(new Vector3(0f, -1.2f, 0f))
                .withAcceleration(acceleration[x % acceleration.Count]);

            spawnWithBehaviour(spawnOrder[x % spawnOrder.Count], behaviour);

            if (x % 3 == 1)
            {
                spawnBigWithBehaviour(centerSpawn.position, new EnemySlideOverScreen(new Vector3(0, -2.2f, 0)));
            }

            yield return new WaitForSeconds(1.0f);
        }
    }

    private void spawnWithBehaviour(Vector3 location, INPCBehaviour behaviour)
    {
        var enemy = Instantiate(basicEnemy, location, Quaternion.identity);
        var enemyScript = enemy.GetComponent<IEnemy>();
        enemyScript.setBehaviour(behaviour);
    }

    private void spawnBigWithBehaviour(Vector3 location, INPCBehaviour behaviour)
    {
        var enemy = Instantiate(bigEnemy, location, Quaternion.identity);
        var enemyScript = enemy.GetComponent<IEnemy>();
        enemyScript.setBehaviour(behaviour);
    }
}
