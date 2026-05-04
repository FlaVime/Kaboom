using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    private LineRenderer lr;
    private float currentRadius = 0f;
    private HashSet<Creatures> exploded = new HashSet<Creatures>();

    [SerializeField] private float maxRadius = 3f;
    [SerializeField] private float expandSpeed = 8f;

    private const int Segments = 36;
    private const float LineWidth = 0.05f;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        SetupLine();
    }

    void Update()
    {
        currentRadius += expandSpeed * Time.deltaTime;
        DrawWave();
        CheckCollisions();

        if (currentRadius >= maxRadius)
        {
            Destroy(gameObject);
        }
    }

    void CheckCollisions()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, currentRadius);
        foreach (var hit in hits)
        {
            Creatures creature = hit.GetComponent<Creatures>();
            if (creature != null && !exploded.Contains(creature))
            {
                exploded.Add(creature);
                creature.ExplodeFromChain();
            }
        }
    }

    void SetupLine()
    {
        lr.widthCurve = AnimationCurve.Constant(0, 1, LineWidth);
        lr.startColor = new Color(1f, 1f, 1f, 0.5f);
        lr.endColor = new Color(1f, 1f, 1f, 0.5f);
        lr.loop = true;
    }

    void DrawWave()
    {
        var points = new Vector3[Segments];
        var pos = transform.position;

        for (int i = 0; i < Segments; i++)
        {
            float angle = ((float)i / Segments) * 360 * Mathf.Deg2Rad;
            float x = Mathf.Sin(angle) * currentRadius + pos.x;
            float y = Mathf.Cos(angle) * currentRadius + pos.y;
            points[i] = new Vector3(x, y, 0);
        }

        lr.positionCount = Segments;
        lr.SetPositions(points);
    }
}
