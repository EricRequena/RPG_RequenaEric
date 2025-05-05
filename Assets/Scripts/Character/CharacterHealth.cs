using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    private Animator animator;
    private bool isDeath = false;

    void Start ()
    {
        animator = GetComponent<Animator> ();
    }

    void Update ()
    {
        if (Input.GetKeyDown (KeyCode.Return) && !isDeath)
        {
            Morir ();
        }
    }

    void Morir ()
    {
        isDeath = true;
        animator.SetBool ("IsDeath", true);
    }
}
