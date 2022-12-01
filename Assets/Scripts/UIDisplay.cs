using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIDisplay : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private Slider healthbar;
    [SerializeField] private Health playerHealth;

    [Header("Score")]
    [SerializeField] private TextMeshProUGUI scoreText;
    private ScoreKeeper scoreKeeper;

    private void Start()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        healthbar.maxValue = playerHealth.GetMaxHealth;
    }

    private void Update()
    {
        healthbar.value = playerHealth.GetHealth;
        scoreText.text = scoreKeeper.GetScore.ToString();
    }
}
