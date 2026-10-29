// Kode_P4 — Installer Scn_Battle: menyiapkan battle dari BattleSession. Ditambahkan di P4.
// Kode_P4: versi sementara (slide p5-battle). P5 mengganti seluruh isinya dengan BattleSystem + BattleView.
using System.Collections;
using UnityEngine;

public class BattleInstaller : SceneInstaller
{
    [SerializeField] private SpriteRenderer enemySprite;   // EnemyMon di Scn_Battle

    public override void Install(GameServices s)
    {
        MonsterInstance enemy = s.Battle.Enemy;
        enemySprite.sprite = enemy.Data.Frames[0];          // bukti data ikut pindah scene
        Debug.Log($"Battle melawan {enemy.Data.DisplayName} Lv {enemy.Level}" + (s.Battle.IsBoss ? " (boss)" : ""));
        StartCoroutine(AutoWin(s.Scenes));
    }

    // Kode_P4: menang otomatis setelah 2 detik agar alur dunia → battle → dunia bisa diuji.
    private IEnumerator AutoWin(SceneFlow scenes)
    {
        yield return new WaitForSeconds(2f);
        scenes.EndBattle(BattleResult.Win);
    }
}
