using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using DarksTowers.Displays;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;
using Il2CppNinjaKiwi.Common.ResourceUtils;

namespace DarksTowers.Towers.PlasmaMonkey.Upgrades.MiddlePath;

public class MoabMeltingDarts : ModUpgrade<PlasmaMonkey>
{
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        towerModel.IncreaseRange(5);
        if (towerModel.GetWeapon().projectile.HasBehavior<TravelStraitModel>())
        {
            towerModel.GetWeapon().projectile.GetBehavior<TravelStraitModel>().lifespan *= 1.125f;
        }

        towerModel.GetWeapon().projectile.pierce += 2;

        var addBehavior = Game.instance.model.GetTowerFromId("MortarMonkey-003").GetDescendant<AddBehaviorToBloonModel>()
            .Duplicate();
        addBehavior.ApplyOverlay<Plasma>();
        var dot = addBehavior.GetBehavior<DamageOverTimeModel>();

        towerModel.GetWeapon().projectile.AddBehavior(addBehavior);
        towerModel.GetWeapon().projectile.UpdateCollisionPassList();
    }

    public override int Path => Middle;
    public override int Tier => 3;
    public override int Cost => 1250;

    public override string Description =>
        "Plasma darts now do more damage to MOAB bloons, and melts them overtime.";
}