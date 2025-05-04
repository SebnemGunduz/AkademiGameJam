using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isDog = true; // Ba�lang��ta k�pek

    public float moveSpeed = 5f;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // �lk durum
        animator.Play("IdleAdult");
        gameObject.tag = "Adult";
    }

    void Update()
    {
        // R tu�una bas�nca karakteri de�i�tir
        if (Input.GetKeyDown(KeyCode.R))
        {
            isDog = !isDog;

            if (isDog)
            {
                animator.Play("IdleAdult");
                gameObject.tag = "Adult";
            }
            else
            {
                animator.Play("IdleChild");
                gameObject.tag = "Child";
            }
        }

        // Hareket girdisi
        float moveInput = Input.GetAxisRaw("Horizontal");
        Vector3 movement = new Vector3(moveInput, 0f, 0f);
        transform.position += movement * moveSpeed * Time.deltaTime;

        // Sprite y�n�
        if (moveInput != 0)
        {
            spriteRenderer.flipX = moveInput < 0;

            // Hareket animasyonlar�
            if (isDog)
                animator.Play("RunAdult");
            else
                animator.Play("RunChild");
        }
        else
        {
            // Idle animasyonlar�
            if (isDog)
                animator.Play("IdleAdult");
            else
                animator.Play("IdleChild");
        }
    }
}
