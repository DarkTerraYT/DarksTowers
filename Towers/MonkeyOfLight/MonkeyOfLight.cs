using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using DarksTowers.Displays;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;

namespace DarksTowers.Towers;

public class MonkeyOfLight : ModTower<DarksTowerSet>
{
    public override string BaseTower => TowerType.DartMonkey;

    public override int Cost => 750;

    public override string Icon => Portrait;

    public override bool DontAddToShop => true;

    public override void ModifyBaseTowerModel(TowerModel towerModel)
    {
        var wpn = towerModel.GetWeapon();
        var proj = wpn.projectile;

        var dmgModel = proj.GetDamageModel();
        dmgModel.damage = 3;
        proj.pierce = 5;
        proj.ApplyDisplay<BoltOfLight>();
        proj.AddBehavior(new RotateModel("RotateModel_", -144));
        proj.GetBehavior<TravelStraitModel>().speed /= 3;
        proj.GetBehavior<TravelStraitModel>().lifespan *= 10;
    }
}