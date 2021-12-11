using System;
using UnityEngine;

public class PlayerLifeManager : MonoBehaviour
{
    [SerializeField] private ShieldButtonControl shieldButtonControl;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material shieldMaterial;

    public CubeExplode cubeExplode;
    private Renderer playerRenderer;
    private string killAreaTag = "KillArea";

    public Action OnPlayerKilled;
    private void Awake()
    {
        playerRenderer = GetComponent<Renderer>();
        shieldButtonControl.OnShieldStateChanged += ChangeMaterial;
    }

    private void ChangeMaterial(bool shieldActive)
    {
        playerRenderer.material = shieldActive ? shieldMaterial : defaultMaterial;
    }

    private void KillPlayer()
    {
        cubeExplode.Explode(transform);
        gameObject.SetActive(false);
        OnPlayerKilled?.Invoke();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(killAreaTag) && !shieldButtonControl.isShieldActive)
            KillPlayer();
    }
}
