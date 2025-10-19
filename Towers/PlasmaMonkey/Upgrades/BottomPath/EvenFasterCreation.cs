using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;

namespace DarksTowers.Towers.PlasmaMonkey.Upgrades.BottomPath;

public class EvenFasterCreation : ModUpgrade<PlasmaMonkey>
{
    public override string Description =>
        "Plasma Monkey regenerates plasma ANOTHER 25% faster, and conjures plasma darts even faster. Plasma darts also travel faster and can pierce through more bloons.";

    public override int Path => Bottom;
    public override int Tier => 2;
    public override int Cost => 725;

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var weaponModel = towerModel.GetWeapon();
        weaponModel.rate *= 0.75f;


        if (weaponModel.projectile.HasBehavior<TravelStraitModel>())
        {
            var travelModel = weaponModel.projectile.GetBehavior<TravelStraitModel>();
            travelModel.speed *= 1.125f;
        }

        weaponModel.projectile.pierce += 2;
    }
}