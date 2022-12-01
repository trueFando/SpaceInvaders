using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private bool isPlayer;
    [SerializeField] private bool useCameraShake = false;
    [SerializeField] private ParticleSystem takeDamageEffect;
    [SerializeField] private int maxHealth = 100;
    private int scoreGiven;
    private int health;
    public int GetHealth => health;
    public int GetMaxHealth => maxHealth;

    private AudioPlayer audioPlayer;
    private ScoreKeeper scoreKeeper;
    private LevelManager levelManager;
    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void Start()
    {
        health = maxHealth;
        scoreGiven = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer dd = collision.GetComponent<DamageDealer>();
        if (dd != null)
        {
            TakeDamage(dd.Damage);
            dd.Hit();
        }
    }


    private void TakeDamage(int damage)
    {
        PlayDamageEffect();
        audioPlayer.PlayDamageClip();
        if (useCameraShake) ShakeCamera();
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (!isPlayer) scoreKeeper.ChangeScore(scoreGiven);
        if (isPlayer) levelManager.LoadGameOver();
        Destroy(gameObject);
    }

    private void PlayDamageEffect()
    {
        takeDamageEffect.Play();
    }

    private void ShakeCamera()
    {
        Camera.main.GetComponent<CameraShake>().PlayShakeEffect();
    }
}
