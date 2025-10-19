using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using DarksTowers.Displays;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Filters;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;

namespace DarksTowers.Towers.PlasmaMonkey.Upgrades.BottomPath;

public class PlasmaBeams : ModUpgrade<PlasmaMonkey>
{
    public override string Description =>
        "The plasma monkey refines the plasma darts into plasma beams! These plasma beams travel slower, but last longer, and have much higher pierce and damage. These plasma beams also can damage camo bloons.";

    public override int Path => Bottom;
    public override int Tier => 3;
    public override int Cost => 2240;

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var weaponModel = towerModel.GetWeapon();
        
        var projectile = weaponModel.projectile;
        
        projectile.pierce *= 2.5f;
        projectile.GetDamageModel().damage += 2;
        projectile.ApplyDisplay<PlasmaBeam>();
        projectile.radius /= 2.5f;

        if (weaponModel.projectile.HasBehavior<TravelStraitModel>())
        {
            var travelModel = projectile.GetBehavior<TravelStraitModel>();
            travelModel.speed *= 0.5f;
            travelModel.lifespan *= 2.5f;
        }

        towerModel.GetDescendants<FilterInvisibleModel>().ForEach(filter => filter.isActive = false);
    }
}