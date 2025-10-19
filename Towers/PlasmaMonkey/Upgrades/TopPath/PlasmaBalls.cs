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
        "The plasma darts are now conjured so hot it's like comparing the temperature of dirt to fire! Even more pierce and damage and melts straight through Purple Bloons' magic coating.";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var projectile = towerModel.GetWeapon().projectile;

        projectile.pierce += 5;
        projectile.GetDamageModel().damage += 2;
        projectile.GetDamageModel().immuneBloonProperties = BloonProperties.None;
        projectile.ApplyDisplay<PlasmaBall>();
    }
}