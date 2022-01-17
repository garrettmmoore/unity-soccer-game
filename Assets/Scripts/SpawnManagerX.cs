using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;

    private const float SpawnRangeX = 10;
    private const float SpawnZMin = 15; // set min spawn Z
    private const float SpawnZMax = 25; // set max spawn Z

    public int enemyCount;
    public int waveCount = 1;

    public GameObject player; 

    // Update is called once per frame
    private void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemyCount == 0)
        {
            waveCount++;
            SpawnEnemyWave(waveCount);
        }

    }

    // Generate random spawn position for powerUps and enemy balls
    private static Vector3 GenerateSpawnPosition ()
    {
        var xPos = Random.Range(-SpawnRangeX, SpawnRangeX);
        var zPos = Random.Range(SpawnZMin, SpawnZMax);
        return new Vector3(xPos, 0, zPos);
    }


    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        // Make powerUps spawn at player end
        var powerUpSpawnOffset = new Vector3(0, 0, -15);

        // If no powerUps remain, spawn a powerUp
        if (GameObject.FindGameObjectsWithTag("PowerUp").Length == 0)
        {
            Instantiate(powerUpPrefab, GenerateSpawnPosition() + powerUpSpawnOffset, powerUpPrefab.transform.rotation);
        }

        // Spawn number of enemy balls based on wave number
        for (var i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }

        // Put player back at start
        ResetPlayerPosition();

    }

    // Move player back to position in front of own goal
    private void ResetPlayerPosition ()
    {
        player.transform.position = new Vector3(0, 1, -7);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

    }

}
