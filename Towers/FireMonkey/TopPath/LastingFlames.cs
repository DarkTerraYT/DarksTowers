using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;

namespace DarksTowers.Towers.FireMonkey.TopPath;

public class LastingFlames : ModUpgrade<FireMonkey>
{
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        towerModel.IncreaseRange(towerModel.range * 0.1f);
        towerModel.GetDescendants<TravelStraitModel>().ForEach(model => model.lifespan *= 1.1f);
    }

    public override int Path => Top;
    public override int Tier => 1;
    public override int Cost => 345;

    public override string Description =>
        "Flames created by Fire Monkey last 1.1x longer! Fire Monkey also has 1.1x more range.";
}