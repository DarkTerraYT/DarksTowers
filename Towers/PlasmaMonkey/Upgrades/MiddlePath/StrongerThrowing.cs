using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;

namespace DarksTowers.Towers.PlasmaMonkey.Upgrades.MiddlePath;

public class StrongerThrowing : ModUpgrade<PlasmaMonkey>
{
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        towerModel.IncreaseRange(10);
        if (towerModel.GetWeapon().projectile.HasBehavior<TravelStraitModel>())
        {
            towerModel.GetWeapon().projectile.GetBehavior<TravelStraitModel>().lifespan *= 1.125f;
        }

        towerModel.GetWeapon().projectile.pierce += 2;
    }

    public override int Path => Middle;
    public override int Tier => 1;
    public override int Cost => 425;

    public override string Description =>
        "Plasma monkey throws darts harder, allowing them to fly further, and pierce through more bloons.";
}