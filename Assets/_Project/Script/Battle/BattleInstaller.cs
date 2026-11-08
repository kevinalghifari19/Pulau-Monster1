// Kode_P5 — Installer Scn_Battle: menyiapkan battle dari BattleSession. Dibuat di P4, diubah di P5.
using UnityEngine;

public class BattleInstaller : SceneInstaller
{
    [SerializeField] private BattleSystem battle;
    [SerializeField] private BattleView view;
    [SerializeField] private SpriteRenderer background;    // versi demo
    [SerializeField] private Sprite forestBackground;
    [SerializeField] private Sprite caveBackground;

    public override void Install(GameServices s)
    {
        background.sprite = s.Battle.IsBoss ? caveBackground : forestBackground;
        battle.Construct(s.Party, s.Battle, s.Scenes);
        new BattlePresenter(battle, view);
        battle.Begin();
    }
}
