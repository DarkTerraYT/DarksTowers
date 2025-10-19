using System.Linq;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Extensions;
using DarksTowers.Towers;
using DarksTowers.Towers.PlasmaMonkey;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppNinjaKiwi.Common.ResourceUtils;
using UnityEngine;

namespace DarksTowers.Displays;
public class PlasmaMonkeyDisplay : ModTowerDisplay<PlasmaMonkey>
{
    public override string BaseDisplay => GetDisplay(TowerType.DartMonkey);

    public override float Scale => 1;

    public override bool UseForTower(params int[] tiers)
    {
        return tiers.Max() <=3 && tiers[1] < 3;
    }

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        for (var i = 0; i < node.GetMeshRenderers().Count; i++)
        {
            SetMeshTexture(node,$"{Name}-{i}", i);
            node.GetMeshRenderer(i).SetOutlineColor(new Color32(82, 0, 87, 255));
        }

        node.RemoveBone("NewMonkeyRigDart:DartCtl");
    }
}
public class FieryPlasmaBallsDisplay : ModTowerDisplay<PlasmaMonkey>
{
    public override string BaseDisplay => GetDisplayGUID<PlasmaMonkeyDisplay>();

    public override float Scale => 1;

    public override bool UseForTower(params int[] tiers)
    {
        return tiers[0] == 4;
    }

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        SetMeshTexture(node, "FieryPlasmaBallsDisplay-2", 2);

        var effect = Main.GetObject("PlasmaMonkey400Effect");
        var effectInstantiated = Object.Instantiate(effect, node.transform);
        effectInstantiated.transform.localPosition = new Vector3(0, -2.3782f, -8.3f);
    }
}
public class SolarPlasmaBallsDisplay : ModTowerDisplay<PlasmaMonkey>
{
    public override string BaseDisplay => GetDisplay(TowerType.DartMonkey);

    public override float Scale => 1;

    public override bool UseForTower(params int[] tiers)
    {
        return tiers[0] == 5;
    }

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        for (var i = 0; i < node.GetMeshRenderers().Count; i++)
        {
            SetMeshTexture(node,$"{Name}-{i}", i);
            node.GetMeshRenderer(i).SetOutlineColor(new Color32(6, 68, 111, 255));
        }
        var effect = Main.GetObject("PlasmaMonkey500Effect");
        var effectInstantiated = Object.Instantiate(effect, node.transform);
        effectInstantiated.transform.GetChild(0).SetParent(node.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0), true);
        effectInstantiated.transform.localPosition = new Vector3(effectInstantiated.transform.localPosition.x, 1, effectInstantiated.transform.localPosition.z);

        node.RemoveBone("NewMonkeyRigDart:DartCtl");
    }
}
public class MoabMeltingDartsDisplay : ModTowerDisplay<PlasmaMonkey>
{
    public override string BaseDisplay => GetDisplayGUID<PlasmaMonkeyDisplay>();

    public override float Scale => 1;

    public override bool UseForTower(params int[] tiers)
    {
        return tiers[1] == 3;
    }

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        var fireHorn = Object.Instantiate(Main.GetObject("FireParticles"), node.transform);
        fireHorn.transform.SetParent(node.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0), true);

        var fireHornRenderers = fireHorn.GetComponentsInChildren<Renderer>().ToList();

        node.genericRendererLayers =
            new Il2CppStructArray<int>(node.genericRendererLayers.Count + fireHornRenderers.Count);
        node.genericRenderers = node.genericRenderers.AddTo(fireHornRenderers);
        node.RecalculateGenericRenderers();
    }
}
public class MoabIncinerationDisplay : ModTowerDisplay<PlasmaMonkey>
{
    public override string BaseDisplay => GetDisplayGUID<PlasmaMonkeyDisplay>();

    public override float Scale => 1;

    public override bool UseForTower(params int[] tiers)
    {
        return tiers[1] == 4;
    }

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        var fireHorn = Object.Instantiate(Main.GetObject("FireParticlesT4"), node.transform);
        fireHorn.transform.SetParent(node.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0), true);

        var fireHornRenderers = fireHorn.GetComponentsInChildren<Renderer>().ToList();

        node.genericRendererLayers =
            new Il2CppStructArray<int>(node.genericRendererLayers.Count + fireHornRenderers.Count);
        node.genericRenderers = node.genericRenderers.AddTo(fireHornRenderers);
        node.RecalculateGenericRenderers();
    }
}
public class BloonDisintegrationDisplay : ModTowerDisplay<PlasmaMonkey>
{
    public override string BaseDisplay => GetDisplay(TowerType.DartMonkey);

    public override float Scale => 1;

    public override bool UseForTower(params int[] tiers)
    {
        return tiers[1] == 5;
    }

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        for (var i = 0; i < node.GetMeshRenderers().Count; i++)
        {
            SetMeshTexture(node,$"{Name}-{i}", i);
            node.GetMeshRenderer(i).SetOutlineColor(new Color32(82, 0, 15, 255));
        }

