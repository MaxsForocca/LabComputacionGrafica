using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpSystem : MonoBehaviour
{
    [Header("Configuración de Raycast")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask pickupLayer;
    [SerializeField] private LayerMask environmentLayer; // Capa del suelo/paredes para la perspectiva
    [SerializeField] private float pickupRange = 25f;       // Más rango para poder alejar objetos

    [Header("Configuración de Agarre")]
    [SerializeField] private float moveSpeed = 15f;

    private Rigidbody heldObject;
    private RigidbodyConstraints originalConstraints;
    
    // Variables para el cálculo de distancia y escala
    private float initialHitDistance;
    private Vector3 initialObjectScale;
    private bool isPerspectiveMode = false;

    void Update()
    {
        // --- AGARRE NORMAL (Clic Izquierdo) ---
        if (Mouse.current.leftButton.wasPressedThisFrame && heldObject == null)
        {
            TryPickUpObject(false);
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame && !isPerspectiveMode)
        {
            DropObject();
        }

        // --- AGARRE PERSPECTIVA (Clic Derecho) ---
        if (Mouse.current.rightButton.wasPressedThisFrame && heldObject == null)
        {
            TryPickUpObject(true);
        }
        if (Mouse.current.rightButton.wasReleasedThisFrame && isPerspectiveMode)
        {
            DropObject();
        }
    }

    void FixedUpdate()
    {
        if (heldObject != null)
        {
            if (isPerspectiveMode)
            {
                MoveObjectWithPerspective();
            }
            else
            {
                MoveHeldObjectStandard();
            }
        }
    }

    private void TryPickUpObject(bool perspective)
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange, pickupLayer))
        {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {   
                heldObject = rb;
                heldObject.useGravity = false;
                heldObject.angularDamping = 5f;
                heldObject.rotation = Quaternion.identity;

                // Guardar datos iniciales
                initialHitDistance = hit.distance;
                initialObjectScale = heldObject.transform.localScale;
                isPerspectiveMode = perspective;

                // Congelar rotación
                originalConstraints = heldObject.constraints;
                heldObject.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }
    }

    // Comportamiento normal (Clic Izquierdo)
    private void MoveHeldObjectStandard()
    {
        Vector3 targetPosition = playerCamera.transform.position + playerCamera.transform.forward * initialHitDistance;
        ApplyPhysicsMovement(targetPosition);
    }

    // Comportamiento de Perspectiva Forzada (Clic Derecho)
    private void MoveObjectWithPerspective()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        // Definimos la distancia máxima por defecto si no choca con nada en el entorno
        Vector3 targetPosition = playerCamera.transform.position + playerCamera.transform.forward * pickupRange;
        float currentDistance = pickupRange;

        // Lanzamos el segundo Raycast que busca SOLO el entorno (e ignora el objeto que cargamos gracias a las capas)
        if (Physics.Raycast(ray, out hit, pickupRange, environmentLayer))
        {
            // Restamos un pequeño margen basado en el tamaño del objeto para que no se entierre a medias en la pared
            float offset = heldObject.transform.localScale.x * 0.5f;
            currentDistance = Mathf.Max(0.5f, hit.distance - offset); 
            
            targetPosition = playerCamera.transform.position + playerCamera.transform.forward * currentDistance;
        }

        // --- EL TRUCO MAGICO DE LA ESCALA ---
        // Calculamos cuánto cambia la escala basándonos en la relación de distancias
        float scaleMultiplier = currentDistance / initialHitDistance;
        heldObject.transform.localScale = initialObjectScale * scaleMultiplier;

        // Movemos el objeto usando físicas sólidas hacia esa nueva posición
        ApplyPhysicsMovement(targetPosition);
    }

    private void ApplyPhysicsMovement(Vector3 targetPosition)
    {
        Vector3 moveDirection = targetPosition - heldObject.position;
        float distanceToTarget = moveDirection.magnitude;

        if (distanceToTarget > 0.05f)
        {
            heldObject.linearVelocity = moveDirection * moveSpeed;
        }
        else
        {
            heldObject.linearVelocity = Vector3.zero;
        }
    }

    private void DropObject()
    {
        if (heldObject != null)
        {
            heldObject.useGravity = true;
            heldObject.angularDamping = 0.05f;
            heldObject.constraints = originalConstraints;

            heldObject = null;
            isPerspectiveMode = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (playerCamera == null) return;
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (heldObject != null)
        {
            Gizmos.color = isPerspectiveMode ? Color.magenta : Color.cyan;
            Gizmos.DrawLine(ray.origin, heldObject.position);
            Gizmos.DrawWireSphere(heldObject.position, heldObject.transform.localScale.x * 0.5f);
        }
        else
        {
            Gizmos.color = new Color(1, 1, 1, 0.3f); 
            Gizmos.DrawRay(ray.origin, ray.direction * pickupRange);
        }
    }
}