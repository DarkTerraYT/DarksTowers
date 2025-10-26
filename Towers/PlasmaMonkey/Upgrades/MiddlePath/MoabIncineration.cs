using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using DarksTowers.Displays;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppNinjaKiwi.Common.ResourceUtils;

namespace DarksTowers.Towers.PlasmaMonkey.Upgrades.MiddlePath;

public class MoabIncineration : ModUpgrade<PlasmaMonkey>
{
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var weaponModel = towerModel.GetWeapon();
        var projectile = weaponModel.projectile;
        towerModel.IncreaseRange(5);
        if (projectile.HasBehavior<TravelStraitModel>())
        {
            projectile.GetBehavior<TravelStraitModel>().lifespan *= 1.125f;
        }

        projectile.pierce += 2;

        var addBehavior = projectile.GetBehavior<AddBehaviorToBloonModel>();
        var dot = addBehavior.GetBehavior<DamageOverTimeModel>();
        dot.Interval *= 2 / 3f;
        dot.damage += 3;
        addBehavior.lifespan *= 1.5f;
        dot.damageModifierModels = dot.damageModifierModels.AddTo(new DamageModifierForTagModel("DamageModifierForTagModel_", "Moabs", 2f, 0, false, true));
        projectile.UpdateCollisionPassList();
        
        var attackModel2 = towerModel.GetAttackModel().Duplicate();
        attackModel2.weapons[0].rate *= 0.9f;
        attackModel2.weapons[0].projectile.AddBehavior(new DamageModifierForTagModel("DamageModifierForTagModel_", "Moabs", 4f, 0, false, true));
        var activateAttackModel = new ActivateAttackModel("ActivateAttackModel_MoabIncineration", 10, true, new Il2CppReferenceArray<AttackModel>([attackModel2]), false, true, false, false, false, true);
        var abiliy = new AbilityModel("AbilityModel_MoabIncineration", "MOAB Incineration", "Darts do 4x damage to MOAB class bloons for a short period of time", 0, 0, GetSpriteReference(Icon), 60, new Il2CppReferenceArray<Model>([activateAttackModel]), false, false, Id, 0, 0, 999999999, 999999999, false, false);
        
        towerModel.AddBehavior(abiliy);
    }

    public override int Path => Middle;
    public override int Tier => 4;
    public override int Cost => 6550;

    public override string DisplayName => "MOAB Incineration";
    public override string Description =>
        "Plasma darts melt bloons for longer, faster, and with more damage! Gains a new ability, MOAB Incineration: Darts do 4x damage to MOAB class bloons for a short period of time.";
}