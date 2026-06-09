using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightController : MonoBehaviour
{
    [Header("Configuración de la Linterna")]
    [Tooltip("Tecla asignada para encender/apagar la linterna.")]

    [SerializeField] private bool isOn = false;
    private Light flashlightLight;
    

    void Start()
    {
        // Obtener el componente Light del mismo objeto
        flashlightLight = GetComponent<Light>();

        if (flashlightLight == null)
        {
            Debug.LogError("No se encontró un componente 'Light' en este objeto. Asegúrate de que este script esté en el Spotlight de la linterna.");
            return;
        }

        // Asegurarnos de que el tipo de luz sea dinámico para la interactividad exigida
        flashlightLight.renderMode = LightRenderMode.ForcePixel; 

        // Inicializar el estado físico de la luz
        flashlightLight.enabled = isOn;
    }

    void Update()
    {
        // Detectar si el jugador presiona la tecla configurada (F)
        if (Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
        {
            ToggleFlashlight();
        }
    }

    private void ToggleFlashlight()
    {
        isOn = !isOn;
        flashlightLight.enabled = isOn;
    }
}