// Kode_P5 — State pemain yang bertahan antar-scene: monster partner & jumlah Ramuan. Dibuat di P4, diubah di P5.
// P5: event PartnerChanged & PotionsChanged untuk HUD ("Ramuan ×N"). P6 menambah waktu bermain dan Reset.
using System;

public class PlayerParty
{
    public const int StartingPotions = 3;

    private int potions = StartingPotions;

    public MonsterInstance Partner { get; private set; }

    public int Potions
    {
        get => potions;
        set { potions = Math.Max(0, value); PotionsChanged?.Invoke(potions); }
    }

    public event Action<MonsterInstance> PartnerChanged;       // P5: HUD
    public event Action<int> PotionsChanged;                   // versi demo: HUD

    public void SetPartner(MonsterInstance monster)
    {
        Partner = monster;
        PartnerChanged?.Invoke(monster);
    }
}
