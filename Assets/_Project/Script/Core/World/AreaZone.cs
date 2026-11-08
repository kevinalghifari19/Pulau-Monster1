// Kode_P5 — Trigger area (Desa, Hutan, Danau, Gua). Dibuat di P3, diubah di P5.
using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AreaZone : MonoBehaviour
{
    [SerializeField] private string areaName = "Desa Awal";

    public string AreaName => areaName;
    public event Action<AreaZone> Entered;                      // P5: HUD berlangganan

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        Entered?.Invoke(this);
    }
}
