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
        animator.Play("IdleDog");
        gameObject.tag = "Dog";
    }

    void Update()
    {
        // R tuþuna basýnca karakteri deðiþtir
        if (Input.GetKeyDown(KeyCode.R))
        {
            isDog = !isDog;

            if (isDog)
            {
                animator.Play("IdleDog");
                gameObject.tag = "Dog";
            }
            else
            {
                animator.Play("IdleCat");
                gameObject.tag = "Cat";
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
                animator.Play("RunDog");
            else
                animator.Play("RunCat");
        }
        else
        {
            // Idle animasyonlarý
            if (isDog)
                animator.Play("IdleDog");
            else
                animator.Play("IdleCat");
        }
    }
}
