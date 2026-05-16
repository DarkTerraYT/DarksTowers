using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;

namespace DarksTowers.Towers.FireMonkey.TopPath;

public class Incarnation : ModUpgrade<FireMonkey>
{
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var createTower = new TowerCreateTowerModel("TowerCreateTowerModel_Emberling", GetTowerModel<FireSpirit>().Duplicate(), true);
        towerModel.AddChildDependant(createTower.towerModel);

        var newAttack = towerModel.GetAttackModel().Duplicate();
        towerModel.AddBehavior(newAttack);

        var attack = towerModel.GetAttackModel();
        attack.range = 900f;
        attack.RemoveWeapon(attack.weapons[0]);
        
        towerModel.AddBehavior(createTower);
    }

    public override int Path => Top;
    public override int Tier => 3;
    public override int Cost => 850;

    public override string Description => "Fire Monkey now creates the \"Emberling!\" This is a creature made of fire and will attack bloons for you, similar to Corvus's spirit.";
}