        node.RemoveBone("NewMonkeyRigDart:DartCtl");
        
        var fireHorn = Object.Instantiate(Main.GetObject("FireParticlesT5"), node.transform);
        var groundEffect = fireHorn.transform.FindChild("GroundEffect");
        groundEffect.parent = node.transform.GetChild(0);
        fireHorn.transform.SetParent(node.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0), true);

        var fireHornRenderers = fireHorn.GetComponentsInChildren<Renderer>().ToList();

        node.genericRendererLayers =
            new Il2CppStructArray<int>(node.genericRendererLayers.Count + fireHornRenderers.Count + 1);
        node.genericRenderers = node.genericRenderers.AddTo(fireHornRenderers);
        node.genericRenderers = node.genericRenderers.AddTo(groundEffect.GetComponent<ParticleSystemRenderer>());
        node.RecalculateGenericRenderers();
    }
}

public class PlasmaGunsTowerDisplay : ModTowerDisplay<PlasmaMonkey>
{
    public override string BaseDisplay => GetDisplay(TowerType.DartMonkey);

    public override float Scale => 1;

    public override bool UseForTower(params int[] tiers)
    {
        return tiers[2] == 4;
    }

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        for (var i = 0; i < node.GetMeshRenderers().Count; i++)
        {
            node.GetMeshRenderer(i).SetMainTexture(GetTexture($"PlasmaMonkeyDisplay-{i}"));
            node.GetMeshRenderer(i).SetOutlineColor(new Color32(82, 0, 87, 255));
        }

        node.RemoveBone("NewMonkeyRigDart:DartCtl");
        node.RemoveBone("NewMonkeyRigDart:MonkeyJnt_Arm_R");
        node.RemoveBone("NewMonkeyRigDart:MonkeyJnt_Arm_L");
    }

    public override void ApplyToTower(TowerModel towerModel)
    {
        base.ApplyToTower(towerModel);
        if (!towerModel.HasBehavior<AttackModel>())
        {
            return;
        }

        var atkModel = towerModel.GetAttackModel();
        if (!atkModel.HasBehavior<DisplayModel>())
        {
            atkModel.AddBehavior(new DisplayModel(atkModel.name + "_Display", new PrefabReference(""), towerModel.GetBehavior<DisplayModel>().layer, DisplayCategory.Tower));
        }

        atkModel.GetBehavior<DisplayModel>().ApplyDisplay<PlasmaGunsDisplay>();
    }

    public class PlasmaGunsDisplay : ModDisplay
    {
        public override string BaseDisplay => Game.instance.model.GetTower("SuperMonkey", 0, 3).GetAttackModel()
            .GetBehavior<DisplayModel>().display.AssetGUID;

        private static readonly int OutlineColor = Shader.PropertyToID("_OutlineColor");

        private void SetOutlineColor(Material mat, Color color)
        {
            mat.SetColor(OutlineColor, color);
            mat.SetShaderKeywords((Il2CppStringArray)System.Array.Empty<string>());
        }

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            node.transform.GetChild(1).localPosition = new(0, -1, -5);
            node.transform.GetChild(1).localRotation = Quaternion.Euler(3.8f, 0, 0);
            node.transform.GetChild(1).localScale = new(1, 1, 1);
            node.transform.GetChild(1).GetChild(0).GetChild(3).gameObject.active = true;
            
            node.genericRenderers = node.genericRenderers.AddTo(node.transform.GetChild(1).GetChild(0).GetChild(3).GetComponent<SkinnedMeshRenderer>());
            node.genericRendererLayers = new Il2CppStructArray<int>(2);
            node.RecalculateGenericRenderers();
            
            var renderer = node.GetMeshRenderer();
            renderer.material.mainTexture = GetTexture($"{Name}-0");
            renderer.materials[1].mainTexture = GetTexture($"{Name}-1");
            SetOutlineColor(renderer.material, new Color32(82, 0, 87, 255));
            SetOutlineColor(renderer.materials[1], new Color32(82, 0, 87, 255));
            renderer = node.GetMeshRenderer(1);
            renderer.material.mainTexture = GetTexture($"{Name}-0");
            renderer.materials[1].mainTexture = GetTexture($"{Name}-1");
            SetOutlineColor(renderer.material, new Color32(82, 0, 87, 255));
            SetOutlineColor(renderer.materials[1], new Color32(82, 0, 87, 255));
        }
    }
}
public class GigaPlasmaGunsTowerDisplay : ModTowerDisplay<PlasmaMonkey>
{
    public override string BaseDisplay => GetDisplay(TowerType.DartMonkey);

    public override float Scale => 1;

