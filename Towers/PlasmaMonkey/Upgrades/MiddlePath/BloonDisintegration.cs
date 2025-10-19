using System.Collections.Generic;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;

namespace DarksTowers.Towers.PlasmaMonkey.Upgrades.MiddlePath;

public class BloonDisintegration : ModUpgrade<PlasmaMonkey>
{
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var projectile = towerModel.GetWeapon().projectile;

        projectile.GetDamageModel().damage = 0;

        var addBehaviors = projectile.GetBehavior<AddBehaviorToBloonModel>();
        addBehaviors.applyOnlyIfDamaged = false;
        var dot = addBehaviors.GetBehavior<DamageOverTimeModel>();
        dot.damage = 85;
        dot.interval = 0.1f;
        List<DamageModifierModel> damageModifiers = [new DamageModifierForTagModel("DamageModifierForTagModel_Moabs", "Moabs", 5, 0, false, true)];

        var activateAttackModel = towerModel.GetAbility().GetBehavior<ActivateAttackModel>();
        towerModel.GetAbility().icon = GetSpriteReference(Icon);
        activateAttackModel.attacks[0].weapons[0] = towerModel.GetWeapon().Duplicate();
        activateAttackModel.attacks[0].weapons[0].projectile.GetDescendant<DamageOverTimeModel>().damage *= 4;
    }

    public override string Description =>
        "No longer does any damage, but disintegrates bloons quickly with the plasma fire. Ability, Bloon Disintegration: Replaces MOAB Incineration, but now the DOT is 4x stronger.";

    public override int Path => Middle;
    public override int Tier => 5;
    public override int Cost => 75000;
}