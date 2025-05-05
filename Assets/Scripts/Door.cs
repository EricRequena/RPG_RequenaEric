using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform puerta; // Referencia a la puerta (su transform)
    public Transform player; // Referencia al jugador 
    public float openAngle = -179.8f; // �ngulo cuando est� abierta (modificado)
    public float closeAngle = -109.3f; // �ngulo cuando est� cerrada (modificado)
    public float speed = 2f; // Velocidad de la rotaci�n (entre m�s alto, m�s r�pido)

    public float openDistance = 3f; // Distancia a la que el jugador abre la puerta
    private bool isOpen = false; // Si la puerta est� abierta o cerrada

    private Quaternion closedRotation; // Rotaci�n cuando est� cerrada
    private Quaternion openRotation; // Rotaci�n cuando est� abierta

    void Start()
    {
        // Guardar las rotaciones originales
        closedRotation = Quaternion.Euler(0, closeAngle, 0); // Rotaci�n cerrada con �ngulo personalizado
        openRotation = Quaternion.Euler(0, openAngle, 0); // Rotaci�n abierta con �ngulo personalizado
    }

    void Update()
    {
        // Calculamos la distancia entre la puerta y el jugador
        float distance = Vector3.Distance(puerta.position, player.position);

        // Si el jugador est� cerca, abrir la puerta; si no, cerrarla
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
