using System;
using System.Collections.Generic;
using System.Linq;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles;
using DarksTowers.Displays;
using HarmonyLib;
using Il2Cpp;
using Il2CppAssets.Scripts;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Bloons;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Map;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Filters;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Weapons;
using Il2CppAssets.Scripts.Simulation;
using Il2CppAssets.Scripts.Simulation.Bloons;
using Il2CppAssets.Scripts.Simulation.Bloons.Behaviors;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Simulation.Towers.Projectiles;
using Il2CppAssets.Scripts.Simulation.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Simulation.Track;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Bridge;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppNinjaKiwi;
using Il2CppNinjaKiwi.Common.ResourceUtils;
using UnityEngine;
using Math = System.Math;
using Vector3 = Il2CppAssets.Scripts.Simulation.SMath.Vector3;

namespace DarksTowers.Towers.PlasmaMonkey.Upgrades;

public class PlasmaMonkeyParagon : ModParagonUpgrade<PlasmaMonkey>
{
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var weapon = towerModel.GetWeapon();
        weapon.rate = 10;
        var projectile = weapon.projectile;
        projectile.pierce = 99999999999999;
        
        var damageModel = projectile.GetDamageModel();
        damageModel.damage = 50;
        damageModel.immuneBloonProperties = BloonProperties.None;
        damageModel.immuneBloonPropertiesOriginal = BloonProperties.None;
        
        projectile.AddBehavior(new DamageModifierForTagModel("DamageModifiers_Moabs", "Moabs", 5, 15, false, true));
        projectile.AddBehavior(new DamageModifierForTagModel("DamageModifiers_Bad", "Bad", 5, 0, false, true));
        projectile.AddBehavior(new DamageModifierForTagModel("DamageModifiers_Ddt", "Ddt", 10, 0, false, true)); // I hate all of you
        
        towerModel.GetDescendant<ProjectileModel>().ApplyDisplay<ParagonPlasmaBall>();

        projectile.RemoveBehavior<TravelStraitModel>();

        var travelAlongPathModel = new TravelAlongPathModel("TravelAlongPathModel_", 2, 300, false, true, 0);
        projectile.AddBehavior(travelAlongPathModel);
        
        var clearHitBloonsModel = new ClearHitBloonsModel("ClearHitBloonsModel", 0.05f);
        projectile.AddBehavior(clearHitBloonsModel);
        
        var attractBloonsModel = new TravelStraitModel("TravelStraitModel_PlasmaMonkey_AttractBloonsModel", 5f, 20); // Pull Strength (Numerator) (x), Event Horizon (Denominator) (y)
        projectile.AddBehavior(attractBloonsModel);
        var orbitModel = Game.instance.model.GetTowerFromId("BoomerangMonkey-Paragon").GetBehavior<OrbitModel>().Duplicate();
        orbitModel.name = "OrbitModel_PlasmaMonkey";
        var orbitProj = projectile.Duplicate();
        orbitProj.RemoveBehavior<TravelStraitModel>();
        orbitProj.RemoveBehavior<TravelAlongPathModel>();
        orbitProj.AddBehavior(new AgeModel("AgeModel_", 9999999, 9999999, true, null));
        orbitProj.AddFilter(new FilterAllModel("FilterAllModel_"));
        orbitProj.AddBehavior(new DontDestroyOnContinueModel("DontDestroyOnContinueModel_"));
        orbitProj.AddBehavior(new CantBeReflectedModel("CantBeReflectedModel_"));
        orbitModel.projectile =  orbitProj;
        orbitModel.count = 8;
        towerModel.AddBehavior(orbitModel);
        
       towerModel.GetDescendants<FilterInvisibleModel>().ForEach(filter => filter.isActive = false);
        
