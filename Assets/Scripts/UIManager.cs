using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button exitButton;

    private CanvasGroup pauseCanvasGroup;
    private bool isPause;
    private float canvasAlpha;

    private void Awake()
    {
        pauseCanvasGroup = pausePanel.GetComponent<CanvasGroup>();
        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);
        exitButton.onClick.AddListener(ExitGame);
    }

    private void Update()
    {
       if (isPause && canvasAlpha < 1)
           PausePanelSetVisible();
    }

    private void PauseGame()
    {
        SetPause(!isPause);
    }
    
    private void SetPause(bool value)
    {
        pausePanel.SetActive(value);
        isPause = value;
        Time.timeScale = isPause? 0 : 1;
    }

    private void PausePanelSetVisible()
    {
        canvasAlpha += Time.unscaledDeltaTime;
        pauseCanvasGroup.alpha = canvasAlpha;
    }

    private void ResumeGame()
    {
        SetPause(!isPause);
        pauseCanvasGroup.alpha = canvasAlpha = 0;
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
