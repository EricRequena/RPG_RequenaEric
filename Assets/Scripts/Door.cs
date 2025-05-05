using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform puerta; // Referencia a la puerta (su transform)
    public Transform player; // Referencia al jugador 
    public float openAngle = -179.8f; // Ángulo cuando está abierta (modificado)
    public float closeAngle = -109.3f; // Ángulo cuando está cerrada (modificado)
    public float speed = 2f; // Velocidad de la rotación (entre más alto, más rápido)

    public float openDistance = 3f; // Distancia a la que el jugador abre la puerta
    private bool isOpen = false; // Si la puerta está abierta o cerrada

    private Quaternion closedRotation; // Rotación cuando está cerrada
    private Quaternion openRotation; // Rotación cuando está abierta

    void Start()
    {
        // Guardar las rotaciones originales
        closedRotation = Quaternion.Euler(0, closeAngle, 0); // Rotación cerrada con ángulo personalizado
        openRotation = Quaternion.Euler(0, openAngle, 0); // Rotación abierta con ángulo personalizado
    }

    void Update()
    {
        // Calculamos la distancia entre la puerta y el jugador
        float distance = Vector3.Distance(puerta.position, player.position);

        // Si el jugador está cerca, abrir la puerta; si no, cerrarla
        if (distance < openDistance && !isOpen)
        {
            isOpen = true;
        }
        else if (distance >= openDistance && isOpen)
        {
            isOpen = false;
        }

        // La puerta se mueve suavemente entre abierta y cerrada usando Slerp
        Quaternion targetRotation = isOpen ? openRotation : closedRotation;
        puerta.rotation = Quaternion.Slerp(puerta.rotation, targetRotation, Time.deltaTime * speed);
    }
}
