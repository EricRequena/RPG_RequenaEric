using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class ActivarContenidoAlPulsarZ : MonoBehaviour
{
    public GameObject[] estatuas;     // Las estatuas a activar/desactivar
    public VideoPlayer video;         // Video a reproducir/parar
    public Image imagenUI;            // Imagen UI a mostrar/ocultar

    private bool estaActivo = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            estaActivo = !estaActivo;

            // Activar o desactivar estatuas
            foreach (GameObject estatua in estatuas)
            {
                if (estatua != null)
                    estatua.SetActive(estaActivo);
            }

            // Activar o desactivar video
            if (video != null)
            {
                video.gameObject.SetActive(estaActivo);

                if (estaActivo)
                    video.Play();
                else
                    video.Stop();
            }

            // Activar o desactivar imagen UI
            if (imagenUI != null)
                imagenUI.gameObject.SetActive(estaActivo);

            Debug.Log("Contenido " + (estaActivo ? "activado" : "desactivado") + " al pulsar Z");
        }
    }
}
