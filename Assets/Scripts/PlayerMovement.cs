using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private AudioSource audioSource;

    public float speed;

    private Vector3 originalScale;
    public AudioClip stepSound; // <-- atribua esse no Inspector
    private bool isWalking;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        originalScale = transform.localScale;
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb.linearVelocity = movement.normalized * speed;

        // Virar personagem
        if (moveHorizontal > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        else if (moveHorizontal < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }

        // Tocar som de passos
        if (movement.magnitude > 0.1f && !isWalking)
        {
            isWalking = true;
            audioSource.PlayOneShot(stepSound);
            Invoke("ResetStep", 0.3f); // ajusta o tempo do passo
        }
    }

    void ResetStep()
    {
        isWalking = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Coletavel")
        {
            audioSource.Play();
            GameController.Collect();
            Destroy(other.gameObject);
        }
    }
}