        var ability = Game.instance.model.GetTowerFromId("MonkeyBuccaneer-Paragon").GetAbility().Duplicate();
        ability.modelName = "AbilityModel_GravitationalCollapse";
        ability.RemoveBehavior<ActivateAttackModel>();
        ability.GetBehavior<CreateSoundOnAbilityModel>().sound = null; // for some reason there must be at least 1 behavior for the ability to appear so I'll just make it do nothing
        ability.animation = 3;
        ability.cooldown = 90;
        ability.icon = GetSpriteReference("PlasmaMonkeyParagonAA");
        towerModel.AddBehavior(ability);
        
        var ability2 = ability.Duplicate();
        ability2.modelName += "_2";
        ability2.activateOnPreLeak = true;
        ability2.isHidden = true;
        towerModel.AddBehavior(ability2);

        
        /*var dot = new DamageOverTimeModel("DamageOverTimeModel_PlasmaMonkey_GravitationalCollapse", Int32.MaxValue, 0, BloonProperties.None, BloonProperties.None, new(), 0, true, ObjectId.Invalid,
            false, 0, true, false, true, new Il2CppReferenceArray<DamageModifierModel>(0), false);
        
        var addBehaviors = new AddBehaviorToBloonInZoneModel("AddBehaviorToBloonInZoneModel_PlasmaMonkey", 9999999, "PMP:InstaKill", true, new Il2CppReferenceArray<BloonBehaviorModel>([dot]),
            new Il2CppReferenceArray<FilterModel>([]), "");*/
        
        var dot2= new DamageOverTimeModel("DamageOverTimeModel_PlasmaMonkey_Paragon", 1000, 0.05f, BloonProperties.None, BloonProperties.None, new(), 0, true, ObjectId.Invalid,
            false, 0, true, false, true, new Il2CppReferenceArray<DamageModifierModel>(0), false);
        
        var addBehaviors2 = new AddBehaviorToBloonInZoneModel("AddBehaviorToBloonInZoneModel_PlasmaMonkey2", towerModel.range, "PMP:InstaKill", true, new Il2CppReferenceArray<BloonBehaviorModel>([dot2]),
            new Il2CppReferenceArray<FilterModel>([]), "");
        
