// Kode_P5 — Installer Scn_World: menyambungkan service ke pemain, NPC, dan monster liar. Dibuat di P2, diubah di P3, P4, P5.
// Versi demo: partner mengikuti pemain. P6 menambah dialog, flag, save, musik, dan narasi pembuka.
using UnityEngine;

public class WorldInstaller : SceneInstaller
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private PlayerInteractor interactor;
    [SerializeField] private PartnerHud hud;                    // P5
    [SerializeField] private PartnerFollower follower;          // versi demo
    [SerializeField] private Vector2 respawnPoint = new Vector2(21.5f, 6f);   // di depan Batu Simpan
    [SerializeField] private MonsterData debugStarter;          // sementara, dihapus P6 (partner dari Tetua)

    private GameServices services;

    public override void Install(GameServices s)
    {
        services = s;
        player.Construct(s.Input);
        interactor.Construct(s.Input);
        hud.Construct(s.Party);
        follower.Construct(s.Party, player);

        foreach (WildMonster wild in FindInScene<WildMonster>())
            wild.Construct(s.Battle, s.Scenes, s.Party, player.transform);
        foreach (AreaZone zone in FindInScene<AreaZone>())
            zone.Entered += hud.ShowArea;                                                                 // P5
        s.Battle.Ended += OnBattleEnded;                                                                  // P5

        if (s.Party.Partner == null && debugStarter != null)
            s.Party.SetPartner(new MonsterInstance(debugStarter, 3));
    }

    void OnDestroy()
    {
        if (services != null) services.Battle.Ended -= OnBattleEnded;
    }

    private void OnBattleEnded(BattleResult result, bool wasBoss)
    {
        if (result == BattleResult.Lose)
            player.Teleport(respawnPoint, Vector2.up);          // partner sudah dipulihkan BattleSystem
    }
}
