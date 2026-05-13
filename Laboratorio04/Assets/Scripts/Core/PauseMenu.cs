using UnityEngine;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour {
    public GameObject pauseUI;

    void Update()
    {
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