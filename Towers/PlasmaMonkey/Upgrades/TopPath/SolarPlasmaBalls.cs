using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using DarksTowers.Displays;
using HarmonyLib;
using Il2CppAssets.Scripts;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Simulation.Bloons;
using Il2CppAssets.Scripts.Simulation.Towers.Projectiles;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using UnityEngine;

namespace DarksTowers.Towers.PlasmaMonkey.Upgrades.TopPath;

public class SolarPlasmaBalls : ModUpgrade<PlasmaMonkey>
{
    
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var weaponModel = towerModel.GetWeapon();
        weaponModel.rate = 3;
        
        var projectile = weaponModel.projectile;
        projectile.pierce = 50;
        projectile.id = "SolarPlasmaBall";
        projectile.GetDamageModel().damage = 2000;
        projectile.ApplyDisplay<SolarPlasmaBall>();

        projectile.RemoveBehavior<TravelStraitModel>();

        var travelAlongPathModel = new TravelAlongPathModel("TravelAlongPathModel_", 20, 30, false, true, 0);
        projectile.AddBehavior(travelAlongPathModel);
        
        var clearHitBloonsModel = new ClearHitBloonsModel("ClearHitBloonsModel", 0.2f);
        projectile.AddBehavior(clearHitBloonsModel);
    }

    public override string DisplayName => "Stellar Plasma";

    public override string Description => "Plasma Monkey now harnesses the power of the Stars! With a little help from the Sun Avatars, the Plasma Monkey can create balls of Stellar Plasma!" +
                                          " This process takes a while, though once it's shot out, it melts every bloon in its path! Each stellar plasma ball can hit each bloon multiple times instead of once!";

    public override int Path => Top;

    public override int Tier => 5;

    public override int Cost => 225000;
}