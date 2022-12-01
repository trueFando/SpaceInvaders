using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private WaveConfigSO currentWave;
    [SerializeField] private List<WaveConfigSO> waves;
    [SerializeField] private float newWaveCooldown = 0f;
    public WaveConfigSO CurrentWave => currentWave;

    [SerializeField] private bool isLooping;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        do
        {
            foreach (WaveConfigSO wave in waves)
            {
                currentWave = wave;
                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    Instantiate(currentWave.GetEnemyPrefab(i),
                                currentWave.GetStartPoint().position,
                                Quaternion.Euler(0, 0, 180f),
                                transform);

                    yield return new WaitForSeconds(currentWave.GetRandomCooldown());
                }
                yield return new WaitForSeconds(newWaveCooldown);
            }
        }
        while (isLooping);
        
    }
}
