using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave Config", menuName = "Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform path;

    [SerializeField] private float defaultSpawnCooldown = 1f;
    [SerializeField] private float cooldownDelta = 3f;
    [SerializeField] private float minCooldown = 0.4f;

    public int GetEnemyCount()
    {
        return enemyPrefabs.Count;
    }

    public GameObject GetEnemyPrefab(int index)
    {
        return enemyPrefabs[index];
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public Transform GetStartPoint()
    {
        return path.GetChild(0);
    }

    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();

        foreach(Transform child in path)
        {
            waypoints.Add(child);
        }

        return waypoints;
    }


    public float GetRandomCooldown()
    {
        float cooldown = Random.Range(defaultSpawnCooldown - cooldownDelta,
                                      defaultSpawnCooldown + cooldownDelta);
        if (cooldown < minCooldown) cooldown = minCooldown;

        return cooldown;
    }

}
