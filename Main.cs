using System;
using System.Linq;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Internal;
using BTD_Mod_Helper.Extensions;
using DarksTowers;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppInterop.Runtime;
using MelonLoader;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using Main = DarksTowers.Main;
using Object = UnityEngine.Object;

[assembly: MelonInfo(typeof(Main), ModHelperData.Name, ModHelperData.Version, ModHelperData.Author)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace DarksTowers;

public class Main : BloonsTD6Mod
{
    internal const bool ShouldPrintBundleInformation = false;

    public override void OnApplicationStart()
    {
        var urp = GraphicsSettings.currentRenderPipeline.TryCast<UniversalRenderPipelineAsset>();
        if (urp == null) return;
        urp.m_RequireOpaqueTexture = true;
    }

    public override void OnTitleScreen()
    {
        if (ShouldPrintBundleInformation) PrintBundleInformation();
    }

    public override void OnMainMenu()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainMenuWorld"));
        Instantiate("PlasmaSingularity", SceneManager.GetActiveScene().GetRootGameObjects()[0]!.transform,
            new Vector3(-85.4444f, -658.7502f, 65.3885f), Quaternion.Euler(14, 90, 0), true).transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }

    public class CustomTowerBehavior : TowerBehavior
    {
        
    }
    
    public class CustomTowerBehaviorModel : TowerBehaviorModel
    {


        public CustomTowerBehaviorModel(string name) : base(name)
        {
            implementationType = Il2CppSystem.Type.GetType(nameof(CustomTowerBehavior));
        }

        public CustomTowerBehaviorModel(IntPtr pointer) : base(pointer) 
        {
            implementationType = Il2CppSystem.Type.GetType(nameof(CustomTowerBehavior));
        }


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
        foreach (var bundleKey in ResourceHandler.Bundles.Keys)
        {
            var bundle = ResourceHandler.Bundles[bundleKey];

            GetInstance().LoggerInstance.Msg($"--------------------{bundle.name}/{bundleKey}--------------------");
            foreach (var asset in bundle.LoadAllAssetsAsync<Object>().allAssets)
                GetInstance().LoggerInstance.Msg($"{asset.name} is {asset.TypeName()}");
        }
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
    public static GameObject Instantiate(string name, Transform parent, Vector3 position, Quaternion rotation, bool instantiateInWorldSpace = false)
    {
        if (instantiateInWorldSpace)
        {
            var obj = Object.Instantiate(GetObject(name), position, rotation);
            obj.transform.SetParent(parent, true);
            return obj;
        }
        return Object.Instantiate(GetObject(name), position, rotation, parent);
    }

    public static T GetFromBundle<T>(string name) where T : Object
    {
        return ResourceHandler.Bundles[GetInstance().IDPrefix + "darkstowers"].LoadAllAssetsAsync<T>().allAssets
            .First(t => t.name == name).Cast<T>();
    }
}