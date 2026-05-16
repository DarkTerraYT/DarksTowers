using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;

namespace DarksTowers.Towers.FireMonkey.TopPath;

public class LongerLastingFlames : ModUpgrade<FireMonkey>
{
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        towerModel.IncreaseRange(towerModel.range * 0.1f);
        towerModel.GetDescendants<TravelStraitModel>().ForEach(model => model.lifespan *= 1.1f);
    }

    public override int Path => Top;
    public override int Tier => 2;
    public override int Cost => 415;

    public override string Description =>
        "Fire monkey can shoot another 1.1x further.";
}