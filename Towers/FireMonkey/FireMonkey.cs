using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using DarksTowers.Displays;
using HarmonyLib;
using Il2Cpp;
using Il2CppAssets.Scripts.Models.Effects;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Weapons.Behaviors;
using Il2CppAssets.Scripts.Simulation.Towers.Projectiles;
using Il2CppAssets.Scripts.Simulation.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;
using Il2CppNinjaKiwi.Common;
using Il2CppNinjaKiwi.Common.ResourceUtils;
using UnityEngine;

namespace DarksTowers.Towers.FireMonkey;

public class FireMonkey : ModTower<DarksTowerSet>
{
    public override string BaseTower => TowerType.DartMonkey;

    public override int Cost => 1400;

    public override string Icon => Portrait;

    public override string Description =>
        "This monkey is made entirely of plasma! The plasma monkey uses this plasma to conjure plasma darts to attack the bloons with!";

    public override void ModifyBaseTowerModel(TowerModel towerModel)
    {
        var wpn = towerModel.GetWeapon();
        wpn.ejectX = 0;
        var proj = wpn.projectile;
        
        var dmgModel = proj.GetDamageModel();
        dmgModel.damage = 1f;
        dmgModel.immuneBloonProperties = BloonProperties.Purple;
        proj.pierce = 1000;
        proj.SetDisplay("01dfdf7fe33be28409a9c2e1db9bbec0");
        
        proj.RemoveBehavior<TravelStraitModel>();
        proj.display = new PrefabReference("");
        proj.AddBehavior(new AgeModel("AgeModel", 0.25f, 0, false, null));
        wpn.rate = 0.25f;
    }

    public override bool DontAddToShop => true;

    public override bool IsValidCrosspath(int[] tiers) =>
    ModHelper.HasMod("UltimateCrosspathing") || base.IsValidCrosspath(tiers);
}