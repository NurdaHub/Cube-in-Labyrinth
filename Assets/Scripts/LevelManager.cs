using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private PlayerSpawner playerSpawner;
    [SerializeField] private MazeSpawner mazeSpawner;
    [SerializeField] private Image fadingImage;
    [SerializeField] private GameObject player;
    
    private float fadeDuration = 1f;

    private void Start()
    {
        GenerateNewLevel();
    }

    private void GenerateNewLevel()
    {
        player.SetActive(false);
        mazeSpawner.InitMaze();
        playerSpawner.RespawnPlayer();
    }

    private void ScreenFade()
    {
        fadingImage.gameObject.SetActive(true);
        fadingImage.DOFade(1, fadeDuration).OnComplete(delegate
        {
            fadingImage.DOFade(0, fadeDuration).OnComplete(delegate
            {
                fadingImage.gameObject.SetActive(false);
            });
            
            GenerateNewLevel();
        });
    }

    public void GameOver()
    {
        ScreenFade();
    }
}
