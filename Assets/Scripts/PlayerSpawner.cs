using System.Collections;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private PlayerLifeManager playerLifeManager;
    [SerializeField] private PlayerMove playerMove;
    
    private Transform playerTransform;

    private void Awake()
    {
        playerTransform = playerLifeManager.transform;
        playerLifeManager.OnPlayerKilled += PlayerKilled;
    }

    private void PlayerKilled()
    {
        StartCoroutine(WaitForRespawn());
    }

    public void RespawnPlayer()
    {
        playerTransform.position = transform.position;
        playerTransform.gameObject.SetActive(true);
        playerMove.SetAgentDestination();
    }

    private void DestroySmallCubes()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var smallCube = transform.GetChild(i);
            Destroy(smallCube.gameObject);
        }
    }
    
    private IEnumerator WaitForRespawn()
    {
        yield return new WaitForSeconds(3);
        RespawnPlayer();
        DestroySmallCubes();
    }
}
