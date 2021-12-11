using System.Collections;
using UnityEngine;

public class ParticleSysController : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    private ParticleSystem particleSystem;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    public void PlayParticles()
    {
        particleSystem.Play();
        StartCoroutine(WaitForParticlesEnd());
    }

    private IEnumerator WaitForParticlesEnd()
    {
        yield return new WaitForSeconds(2);
        particleSystem.Stop();
        levelManager.GameOver();
    }
}
