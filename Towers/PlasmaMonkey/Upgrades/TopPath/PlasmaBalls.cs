using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using DarksTowers.Displays;
using Il2Cpp;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Filters;

namespace DarksTowers.Towers.PlasmaMonkey.Upgrades.TopPath;

public class PlasmaBalls : ModUpgrade<PlasmaMonkey>
{
    public override int Path => Top;
    public override int Tier => 3;
    public override int Cost => 725;

    public override string Description =>
        "The plasma darts condense down into plasma balls. You probably already know the deal already, more damage, more pierce yada yada... Plasma balls can also damage purples now.";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var projectile = towerModel.GetWeapon().projectile;

        projectile.pierce += 5;
        projectile.GetDamageModel().damage += 2;
        projectile.GetDamageModel().immuneBloonProperties = BloonProperties.None;
        projectile.ApplyDisplay<PlasmaBall>();
    }
}