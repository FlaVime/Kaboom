using UnityEngine;

public class BoundsCreator : MonoBehaviour
{
    [SerializeField] private PhysicsMaterial2D bounceMaterial;

    void Start()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        CreateWall(new Vector2(min.x - 0.5f, 0), new Vector2(1, max.y - min.y + 2)); // Left
        CreateWall(new Vector2(max.x + 0.5f, 0), new Vector2(1, max.y - min.y + 2)); // Right
        CreateWall(new Vector2(0, min.y - 0.5f), new Vector2(max.x - min.x + 2, 1)); // Bottom
        CreateWall(new Vector2(0, max.y + 0.5f), new Vector2(max.x - min.x + 2, 1)); // Top
    }

    void CreateWall(Vector2 position, Vector2 size)
    {
        GameObject wall = new GameObject("Wall");
        wall.transform.position = position;

        BoxCollider2D collider = wall.AddComponent<BoxCollider2D>();
        collider.size = size;
        collider.sharedMaterial = bounceMaterial;
    }
}
