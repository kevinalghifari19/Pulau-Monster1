// Kode_P4 — State pemain yang bertahan antar-scene: monster partner & jumlah Ramuan. Ditambahkan di P4.
// P5 menambah event PartnerChanged & PotionsChanged untuk HUD; P6 menambah waktu bermain dan Reset.
using System;

public class PlayerParty
{
    public const int StartingPotions = 3;

    private int potions = StartingPotions;

    public MonsterInstance Partner { get; private set; }

    public int Potions
    {
        get => potions;
        set => potions = Math.Max(0, value);
    }

    public void SetPartner(MonsterInstance monster) => Partner = monster;
}
