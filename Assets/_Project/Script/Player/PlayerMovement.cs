using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Kecepatan")]
    [SerializeField] private float walkSpeed = 3.75f;
    [SerializeField] private float runSpeed = 6f;

    [Header("Komponen")]
    [SerializeField] private Rigidbody2D rb;

    private Vector2 velocity;

    void Update()
    {
        // Cara lama yang Anda suka: WASD / panah / gamepad lewat axis bawaan
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 move = new Vector2(x, y);

        // Sprint: tahan Shift
        bool isRunning = Input.GetKey(KeyCode.LeftShift)
                      || Input.GetKey(KeyCode.RightShift);

        float speed = isRunning ? runSpeed : walkSpeed;
        velocity = move * speed;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = velocity;
    }
}