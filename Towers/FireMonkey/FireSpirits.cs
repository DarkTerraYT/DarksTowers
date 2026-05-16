using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using DarksTowers.Displays;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.UI_New.InGame.TowerSelectionMenu;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using UnityEngine;

namespace DarksTowers.Towers.FireMonkey;

public class FireSpirit : ModSubTower<FireMonkey>
{   
    public override void ModifyBaseTowerModel(TowerModel towerModel)
    {
        var weapon = towerModel.GetWeapon();
        var projectile = weapon.projectile;

        weapon.rate = 0.25f;
        weapon.SetEject(Vector3.zero);
        projectile.ApplyDisplay<FireRing>();
        projectile.radius = 15;
        
        var movement = towerModel.GetBehavior<SpiritTowerMovementModel>();
        movement.spinAttackAnimState = 1;
        movement.baseAttackAnimState = 1;
        
        towerModel.ApplyDisplay<FireSpiritDisplay>();
    }

    public override TowerModel GetBaseTowerModel(params int[] tiers)
    {
        return Game.instance.model.GetTower("Corvus").GetBehavior<TowerCreateTowerModel>().towerModel.MakeCopy(Id);
    }

    public override string BaseTower => TowerType.DartMonkey;
    public override int Cost => 0;

    protected override int Order => -1;

    public override string Icon => Portrait;
    public override string DisplayName => "Emberling";
}
public class FireSpirit2 : ModSubTower<FireMonkey>
{
    public override void ModifyBaseTowerModel(TowerModel towerModel)
    {
        var movement = Game.instance.model.GetTower("Corvus").GetBehavior<TowerCreateTowerModel>().towerModel.GetBehavior<SpiritTowerMovementModel>().Duplicate();
        movement.maxSpeed *= 1.5f;
        movement.acceleration *= 2f;
        movement.accelerateInAngle *= 2f;
        movement.spinAttackAnimState = 1;
        movement.baseAttackAnimState = 1;
        
        towerModel.AddBehavior(movement);
        
        towerModel.ApplyDisplay<FireSpirit2Display>();
    }

    public override string BaseTower => TowerType.DartMonkey;
    public override int Cost => 0;
    
    protected override int Order => -1;

    public override string Icon => Portrait;
    public override string DisplayName => "Flareborn";
}
public class FireSpirit3 : ModSubTower<FireMonkey>
{
    public override void ModifyBaseTowerModel(TowerModel towerModel)
    {
        var movement = Game.instance.model.GetTower("Corvus").GetBehavior<TowerCreateTowerModel>().towerModel.GetBehavior<SpiritTowerMovementModel>().Duplicate();
        movement.maxSpeed *= 1.5f;
        movement.acceleration *= 2f;
        movement.accelerateInAngle *= 2f;
        
        towerModel.AddBehavior(movement);
        
        towerModel.ApplyDisplay<FireSpirit3Display>();
    }

    public override string BaseTower => TowerType.DartMonkey;
    public override int Cost => 0;
    
    protected override int Order => -1;

    public override string Icon => Portrait;

    public override string DisplayName => "Pyroblast";
}
/*
For the paragon once I eventually make it
public class FireSpirit4 : ModSubTower<FireMonkey>
{
    public override void ModifyBaseTowerModel(TowerModel towerModel)
    {
        var movement = Game.instance.model.GetTower("Corvus").GetBehavior<TowerCreateTowerModel>().towerModel.GetBehavior<SpiritTowerMovementModel>().Duplicate();
        towerModel.AddBehavior(movement);
        
        towerModel.ApplyDisplay<FireSpirit2Display>();
    }

    public override string BaseTower => TowerType.DartMonkey;
    public override int Cost => 0;
    
    protected override int Order => -1;

    public override string Icon => Portrait;

    public override string DisplayName => "Ignathar";
}*/