    public override bool UseForTower(params int[] tiers)
    {
        return tiers[2] == 5;
    }

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        for (var i = 0; i < node.GetMeshRenderers().Count; i++)
        {
            node.GetMeshRenderer(i).SetMainTexture(GetTexture($"PlasmaMonkeyDisplay2-{i}"));
            node.GetMeshRenderer(i).SetOutlineColor(new Color32(76, 61, 0, 255));
        }

        node.RemoveBone("NewMonkeyRigDart:DartCtl");
        node.RemoveBone("NewMonkeyRigDart:MonkeyJnt_Arm_R");
        node.RemoveBone("NewMonkeyRigDart:MonkeyJnt_Arm_L");

        var aura = Object.Instantiate(Main.GetObject("GigaPlasmaGunsAura"), node.transform.GetChild(0));
        node.genericRendererLayers = new Il2CppStructArray<int>(node.genericRendererLayers.Count + 3);
        node.genericRenderers = node.genericRenderers.AddTo(aura.GetComponentsInChildren<Renderer>().ToList());
        node.RecalculateGenericRenderers();
    }

    public override void ApplyToTower(TowerModel towerModel)
    {
        base.ApplyToTower(towerModel);
        if (!towerModel.HasBehavior<AttackModel>())
        {
            return;
        }
        
        var atkModel = towerModel.GetAttackModel();
        if (!atkModel.HasBehavior<DisplayModel>())
        {
            atkModel.AddBehavior(new DisplayModel(atkModel.name + "_Display", new PrefabReference(""), towerModel.GetBehavior<DisplayModel>().layer, DisplayCategory.Tower));
        }

        atkModel.GetBehavior<DisplayModel>().ApplyDisplay<GigaPlasmaGunsDisplay>();
    }
    
    public class GigaPlasmaGunsDisplay : ModDisplay
    {
        public override string BaseDisplay => Game.instance.model.GetTower("SuperMonkey", 0, 3).GetAttackModel()
            .GetBehavior<DisplayModel>().display.AssetGUID;

        private static readonly int OutlineColor = Shader.PropertyToID("_OutlineColor");

        private void SetOutlineColor(Material mat, Color color)
        {
            mat.SetColor(OutlineColor, color);
            mat.SetShaderKeywords((Il2CppStringArray)System.Array.Empty<string>());
        }

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            node.transform.GetChild(0).localPosition = new(0, -1, -5);
            node.transform.GetChild(0).localRotation = Quaternion.Euler(3.8f, 0, 0);
            node.transform.GetChild(0).localScale = new(1, 1, 1);
            node.transform.GetChild(0).GetChild(0).GetChild(3).gameObject.active = true;
            
            node.genericRenderers = node.genericRenderers.AddTo(node.transform.GetChild(0).GetChild(0).GetChild(3).GetComponent<SkinnedMeshRenderer>());
            node.genericRendererLayers = new Il2CppStructArray<int>(2);
            node.RecalculateGenericRenderers();
            
            var renderer = node.GetMeshRenderer();
            renderer.material.mainTexture = GetTexture($"{Name}-0");
            renderer.materials[1].mainTexture = GetTexture($"{Name}-1");
            SetOutlineColor(renderer.material, new Color32(76, 61, 0, 255));
            SetOutlineColor(renderer.materials[1], new Color32(76, 61, 0, 255));
            renderer = node.GetMeshRenderer(1);
            renderer.material.mainTexture = GetTexture($"{Name}-0");
            renderer.materials[1].mainTexture = GetTexture($"{Name}-1");
            SetOutlineColor(renderer.material, new Color32(76, 61, 0, 255));
            SetOutlineColor(renderer.materials[1], new Color32(76, 61, 0, 255));
        }
    }
}

public class MonkeyOfLightDisplay : ModTowerDisplay<MonkeyOfLight>
{
    public override string BaseDisplay => GetDisplay(TowerType.DartMonkey);

    public override float Scale => 1;

    public override bool UseForTower(params int[] tiers)
    {
        return tiers.Max() < 3;
    }

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        for (var i = 0; i < node.GetMeshRenderers().Count; i++)
        {
            node.GetMeshRenderer(i).SetMainTexture(GetTexture($"{Name}-{i}"));
            node.GetMeshRenderer(i).SetOutlineColor(new Color32(220, 220, 129, 255));
        }

        node.RemoveBone("NewMonkeyRigDart:DartCtl");
    }
}

public class CyberMonkeyDisplay : ModTowerDisplay<CyberMonkey>
{
    public override string BaseDisplay => GetDisplay(TowerType.DartMonkey);

    public override float Scale => 1;

    public override bool UseForTower(params int[] tiers)
    {
        return tiers.Max() < 3;
    }

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        node.RemoveBone("NewMonkeyRigDart:DartCtl");

        foreach (var rend in node.GetMeshRenderers())
        {
            rend.material = Main.GetMaterial("HologramMat");
        }
    }
}
