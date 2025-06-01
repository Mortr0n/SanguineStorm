using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] int poolSize = 20;
    [SerializeField] List<WaveData> waves = new();

    Dictionary<GameObject, List<GameObject>> enemyPools = new();
    List<ActiveWave> activeWaves = new();
    int nextWaveIndex = 0;
    float globalWaveTimer = 0;

    public static SpawnManager instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        ActivateNextWave();
    }

    void Update()
    {
        globalWaveTimer += Time.deltaTime;

        // Activate next wave based on cumulative wave durations
        if (nextWaveIndex < waves.Count && globalWaveTimer >= waves[nextWaveIndex].waveStartTime)
            ActivateNextWave();

        for (int i = activeWaves.Count - 1; i >= 0; i--)
        {
            var wave = activeWaves[i];
            wave.spawnTimer += Time.deltaTime;

            if (wave.spawnTimer >= wave.data.waveSpawnTime)
            {
                wave.spawnTimer = 0;
                SpawnEnemy(wave.data.waveEnemy);
            }

            wave.waveTimer += Time.deltaTime;

            if (wave.waveTimer >= wave.data.waveDuration)
            {
                CleanupPoolIfUnused(wave.data.waveEnemy);
                activeWaves.RemoveAt(i);
            }
        }
    }

    void CleanupPoolIfUnused(GameObject prefab)
    {
        // Only destroy the pool if no future waves will use this prefab
        for (int i = nextWaveIndex; i < waves.Count; i++)
        {
            if (waves[i].waveEnemy == prefab)
                return;
        }

        if (enemyPools.TryGetValue(prefab, out var pool))
        {
            foreach (var enemy in pool)
            {
                Destroy(enemy);
            }
            enemyPools.Remove(prefab);
            Debug.Log($"Destroyed pool for {prefab.name}");
        }
    }

    void ActivateNextWave()
    {
        var waveData = waves[nextWaveIndex];
        EnsurePoolExists(waveData.waveEnemy);
        activeWaves.Add(new ActiveWave(waveData));
        nextWaveIndex++;
    }

    void SpawnEnemy(GameObject prefab)
    {
        GameObject enemy = GetEnemyFromPool(prefab);
        if (enemy != null)
        {
            float angle = Random.Range(0f, Mathf.PI * 2);
            Vector3 position = VS_PlayerController.instance.transform.position +
                               new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * 10f;
            enemy.transform.position = position;
            enemy.SetActive(true);
        }
    }

    void EnsurePoolExists(GameObject prefab)
    {
        if (!enemyPools.ContainsKey(prefab))
        {
            enemyPools[prefab] = new List<GameObject>();
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                enemyPools[prefab].Add(obj);
            }
        }
    }

    GameObject GetEnemyFromPool(GameObject prefab)
    {
        foreach (var enemy in enemyPools[prefab])
        {
            if (!enemy.activeInHierarchy)
                return enemy;
        }
        return null;
    }

    class ActiveWave
    {
        public WaveData data;
        public float spawnTimer = 0;
        public float waveTimer = 0;

        public ActiveWave(WaveData waveData)
        {
            data = waveData;
        }
    }
}

[System.Serializable]
public class WaveData
{
    public GameObject waveEnemy;
    public float waveSpawnTime = 1;
    public float waveDuration = 10;
    public float waveStartTime = 0; // New: time after game starts to begin this wave
}
