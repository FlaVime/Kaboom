using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private float comboMaxTime = 5f;
    [SerializeField] private float killTimeBonus = 0.5f;
    [SerializeField] private float baseDecayRate = 1f;
    [SerializeField] private int pointsPerKill = 10;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI comboText;

    private int score = 0;
    private int combo = 1;
    private float comboTimer = 0f;

    public static ScoreManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        if (combo > 1)
        {
            float decayRate = baseDecayRate * combo;
            comboTimer -= decayRate * Time.deltaTime;

            if (comboTimer <= 0)
            {
                combo = 1;
                comboTimer = 0f;
                UpdateUI();
            }
        }
    }

    public void AddScore()
    {
        score += pointsPerKill * combo;
        combo++;

        comboTimer -= killTimeBonus;
        comboTimer = Mathf.Max(comboTimer, 1f);

        if (combo == 2)
        {
            comboTimer = comboMaxTime;
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        scoreText.text = $"Score: {score}";
        comboText.text = combo > 1 ? $"Combo x{combo}" : "";
    }

}
