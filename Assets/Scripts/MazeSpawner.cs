using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] private NavMeshBaker navMeshBaker;

    public List<GameObject> wallsList;
    private int maxWallCount = 30;
    
    public void InitMaze()
    {
        DisableAllWalls();
        ActivateRandomWalls();
        navMeshBaker.BakeNavMesh();
    }

    private void ActivateRandomWalls()
    {
        for(int i = 0; i < maxWallCount; i++)
        {
            int random = Random.Range(0, wallsList.Count);
            
            if(!wallsList[random].activeSelf) 
                wallsList[random].SetActive(true);
            else i--;
        }
    }

    private void DisableAllWalls()
    {
        if (wallsList == null || wallsList.Count <= 0)
            return;
        
        foreach (var wall in wallsList)
        {
            if (wall.activeInHierarchy)
                wall.SetActive(false);
        }
    }
}