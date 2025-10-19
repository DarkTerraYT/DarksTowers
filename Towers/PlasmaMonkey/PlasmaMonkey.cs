using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using DarksTowers.Displays;
using Il2Cpp;
using Il2CppAssets.Scripts.Models.Towers;

namespace DarksTowers.Towers.PlasmaMonkey;

public class PlasmaMonkey : ModTower<DarksTowerSet>
{
    public override string BaseTower => TowerType.DartMonkey;

    public override int Cost => 1400;

    public override string Icon => Portrait;

    public override string Portrait => "PlasmaMonkey-Portrait";

    public override string Description =>
        "This monkey is made entirely of plasma! The plasma monkey uses this plasma to conjure plasma darts to attack the bloons with!";

    public override void ModifyBaseTowerModel(TowerModel towerModel)
    {
        var wpn = towerModel.GetWeapon();
        var proj = wpn.projectile;

        var dmgModel = proj.GetDamageModel();
        dmgModel.damage = 3;
        dmgModel.immuneBloonProperties = BloonProperties.Purple;
        proj.pierce = 5;
        proj.ApplyDisplay<PlasmaDart>();
    }

    public override bool IsValidCrosspath(int[] tiers) =>
    ModHelper.HasMod("UltimateCrosspathing") || base.IsValidCrosspath(tiers);
}