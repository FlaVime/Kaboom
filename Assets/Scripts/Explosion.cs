using UnityEngine;

public class Explosion : MonoBehaviour
{

    private ParticleSystem particles;

    void Awake()
    {
        particles = GetComponent<ParticleSystem>();
    }

    public void Init(Color color)
    {
        var main = particles.main;
        main.startColor = color;
    }
}
