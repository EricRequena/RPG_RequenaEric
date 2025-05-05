using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public Transform target;                  // El objeto que la cámara sigue (por ejemplo, el jugador)
    public float distance = 5.0f;             // Distancia deseada desde el objetivo
    public float smoothSpeed = 10.0f;         // Suavizado del movimiento de cámara
    public LayerMask collisionLayers;         // Capas con las que la cámara debe colisionar

    private Vector3 currentVelocity;

    void LateUpdate()
    {
        // Dirección desde el objetivo hacia atrás (posición deseada de la cámara)
        Vector3 desiredCameraPos = target.position - target.forward * distance;
        RaycastHit hit;

        // Raycast desde el objetivo hacia la posición deseada
        if (Physics.Raycast(target.position, -target.forward, out hit, distance, collisionLayers))
        {
            // Colisión detectada: mueve la cámara al punto de impacto, ligeramente hacia adelante
            Vector3 hitPos = hit.point + hit.normal * 0.2f;
            transform.position = Vector3.SmoothDamp(transform.position, hitPos, ref currentVelocity, Time.deltaTime * smoothSpeed);
        }
        else
        {
            // Sin colisión: mueve la cámara a la posición deseada suavemente
            transform.position = Vector3.SmoothDamp(transform.position, desiredCameraPos, ref currentVelocity, Time.deltaTime * smoothSpeed);
        }

        // Siempre mirar al objetivo
        transform.LookAt(target);
    }
}
