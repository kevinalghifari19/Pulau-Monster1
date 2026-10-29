using UnityEngine;

public class WorldInstaller : SceneInstaller
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private PlayerInteractor interactor;
    [SerializeField] private PartnerFollower follower;
    [SerializeField] private MonsterData debugStarter; // sementara, dihapus P6

    public override void Install(GameServices s)
    {
        player.Construct(s.Input);
        interactor.Construct(s.Input);
        follower.Construct(s.Party, player);

        foreach (WildMonster wild in FindObjectsByType<WildMonster>(FindObjectsSortMode.None))
            wild.Construct(s.Battle, s.Scenes, s.Party, player.transform);

        if (s.Party.Partner == null && debugStarter != null)
            s.Party.SetPartner(new MonsterInstance(debugStarter, 3));
    }
}