        //towerModel.AddBehavior(addBehaviors);
        towerModel.AddBehavior(addBehaviors2);
        towerModel.IncreaseRange(25);
    }

    private const float OrbitRange = 40f;

    [HarmonyPatch(typeof(Orbit), nameof(Orbit.Process))]
    private static class Orbit_Process
    {
        public static void Prefix(Orbit __instance, int elapsed)
        {
            if(__instance.orbitModel.name != "OrbitModel_PlasmaMonkey") return;
            float sineTime = Remap.RemapRange(Mathf.Sin(elapsed / 60f), -1, 1, 0, 1);
            __instance.orbitModel.range = OrbitRange * sineTime;
            __instance.projectiles.ForEach(new Action<Projectile>(proj =>
            {
                proj.Scale = new (sineTime, sineTime, sineTime);
            }));
        }
    }

    [HarmonyPatch(typeof(DamageOverTime), nameof(DamageOverTime.DealDamage))]
    private static class DamageOverTime_DealDamage
    {
        public static bool Prefix(DamageOverTime __instance)
        {
            return true;
        }
    }
    
    [HarmonyPatch(typeof(TravelStrait), nameof(TravelStrait.Process))]
    private static class TravelStrait_Process
    {
        private static bool Prefix(TravelStrait __instance)
        {
            TravelStraitModel model = __instance.travelStraitModel;
            if (model.name != "TravelStraitModel_PlasmaMonkey_AttractBloonsModel") return true;

            Projectile projectile = __instance.projectile;
        
            foreach (var bloon in InGame.instance.GetBloons())
            {
                if (bloon.HasTag("PlasmaMonkey_GravitationalCollapse")) continue;
                var dist = projectile.GetProjectileBehavior<TravelAlongPath>().distanceTraveled - bloon.distanceTraveled;
                if (Mathf.Abs(dist) > __instance.travelStraitModel.lifespan) continue;
                var strength = dist == 0 ? 0 : Mathf.Pow(Math.Abs(model.lifespan / dist), 1.25f) * (dist < 0 ? -1 : 1) * model.speed / bloon.bloonModel.radius; 
                bloon.distanceTraveled += strength;
                if (Math.Abs(projectile.GetProjectileBehavior<TravelAlongPath>().distanceTraveled - bloon.distanceTraveled) > Math.Abs(dist))
                {
                    bloon.distanceTraveled = projectile.GetProjectileBehavior<TravelAlongPath>().distanceTraveled;
                }
            }
        
            return false;    
        }
    }

    [HarmonyPatch(typeof(Ability), nameof(Ability.Activate))]
    private static class Ability_Activate
    {
        public static void Postfix(Ability __instance)
        {
            if (__instance.abilityModel.modelName.StartsWith("AbilityModel_GravitationalCollapse"))
            {
                var tower = __instance.tower;
                Vector3 towerPos = tower.Position.data;
                foreach (var bts in InGame.instance.GetAllBloonToSim())
                {
                    var bloon = bts.GetSimBloon();
                    bloon.Position.data = Vector3.MoveTowards(bloon.Position.data, towerPos, 2);
                    bloon.SetRotation(UnityEngine.Vector3.Angle(bloon.Position.ToUnity(), towerPos.ToUnity()));
                    bts.position = bloon.Position.ToUnity();

                    var newBloonModel = bloon.bloonModel.Duplicate();
                    newBloonModel.AddTag("PlasmaMonkey_GravitationalCollapse");
                    bloon.UpdatedModel(newBloonModel);
                    if(!AbilityEffectedBloonsByTower.TryAdd(tower.Id, [bts]))
                        AbilityEffectedBloonsByTower[tower.Id].Add(bts);
                }
            }
        }
    }

    [HarmonyPatch(typeof(Bloon), nameof(Bloon.UpdatePositionAlongTrack))]
    private static class Bloon_UpdatePositionAlongTrack
    {
        public static bool Prefix(Bloon __instance)
        {
            return !__instance.HasTag("PlasmaMonkey_GravitationalCollapse");
        }
    }
    

    public override bool DoesTick => true;
    
    protected override void Tick(int ticks, Simulation sim, Tower tower)
    {
        if (!AbilityEffectedBloonsByTower.TryGetValue(tower.Id, out var abilityEffectedBloons)) return;
        if (abilityEffectedBloons.Count == 0) return;
        
        HashSet<BloonToSimulation> nulls = new HashSet<BloonToSimulation>();
        
        Vector3 towerPos = tower.Position.data;
        foreach (var bts in abilityEffectedBloons)
        {
            var bloon = bts.GetSimBloon();
            if (bloon == null)
            {
                nulls.Add(bts);
                continue;
            }
            bloon.Position.data = Vector3.MoveTowards(bloon.Position.data, towerPos, 2);
            bloon.SetRotation(UnityEngine.Vector3.Angle(bloon.Position.ToUnity(), towerPos.ToUnity()) + 90f);
            bts.position = bloon.Position.data.ToUnity();

            if (ticks % 6 == 0)
            {
                bloon.pendingDamageTasks.Add(new Bloon.DamageTask() {blockSpawnChildren = true, createEffect = true, distributeToChildren = true,
                    immuneBloonProperties = BloonProperties.None, originalImmuneBloonProperties = BloonProperties.None, overrideDistributeBlocker = false,
                    projectile = tower.GetTowerBehavior<Orbit>().projectiles[0], tower = tower, totalAmount = bloon.bloonModel.maxHealth / 5f});
            }
        }

        foreach (var nullBloon in nulls)
        {
            abilityEffectedBloons.Remove(nullBloon);
        }
    }
    
    private static Dictionary<ObjectId, HashSet<BloonToSimulation>> AbilityEffectedBloonsByTower = [];
    
    public override string DisplayName => "Plasma Singularity";
    public override string Description => "Where all plasma becomes one.";

    public override int Cost => 1_500_000;
}