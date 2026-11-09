using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerConfig config;
    [SerializeField] private Rigidbody2D rb;
    private IMoveInput input;

    public Vector2 Velocity { get; private set; }
    public Vector2 Facing { get; private set; } = Vector2.down;

    public void Construct(IMoveInput moveInput) => input = moveInput;

    void Update()
    {
        if (input == null) return;

        Vector2 move = input.Move;
        float speed = input.IsRunning ? config.RunSpeed : config.WalkSpeed;
        Velocity = move * speed;

        if (move.sqrMagnitude > 0.01f)
        {
            Facing = ToCardinal(move);
        }

        transform.Translate(Velocity * Time.deltaTime);
    }

    // Teleport dengan 1 parameter posisi
    public void Teleport(Vector2 position)
    {
        transform.position = position;
        if (rb != null)
        {
            rb.position = position;
        }
    }

    // Teleport dengan 2 parameter (Posisi dan Arah Hadap)
    public void Teleport(Vector2 position, Vector2 facing)
    {
        Teleport(position);
        if (facing.sqrMagnitude > 0.01f)
        {
            Facing = ToCardinal(facing);
        }
    }

    // Teleport dengan 2 parameter (Koordinat Float X dan Y)
    public void Teleport(float x, float y)
    {
        Teleport(new Vector2(x, y));
    }

    private static Vector2 ToCardinal(Vector2 v)
    {
        return Mathf.Abs(v.x) > Mathf.Abs(v.y)
            ? new Vector2(Mathf.Sign(v.x), 0f)
            : new Vector2(0f, Mathf.Sign(v.y));
    }
}