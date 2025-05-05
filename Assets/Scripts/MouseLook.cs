using UnityEngine;
using Cinemachine;

public class CameraLookAtCursor : MonoBehaviour
{
    public CinemachineVirtualCamera[] virtualCameras;
    private CinemachineVirtualCamera activeCamera;
    public Transform cursorTarget;        // El objeto que la cámara mirará
    public LayerMask raycastLayer;        // Capa del suelo u objetivo para raycast

    void Start()
    {
        activeCamera = virtualCameras[0];
        SetActiveCamera(0);
    }

    void Update()
    {
        // Cambio de cámara con teclas
        if (Input.GetKeyDown(KeyCode.Alpha1)) SetActiveCamera(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SetActiveCamera(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SetActiveCamera(2);

        UpdateCursorTargetPosition();
    }

    void SetActiveCamera(int index)
    {
        if (activeCamera != null) activeCamera.gameObject.SetActive(false);
        activeCamera = virtualCameras[index];
        activeCamera.gameObject.SetActive(true);
    }

    void UpdateCursorTargetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, raycastLayer))
        {
            cursorTarget.position = hit.point;
        }
    }
}
