using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Enemies")]
    public List<GameObject> enemyPrefabs = new List<GameObject>();

    [Header("Spawn Points")]
    public Transform topPoint;
    public Transform downPoint;
    public Transform leftPoint;
    public Transform rightPoint;

    [Header("Size Points")]
    public Vector2 topSize;
    public Vector2 downSize;
    public Vector2 leftSize;
    public Vector2 rightSize;

    [Header("Interval & Settings")]
    public float initialSpawnInterval = 2f;
    private float spawnInterval;
    public int initialMaxEnemies = 10;
    private int maxEnemies;

    private int currentEnemyCount = 0;
    private float spawnTimer = 0f;
    private float timeRemaining;

    private int enemyStage = 0;

    private void Start()
    {
        spawnInterval = initialSpawnInterval;
        maxEnemies = initialMaxEnemies;
        timeRemaining = GameManager.Instance.gameDuration;
    }

    private void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            BalanceSpawner();
            UpdateEnemyStage();
        }

        if (currentEnemyCount < maxEnemies)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnInterval)
            {
                SpawnEnemy();
                spawnTimer = 0f;
            }
        }
    }

    private void BalanceSpawner()
    {
        float progress = 1 - (timeRemaining / GameManager.Instance.gameDuration);

        spawnInterval = Mathf.Lerp(initialSpawnInterval, 0.5f, progress);
        maxEnemies = Mathf.RoundToInt(Mathf.Lerp(initialMaxEnemies, initialMaxEnemies * 2, progress));
    }

    private void UpdateEnemyStage()
    {
        float elapsedTime = GameManager.Instance.gameDuration - timeRemaining;

        if (elapsedTime <= 40f)
        {
            enemyStage = 1;
        }
        else if (elapsedTime <= 80f)
        {
            enemyStage = 2;
        }
        else
        {
            enemyStage = 3;
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = Vector3.zero;
        float randomX = 0;
        float randomY = 0;

        int zoneChoice = currentEnemyCount % 4;

        switch (zoneChoice)
        {
            case 0:
                randomX = Random.Range(topPoint.position.x - topSize.x / 2, topPoint.position.x + topSize.x / 2);
                randomY = Random.Range(topPoint.position.y - topSize.y / 2, topPoint.position.y + topSize.y / 2);
                spawnPosition = new Vector3(randomX, randomY, topPoint.position.z);
                break;
            case 1:
                randomX = Random.Range(downPoint.position.x - downSize.x / 2, downPoint.position.x + downSize.x / 2);
                randomY = Random.Range(downPoint.position.y - downSize.y / 2, downPoint.position.y + downSize.y / 2);
                spawnPosition = new Vector3(randomX, randomY, downPoint.position.z);
                break;
            case 2:
                randomX = Random.Range(leftPoint.position.x - leftSize.x / 2, leftPoint.position.x + leftSize.x / 2);
                randomY = Random.Range(leftPoint.position.y - leftSize.y / 2, leftPoint.position.y + leftSize.y / 2);
                spawnPosition = new Vector3(randomX, randomY, leftPoint.position.z);
                break;
            case 3:
                randomX = Random.Range(rightPoint.position.x - rightSize.x / 2, rightPoint.position.x + rightSize.x / 2);
                randomY = Random.Range(rightPoint.position.y - rightSize.y / 2, rightPoint.position.y + rightSize.y / 2);
                spawnPosition = new Vector3(randomX, randomY, rightPoint.position.z);
                break;
        }

        GameObject enemyToSpawn = SelectEnemyByStage();
        Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);

        currentEnemyCount++;
    }

    private GameObject SelectEnemyByStage()
    {
        switch (enemyStage)
        {
            case 1:
                return enemyPrefabs[0];
            case 2:
                return enemyPrefabs[Random.Range(0, 2)];
            case 3:
                return enemyPrefabs[Random.Range(0, 3)];
            default:
                return enemyPrefabs[0];
        }
    }

    public void OnEnemyDestroyed()
    {
        currentEnemyCount--;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(topPoint.position, topSize);
        Gizmos.DrawWireCube(downPoint.position, downSize);
        Gizmos.DrawWireCube(leftPoint.position, leftSize);
        Gizmos.DrawWireCube(rightPoint.position, rightSize);
    }
}
