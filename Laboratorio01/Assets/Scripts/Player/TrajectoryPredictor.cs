using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryPredictor : MonoBehaviour
{
    public Transform firePoint;

    [Header("Simulación")]
    public int steps = 30;
    public float timeStep = 0.1f;

    private LineRenderer line;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    public void ShowTrajectory(float velocity)
    {
        line.positionCount = steps;

        Vector3 startPosition = firePoint.position;
        Vector3 startVelocity = firePoint.forward * velocity;

        for (int i = 0; i < steps; i++)
        {
            float t = i * timeStep;

            // Fórmula física: posición = p0 + v*t + (1/2) g t²
            Vector3 point = startPosition +
                            startVelocity * t +
                            0.5f * Physics.gravity * t * t;

            line.SetPosition(i, point);
        }

        line.enabled = true;
    }

    public void HideTrajectory()
    {
        line.enabled = false;
    }
}