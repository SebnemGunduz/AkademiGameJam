using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isDog = true; // Baþlangýçta köpek

    public float moveSpeed = 5f;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Ýlk durum
        animator.Play("IdleAdult");
        gameObject.tag = "Adult";
    }

    void Update()
    {
        // R tuþuna basýnca karakteri deðiþtir
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

        // Sprite yönü
        if (moveInput != 0)
        {
            spriteRenderer.flipX = moveInput < 0;

            // Hareket animasyonlarý
            if (isDog)
                animator.Play("RunAdult");
            else
                animator.Play("RunChild");
        }
        else
        {
            // Idle animasyonlarý
            if (isDog)
                animator.Play("IdleAdult");
            else
                animator.Play("IdleChild");
        }
    }
}
