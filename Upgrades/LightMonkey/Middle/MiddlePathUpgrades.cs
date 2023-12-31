﻿using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using DarksTowers.Displays.MoneyofLight;
using Il2Cpp;
using Il2CppAssets.Scripts.Models.Towers;
using static DarksTowers.Displays.Proj.ProjectileDisplays;

namespace DarksTowers.Upgrades.LightMonkey.Middle
{
    public class FasterBlasts : ModUpgrade<MonkeyofLight>
    {
        public override int Path => MIDDLE;

        public override int Tier => 1;

        public override int Cost => 675;
        public override string Description => "Makes The Monkey of Light Attack Twice as Fast!";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            foreach (var weaponModel in towerModel.GetWeapons())
            {
                weaponModel.rate *= 0.5f;
            }
        }
    }
    public class VeryFastBlasts : ModUpgrade<MonkeyofLight>
    {
        public override int Path => MIDDLE;

        public override int Tier => 2;

        public override int Cost => 855;
        public override string Description => "The Monkey of Light Attacks Even Faster Now!";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            foreach (var weaponModel in towerModel.GetWeapons())
            {
                weaponModel.rate *= 0.45f;
            }
        }
    }
    public class HeatBlasts : ModUpgrade<MonkeyofLight>
    {
        public override int Path => MIDDLE;

        public override int Tier => 3;

        public override int Cost => 1145;
        public override string Description => "The Monkey of Light Shoots The Light so Fast The Light is Starting to Get Hot!";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.ApplyDisplay<MonkeyofLight030Display>();
            var weaponModel = towerModel.GetWeapon();
            var proj = weaponModel.projectile;
            var damageModel = proj.GetDamageModel();
            damageModel.damage += 3;
            proj.ApplyDisplay<HeatBlast>();
            foreach(var weapon in towerModel.GetWeapons())
            {
                weapon.rate *= 0.67f;
            }
        }
    }
    public class FireBlasts : ModUpgrade<MonkeyofLight>
    {
        public override int Path => MIDDLE;
        public override int Tier => 4;
        public override int Cost => 3452;
        public override string Description => "The Light Blasts Are Being Shot so Fast it's Making it so Hot it's Practically Fire! Can Now Pop Lead Bloons.";
        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.ApplyDisplay<MonkeyofLight040Display>();
            var weaponModel = towerModel.GetWeapon();
            var proj = weaponModel.projectile;
            var damageModel = proj.GetDamageModel();
            damageModel.damage += 6;
            proj.ApplyDisplay<FireBlast>();
            foreach (var weapon in towerModel.GetWeapons())
            {
                weapon.projectile.GetDamageModel().immuneBloonProperties = BloonProperties.None;
                weapon.rate *= 0.67f;
            }
        }
    }
    public class LavaBlasts : ModUpgrade<MonkeyofLight>
    {
        public override int Path => MIDDLE;
        public override int Tier => 5;
        public override int Cost => 23650;
        public override string Description => "The Light Got so Hot it Leaves Nothing Behind...";
        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.ApplyDisplay<MonkeyofLight050Display>();
            var weaponModel = towerModel.GetWeapon();
            var proj = weaponModel.projectile;
            var damageModel = proj.GetDamageModel();
            damageModel.damage += 18;
            proj.ApplyDisplay<LavaBlast>();
            foreach (var weapon in towerModel.GetWeapons())
            {
                weapon.rate *= 0.5f;
            }
        }
    }
}
