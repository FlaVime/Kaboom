using TMPro;
using UnityEngine;

public class Creatures : MonoBehaviour
{
    [SerializeField] private SpriteRenderer body;
    [SerializeField] private TextMeshPro face;
    [SerializeField] private Sprite[] shapes;

    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float changeDirectionInterval = 3f;

    private string[] faces = { "^_^", ">_<", "o_O", "•_•", "T_T", "._.", "^o^", "0_0", "x_x" };
    private Color[] colors = { Color.red, Color.green, Color.blue, Color.yellow, Color.cyan, Color.magenta };

    private Vector2 moveDirection;
    private float directionChangeTimer;

    void Start()
    {
        body.sprite = shapes[Random.Range(0, shapes.Length)];
        body.color = colors[Random.Range(0, colors.Length)];
        face.text = faces[Random.Range(0, faces.Length)];
        face.color = Color.black;

        ChangeDirection();
    }

    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        directionChangeTimer -= Time.deltaTime;
        if (directionChangeTimer <= 0f)
        {
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        moveDirection = Random.insideUnitCircle.normalized;
        directionChangeTimer = changeDirectionInterval;
    }
}
