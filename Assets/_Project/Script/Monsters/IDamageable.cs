// Kode_P4 — Apa pun yang bisa menerima damage. Ditambahkan di P4.
public interface IDamageable
{
    int CurrentHP { get; }
    void TakeDamage(int amount);
}
