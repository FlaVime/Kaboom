using TMPro;
using UnityEngine;

public class Creatures : MonoBehaviour
{
    [SerializeField] private SpriteRenderer body;
    [SerializeField] private TextMeshPro face;
    [SerializeField] private Sprite[] shapes;

    [SerializeField] private float moveSpeed = 2f;

    private static readonly string[] faces = { "^_^", ">_<", "o_O", "•_•", "T_T", "._.", "^o^", "0_0", "x_x" };
    private static readonly Color[] colors = { Color.red, Color.green, Color.blue, Color.yellow, Color.cyan, Color.magenta };
    public Color BodyColor => body.color;

    private Rigidbody2D rb;

    void Start()
    {
        // Randomly assign shape, color, and face
        body.sprite = shapes[Random.Range(0, shapes.Length)];
        body.color = colors[Random.Range(0, colors.Length)];
        face.text = faces[Random.Range(0, faces.Length)];
        face.color = Color.black;

        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Random.insideUnitCircle.normalized * moveSpeed; // random initial velocity
    }

    void OnDestroy()
    {
        CreatureSpawner.Instance?.OnCreatureDestroyed();
    }

    public void ExplodeFromChain()
    {
        ExplosionManager.Instance.SpawnExplosion(transform.position, BodyColor);
        Destroy(gameObject);
    }
}
