using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject camaraTercera;
    public GameObject camaraPrimera;
    
    void Update()
    {
        // Cambio de cámara al presionar la tecla 'C'
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchCamera();
        }
    }

    public void SwitchCamera()
    {
        camaraTercera.SetActive(!camaraTercera.activeSelf);
        camaraPrimera.SetActive(!camaraPrimera.activeSelf);
    }
}