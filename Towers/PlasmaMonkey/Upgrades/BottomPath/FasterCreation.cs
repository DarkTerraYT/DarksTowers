using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;

namespace DarksTowers.Towers.PlasmaMonkey.Upgrades.BottomPath;

public class FasterCreation : ModUpgrade<PlasmaMonkey>
{
    public override string Description =>
        "Plasma Monkey regenerates plasma 25% faster, and therefore can conjure plasma darts 25% faster.";

    public override int Path => Bottom;
    public override int Tier => 1;
    public override int Cost => 650;

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var weaponModel = towerModel.GetWeapon();
        weaponModel.rate *= 0.75f;
    }
}