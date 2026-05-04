using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int value = 1;
    public float rotationSpeed = 50f;

    void Start()
    {
        // Registramos que existe un coleccionable más al iniciar
        UIManager.Instance.RegistryCollectible();
    }

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // BUG FIX: Verificamos el tag en el collider directo Y en el padre,
        // por si el WheelCollider o un hijo del carro entra al trigger.
        bool isPlayer = other.CompareTag("Player") ||
                        (other.GetComponentInParent<CarController>() != null);

        if (isPlayer)
        {
            UIManager.Instance.AddCollectible(value);
            Destroy(gameObject);
        }
    }
}
