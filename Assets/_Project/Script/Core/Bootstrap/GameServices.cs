// Kode_P4 — Wadah semua service yang dibuat di Composition Root (GameBootstrap). Ditambahkan di P4.
// Hanya installer yang menerima wadah ini; component lain menerima service satu per satu lewat Construct.
public class GameServices
{
    public InputGate Input;              // IMoveInput + IActionInput yang bisa dikunci
    public MonsterDatabase Database;
    public PlayerParty Party;
    public BattleSession Battle;
    public SceneFlow Scenes;
}
