using UnityEngine;
using UnityEngine.Events;

public class LightSwitch : MonoBehaviour, IInteractable
{
    [SerializeField] private Light targetLight; // Luz que se controlará
    [SerializeField] private string promptMessage = "Interactuar con el interruptor"; // Mensaje de interacción

    // Esta función será llamada por el Raycast del jugador
    public string GetInteractionPrompt()
    {
        string estado = targetLight.enabled ? "Apagar" : "Encender";
        return $"{promptMessage} ({estado})";
    }
    public void Interact()
    {
        if (targetLight != null)
        {
            targetLight.enabled = !targetLight.enabled;
        }
    }
}