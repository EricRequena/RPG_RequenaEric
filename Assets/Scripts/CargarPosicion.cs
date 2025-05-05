using UnityEngine;
using TMPro; // Asegúrate de tener este espacio de nombres para TextMeshPro

public class GuardarCercaDeEstatua : MonoBehaviour
{
    public TextMeshProUGUI mensajeGuardar; // Cambié a TextMeshProUGUI para usar en UI
    private bool cercaDeEstatua = false;

    void Start()
    {
        // Cargar posición si existe
        if (PlayerPrefs.HasKey("PlayerX"))
        {
            float x = PlayerPrefs.GetFloat("PlayerX");
            float y = PlayerPrefs.GetFloat("PlayerY");
            float z = PlayerPrefs.GetFloat("PlayerZ");

            transform.position = new Vector3(x, y, z);
            Debug.Log("Posición cargada");
        }

        if (mensajeGuardar != null)
            mensajeGuardar.gameObject.SetActive(false); // Ocultar el texto al iniciar
    }

    void Update()
    {
        if (cercaDeEstatua && Input.GetKeyDown(KeyCode.G))
        {
            GuardarPosicion();
        }
    }

    void GuardarPosicion()
    {
        Vector3 pos = transform.position;

        PlayerPrefs.SetFloat("PlayerX", pos.x);
        PlayerPrefs.SetFloat("PlayerY", pos.y);
        PlayerPrefs.SetFloat("PlayerZ", pos.z);
        PlayerPrefs.Save();

        Debug.Log("Juego guardado cerca de la estatua.");

        if (mensajeGuardar != null)
        {
            mensajeGuardar.text = "Juego guardado.";  // Mostrar mensaje de guardado
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Chekpoint")) // Asegúrate de que la estatua tenga este tag
        {
            cercaDeEstatua = true;

            if (mensajeGuardar != null)
            {
                mensajeGuardar.text = "PRESIONA [G] PARA GUARDAR";
                mensajeGuardar.gameObject.SetActive(true); // Mostrar mensaje cuando entras al trigger
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Chekpoint"))
        {
            cercaDeEstatua = false;

            if (mensajeGuardar != null)
                mensajeGuardar.gameObject.SetActive(false); // Ocultar mensaje cuando sales del trigger
        }
    }
}
