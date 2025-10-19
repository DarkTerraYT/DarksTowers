using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using DarksTowers.Displays;
using Il2CppAssets.Scripts.Models.Towers;

namespace DarksTowers.Towers.PlasmaMonkey.Upgrades.TopPath;

public class FieryPlasmaBalls : ModUpgrade<PlasmaMonkey>
{
    public override string Description => "Fiery plasma balls do more damage! The increased heat means they can be shot faster too.";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        towerModel.GetWeapon().rate *= 0.95f;
        var projectile = towerModel.GetWeapon().projectile;

        projectile.GetDamageModel().damage += 12;
        projectile.ApplyDisplay<FieryPlasmaBall>();
        
    }

    public override int Path => Top;

    public override int Tier => 4;

    public override int Cost => 4450;
}