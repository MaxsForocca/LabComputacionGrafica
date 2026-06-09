using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class PlayerRaycastInteract : MonoBehaviour
{
    [Header("Configuración de Raycast")]
    [SerializeField] private float interactDistance = 4f; // Distancia máxima de alcance
    [SerializeField] private LayerMask interactableLayer;   // Capa para optimizar el Raycast
    [Header("UI de Interaccion")]
    [SerializeField] private GameObject interactUI; // UI para mostrar el mensaje de interacción
    [SerializeField] private TextMeshProUGUI interactText; // Texto dentro de la UI para mostrar el mensaje

    private Camera mainCamera;
    private IInteractable currentInteractable;

    void Start()
    {
        mainCamera = Camera.main;
        if (interactUI != null)
            interactUI.SetActive(false); // Asegurarnos de que la UI esté oculta al inicio
    }

    void Update()
    {
        CheckForInteractable();
        // Detectar Clic Izquierdo del mouse (0)
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            if(currentInteractable != null)
                currentInteractable.Interact();
        }
    }

    void CheckForInteractable()
    {
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        // Lanzar el Raycast físico
        if (Physics.Raycast(ray, out hit, interactDistance, interactableLayer))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                currentInteractable = interactable;
                ShowUI(interactable.GetInteractionPrompt());
                return;
            }
        }
        ClearInteractable();
    }
    void ShowUI(string promptText)
    {
        if (interactUI != null && interactText != null)
        {
            interactText.text = promptText;
            interactUI.SetActive(true);
        }
    }

    void ClearInteractable()
    {
        currentInteractable = null;
        if (interactUI != null)
            interactUI.SetActive(false);
    }

    // Dibujar el rayo en el editor para pruebas visuales
    void OnDrawGizmos()
    {
        Camera cam = Camera.main;
        if (cam != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(cam.transform.position, cam.transform.forward * interactDistance);
        }
    }
}