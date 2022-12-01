using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private bool isShooting = false;
    public bool IsShooting
    {
        get => isShooting;
        set => isShooting = value;
    }

    [Header("Common")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileLifetime = 5f;
    [SerializeField] private float defaultShotCooldown = 0.5f;

    [Header("AI")]
    [SerializeField] private bool isAI;
    [SerializeField] private float minShotCooldown = 0.5f;
    [SerializeField] private float maxShotCooldown = 1f;

    private Coroutine firingRoutine;

    private float timeFromLastRoutineStopped = 0f;

    private AudioPlayer audioPlayer;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void Start()
    {
        if (isAI) isShooting = true;
    }

    private void Update()
    {
        timeFromLastRoutineStopped += Time.deltaTime;
        if (isShooting && firingRoutine == null)
        {
            if (timeFromLastRoutineStopped >= defaultShotCooldown)
                firingRoutine = StartCoroutine(ContinuousFiring());
        }
        else if (!isShooting && firingRoutine != null)
        {
            StopCoroutine(firingRoutine);
            firingRoutine = null;
            timeFromLastRoutineStopped = 0f;
        }
    }

    private IEnumerator ContinuousFiring()
    {
        while (true)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            audioPlayer.PlayShootingClip();
            Rigidbody2D projectileRB = projectile.GetComponent<Rigidbody2D>();
            projectileRB.velocity = transform.up * projectileSpeed;
            Destroy(projectile, projectileLifetime);

            if (isAI) yield return new WaitForSeconds(Random.Range(minShotCooldown, maxShotCooldown));
            yield return new WaitForSeconds(defaultShotCooldown);
        }
    }
}
