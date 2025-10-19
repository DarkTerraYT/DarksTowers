using System.Linq;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Internal;
using BTD_Mod_Helper.Extensions;
using DarksTowers;
using Il2CppAssets.Scripts.Simulation.Towers;
using MelonLoader;
using UnityEngine;
using Main = DarksTowers.Main;

[assembly: MelonInfo(typeof(Main), ModHelperData.Name, ModHelperData.Version, ModHelperData.Author)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace DarksTowers;

public class Main : BloonsTD6Mod
{
    internal const bool ShouldPrintBundleInformation = true;

    public override void OnTitleScreen()
    {
        if (ShouldPrintBundleInformation) PrintBundleInformation();
    }

    public static Main GetInstance()
    {
        return ModHelper.GetMod<Main>();
    }

    public override void OnTowerSelected(Tower tower)
    {
        foreach (var renderer in tower.GetUnityDisplayNode().GetMeshRenderers())
        foreach (var mat in renderer.materials)
            if (mat.HasInt("_Selected"))
                mat.SetInt("_Selected", 1);
    }

    public override void OnTowerDeselected(Tower tower)
    {
        try
        {
            if (tower.GetUnityDisplayNode() == null) return;
            foreach (var renderer in tower.GetUnityDisplayNode().GetMeshRenderers())
            foreach (var mat in renderer.materials)
                if (mat.HasInt("_Selected"))
                    mat.SetInt("_Selected", 0);
        }
        catch
        {
        }
    }

    public static void PrintBundleInformation()
    {
        /*foreach (var bundleKey in ResourceHandler.Bundles.Keys)
        {
            var bundle = ResourceHandler.Bundles[bundleKey];

            GetInstance().LoggerInstance.Msg($"--------------------{bundle.name}/{bundleKey}--------------------");
            foreach (var asset in bundle.LoadAllAssetsAsync<Object>().allAssets)
                GetInstance().LoggerInstance.Msg($"{asset.name} is {asset.TypeName()}");
        }*/
    }

    public static Shader GetShader(string name)
    {
        return GetFromBundle<Shader>(name);
    }

    public static Material GetMaterial(string name)
    {
        return GetFromBundle<Material>(name);
    }

    public static GameObject GetObject(string name)
    {
        return GetFromBundle<GameObject>(name);
    }

    public static T GetFromBundle<T>(string name) where T : Object
    {
        return ResourceHandler.Bundles[GetInstance().IDPrefix + "darkstowers"].LoadAllAssetsAsync<T>().allAssets
            .First(t => t.name == name).Cast<T>();
    }
}