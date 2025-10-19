using System.Collections.Generic;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using DarksTowers.Displays;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Filters;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Weapons.Behaviors;

namespace DarksTowers.Towers.PlasmaMonkey.Upgrades.BottomPath;

public class PlasmaGuns : ModUpgrade<PlasmaMonkey>
{
    public override string Description =>
        "The plasma monkey now uses two plasma guns! These allow for more bloon popping power!.";

    public override int Path => Bottom;
    public override int Tier => 4;
    public override int Cost => 10000;

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        towerModel.IncreaseRange(10);
        
        var weaponModel = towerModel.GetWeapon();
        
        ThrowMarkerOffsetModel[] emissionOffsets = new ThrowMarkerOffsetModel[2] { new ThrowMarkerOffsetModel("Gun_Left", -3.5f, 0, weaponModel.ejectZ, 0),
            new ThrowMarkerOffsetModel("Gun_Right", 3.5f, 0, weaponModel.ejectZ, 0) };
        
        weaponModel.ejectX = 0;
        weaponModel.ejectZ = 0;
        
        weaponModel.SetEmission(new EmissionWithOffsetsModel("EmissionsWithOffsetsModel_", emissionOffsets, 2, false, null, 0, false));

        weaponModel.rate /= 3f;
        
        var projectile = weaponModel.projectile;

        projectile.GetDamageModel().damage += 2;
        projectile.AddBehavior(new DamageModifierForTagModel("DamageModifierForTagModel_Moabs", "Moabs", 1.2f, 1, false,
            true));
        projectile.AddBehavior(new DamageModifierForTagModel("DamageModifierForTagModel_Fortified", "Fortified", 1.35f,
            1, false, true));
        projectile.ApplyDisplay<PlasmaBeam>();

        projectile.radius = towerModel.range / 2;
        

        if (weaponModel.projectile.HasBehavior<TravelStraitModel>())
        {
            var travelModel = projectile.GetBehavior<TravelStraitModel>();
            travelModel.speed *= 0.5f;
            travelModel.lifespan *= 2.5f;
        }
    }
}