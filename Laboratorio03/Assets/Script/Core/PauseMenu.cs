using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseUI;

    private bool paused = false;

    void Update()
    {
        // NO permitir pause en Win
        if (GameManager.Instance.IsWin())
            return;
            
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.IsPaused())
            {
                GameManager.Instance.ResumeGame();
            }
            else
            {
                GameManager.Instance.PauseGame();
            }
        }
    }    
}