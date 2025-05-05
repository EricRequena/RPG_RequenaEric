using UnityEngine;

public class RecogerAro : MonoBehaviour
{
    public GameObject aroActivado; // El que va en la cabeza del jugador
    public GameObject aroDesactivado; // El que está en el mapa

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Object"))
        {
            // Desactivar el aro del mapa
            aroDesactivado.SetActive(false);

            // Activar y mover el aro a la cabeza
            aroActivado.SetActive(true);
        }
    }
}
