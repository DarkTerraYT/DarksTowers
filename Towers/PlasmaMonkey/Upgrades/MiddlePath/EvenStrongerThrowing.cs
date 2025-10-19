using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;

namespace DarksTowers.Towers.PlasmaMonkey.Upgrades.MiddlePath;

public class EvenStrongerThrowing : ModUpgrade<PlasmaMonkey>
{
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        towerModel.IncreaseRange(5);

        if (towerModel.GetWeapon().projectile.HasBehavior<TravelStraitModel>())
        {
            towerModel.GetWeapon().projectile.GetBehavior<TravelStraitModel>().lifespan *= 1.125f;
        }

        towerModel.GetWeapon().projectile.pierce += 2;
    }

    public override int Path => Middle;
    public override int Tier => 2;
    public override int Cost => 680;

    public override string Description =>
        "Plasma monkey throws darts even harder, allowing them to fly even further, and pierce through even more bloons.";
}