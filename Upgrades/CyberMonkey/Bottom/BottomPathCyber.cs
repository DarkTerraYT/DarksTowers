﻿using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Filters;
using Il2CppAssets.Scripts.Models.Towers.Projectiles;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Weapons;
using Il2CppAssets.Scripts.Unity;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DarksTowers.Displays.Proj.ProjectileDisplays;

namespace DarksTowers.Upgrades.CyberMonkey.Bottom
{
    internal class BetterSensors : ModUpgrade<DarksTowers.CyberMonkey>
    {
        public override int Path => BOTTOM;

        public override int Tier => 1;

        public override int Cost => 435;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.range += 20;
            towerModel.GetAttackModel().range += 20;
            towerModel.GetWeapon().rate *= 0.8f;
        }
    }
    internal class CamoSensors : ModUpgrade<DarksTowers.CyberMonkey>
    {
        public override int Path => BOTTOM;

        public override int Tier => 2;

        public override int Cost => 615;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.range += 10;
            towerModel.GetAttackModel().range += 10;
            towerModel.GetWeapon().rate *= 0.8f;
            towerModel.GetDescendants<FilterInvisibleModel>().ForEach(model => model.isActive = false);
        }
    }
    internal class SharpBeams : ModUpgrade<DarksTowers.CyberMonkey>
    {
        public override int Path => BOTTOM;
        public override int Tier => 3;
        public override int Cost => 1545;
        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetWeapon().projectile.pierce += 3;
            towerModel.GetWeapon().projectile.GetDamageModel().immuneBloonProperties = Il2Cpp.BloonProperties.Purple;
        }
    }
    internal class ExplodingBeams : ModUpgrade<DarksTowers.CyberMonkey>
    {
        public override int Path => BOTTOM;

        public override int Tier => 4;

        public override int Cost => 6545;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetWeapon().rate *= 0.8f;
            towerModel.GetWeapon().projectile.AddBehavior(Game.instance.model.GetTowerFromId("BombShooter").GetWeapon().projectile.GetBehavior<CreateProjectileOnContactModel>().Duplicate());
            towerModel.GetWeapon().projectile.GetDamageModel().damage += 5;
            towerModel.GetWeapon().projectile.pierce = 15;
            towerModel.GetWeapon().projectile.GetDamageModel().immuneBloonProperties = Il2Cpp.BloonProperties.Black;
            towerModel.GetWeapon().projectile.ApplyDisplay<ExplodingCyberLaser>();
        }
    }
    internal class RocketLauncer : ModUpgrade<DarksTowers.CyberMonkey>
    {
        public override int Path => BOTTOM;

        public override int Tier => 5;

        public override int Cost => 31545;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetAttackModel().AddWeapon(Game.instance.model.GetTowerFromId("BombShooter-003").GetWeapon().Duplicate());
            var Projectile = towerModel.GetWeapon(1).projectile;
            Projectile.pierce = towerModel.GetWeapon(0).projectile.pierce + 10;
            Projectile.AddBehavior(new DamageModel(null, towerModel.GetWeapon().projectile.GetDamageModel().damage, 9999999999999999999, false, false, true, Il2Cpp.BloonProperties.None, Il2Cpp.BloonProperties.None));
            var DamageModel = towerModel.GetWeapon(1).projectile.GetDamageModel();
            Projectile.ApplyDisplay<CyberBomb>();
            DamageModel.damage += 25;
            DamageModel.immuneBloonProperties = Il2Cpp.BloonProperties.None;
        }
    }
}