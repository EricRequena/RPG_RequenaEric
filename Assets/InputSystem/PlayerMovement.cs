using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementInput : MonoBehaviour, PlayerInput.IMovementActions
{
    public float speed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    public GameObject arma;                  // Objeto del arma
    public GameObject projectilePrefab;      // Prefab del proyectil
    public Transform spawnPoint;             // Punto desde donde se dispara
    public float shotForce = 1500f;
    public float shotRate = 0.5f;

    private float shotRateTime = 0f;

    private Animator animator;
    private bool isMovement = false;

    private CharacterController controller;
    private PlayerInput pController;
    private Vector2 inputMovement;
    private Vector3 velocity;

    private Coroutine danceCoroutine;
    private bool isDancing = false;
    private bool isAiming = false;

    private Camera mainCamera;

    void Awake()
    {
        pController = new PlayerInput();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;
        pController.Movement.SetCallbacks(this);
    }

    private void OnEnable() => pController.Enable();
    private void OnDisable() => pController.Disable();

    void Update()
    {
        if (isDancing) return;

        // Movimiento y gravedad
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            animator.SetBool("IsJumping", false);
        }

        Vector3 move = transform.right * inputMovement.x + transform.forward * inputMovement.y;
        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        animator.SetBool("IsWalking", isMovement);

        // Mostrar arma solo si estás apuntando
        if (arma != null)
        {
            arma.SetActive(isAiming);
        }

        // Disparo
        if (isAiming && Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (Time.time > shotRateTime)
            {
                GameObject newBullet = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
                Rigidbody rb = newBullet.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(spawnPoint.forward * shotForce);
                }

                shotRateTime = Time.time + shotRate;

                Destroy(newBullet, 2f);
            }
        }
    }

    public void OnWASD(InputAction.CallbackContext context)
    {
        inputMovement = context.ReadValue<Vector2>();

        if (context.performed)
        {
            CancelDance();
            isMovement = true;
        }
        else if (context.canceled)
        {
            isMovement = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && controller.isGrounded)
        {
            CancelDance();
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetBool("IsJumping", true);
        }
    }

    public void OnDancing(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!isDancing)
            {
                isDancing = true;
                animator.SetBool("IsDance", true);
                danceCoroutine = StartCoroutine(StopDanceAfterSeconds(5f));
            }
        }
    }

    public void OnAiming(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();

        if (value > 0.5f)
        {
            if (!isAiming)
            {
                isAiming = true;
                animator.SetBool("IsAim", true);
            }
        }
        else
        {
            if (isAiming)
            {
                isAiming = false;
                animator.SetBool("IsAim", false);
            }
        }
    }

    private IEnumerator StopDanceAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        animator.SetBool("IsDance", false);
        isDancing = false;
    }

    private void CancelDance()
    {
        if (isDancing)
        {
            animator.SetBool("IsDance", false);
            if (danceCoroutine != null) StopCoroutine(danceCoroutine);
            isDancing = false;
        }
    }
}
