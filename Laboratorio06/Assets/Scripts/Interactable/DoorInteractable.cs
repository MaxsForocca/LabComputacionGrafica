using NUnit.Framework;
using UnityEngine;
public class DoorInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string promptMessage = "Abrir/Cerrar Puerta";
    public bool isOpen = false;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public string GetInteractionPrompt()
    {
        return isOpen ? $"Cerrar Puerta" : $"Abrir Puerta";
    }

    // Método que se llamará cuando el jugador interactúe con la puerta
    public void Interact()
    {
        isOpen = !isOpen; // Alternar el estado de la puerta
        if (animator != null)
        {
            animator.SetBool("isOpen", isOpen); // Actualizar el estado del Animator
        }
        else
        {
            Debug.Log(isOpen ? "La puerta se ha abierto." : "La puerta se ha cerrado.");
        }
    }
}