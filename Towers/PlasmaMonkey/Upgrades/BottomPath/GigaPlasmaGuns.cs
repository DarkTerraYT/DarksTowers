using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using DarksTowers.Displays;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Filters;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;

namespace DarksTowers.Towers.PlasmaMonkey.Upgrades.BottomPath;

public class GigaPlasmaGuns : ModUpgrade<PlasmaMonkey>
{
    public override string Description =>
        "The Plasma Guns are now <color=#ffd732><b>GIGA!</b></color> Plasma Monkey now shoots at HYPERSONIC speeds and plasma beams travel much much faster! Plasma Beams are now also Solar Plasma Beams.";

    public override int Path => Bottom;
    public override int Tier => 5;
    public override int Cost => 160000;

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        towerModel.IncreaseRange(20);
        
        var weaponModel = towerModel.GetWeapon();

        weaponModel.rate = 0;
        
        var projectile = weaponModel.projectile;
        projectile.pierce = 1;
        projectile.GetDamageModel().damage = 1;
        projectile.RemoveBehaviors<DamageModifierForTagModel>();
        projectile.ApplyDisplay<SolarPlasmaBeam>();

        if (projectile.HasBehavior<TravelStraitModel>())
        {
            var travelModel = projectile.GetBehavior<TravelStraitModel>();
            travelModel.speed *= 3f;
            travelModel.lifespan *= 2.5f;
        }
    }
}