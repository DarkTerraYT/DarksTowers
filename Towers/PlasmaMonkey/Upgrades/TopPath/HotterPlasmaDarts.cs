using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using DarksTowers.Displays;
using Il2CppAssets.Scripts.Models.Towers;

namespace DarksTowers.Towers.PlasmaMonkey.Upgrades.TopPath;

public class HotterPlasmaDarts : ModUpgrade<PlasmaMonkey>
{
    public override int Path => Top;
    public override int Tier => 1;
    public override int Cost => 550;

    public override string Description =>
        "The Plasma Monkey now conjures the plasma darts even hotter than before! This increases the pierce and damage of the darts.";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var projectile = towerModel.GetWeapon().projectile;

        projectile.pierce += 2;
        projectile.GetDamageModel().damage += 1;

        projectile.ApplyDisplay<HotPlasmaDart>();
    }
}