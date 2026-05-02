using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private float comboMaxTime = 10f;
    [SerializeField] private float killTimeBonus = 0.5f;
    [SerializeField] private float baseDecayRate = 0.15f;
    [SerializeField] private int pointsPerKill = 10;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private Image comboBarBG;
    [SerializeField] private Image comboSlider;

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
            }
        }
        UpdateUI();
    }

    public void AddScore()
    {
        score += pointsPerKill * combo;
        combo++;

        if (combo == 2)
            comboTimer = comboMaxTime;
        else
            comboTimer += killTimeBonus;

        comboTimer = Mathf.Min(comboTimer, comboMaxTime);

        UpdateUI();
    }

    void UpdateUI()
    {
        scoreText.text = $"Score: {score}";
        bool hasCombo = combo > 1;
        comboBarBG.gameObject.SetActive(hasCombo);
        comboSlider.fillAmount = hasCombo ? comboTimer / comboMaxTime : 0f;
        comboText.text = hasCombo ? $"Combo x{combo}" : "";
    }

}
