using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance;

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible   = false;
    }

    public void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;  
        Cursor.visible   = true;
    }
}