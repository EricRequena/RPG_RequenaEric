using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public Transform target;                  // El objeto que la c�mara sigue (por ejemplo, el jugador)
    public float distance = 5.0f;             // Distancia deseada desde el objetivo
    public float smoothSpeed = 10.0f;         // Suavizado del movimiento de c�mara
    public LayerMask collisionLayers;         // Capas con las que la c�mara debe colisionar

    private Vector3 currentVelocity;

    void LateUpdate()
    {
        // Direcci�n desde el objetivo hacia atr�s (posici�n deseada de la c�mara)
        Vector3 desiredCameraPos = target.position - target.forward * distance;
        RaycastHit hit;

        // Raycast desde el objetivo hacia la posici�n deseada
        if (Physics.Raycast(target.position, -target.forward, out hit, distance, collisionLayers))
        {
            // Colisi�n detectada: mueve la c�mara al punto de impacto, ligeramente hacia adelante
            Vector3 hitPos = hit.point + hit.normal * 0.2f;
            transform.position = Vector3.SmoothDamp(transform.position, hitPos, ref currentVelocity, Time.deltaTime * smoothSpeed);
        }
        else
        {
            // Sin colisi�n: mueve la c�mara a la posici�n deseada suavemente
            transform.position = Vector3.SmoothDamp(transform.position, desiredCameraPos, ref currentVelocity, Time.deltaTime * smoothSpeed);
        }

        // Siempre mirar al objetivo
        transform.LookAt(target);
    }
}
