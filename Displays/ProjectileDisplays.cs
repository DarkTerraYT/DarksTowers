using System;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppAssets.Scripts.Unity.Display.Animation;
using MelonLoader;
using UnityEngine;
using Vector3 = Il2CppAssets.Scripts.Simulation.SMath.Vector3;

namespace DarksTowers.Displays;

[RegisterTypeInIl2Cpp]
internal class TrailHelper : MonoBehaviour
{
    private bool created;
    private TrailRenderer trailRenderer;

    private void Awake()
    {
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Start()
    {
        created = true;
        if (trailRenderer) trailRenderer.Clear();
    }

    private void OnEnable()
    {
        if (!trailRenderer) trailRenderer = gameObject.AddComponent<TrailRenderer>();

        if (created) trailRenderer.Clear();
    }

    private void OnDisable()
    {
        Console.WriteLine("Disabled Trail");
        if (trailRenderer) trailRenderer.Clear();
    }
}

public class Plasma : ModBloonOverlay
{
    private static Material plasma2dMaterial;
    
    public override string BaseOverlay => Game.instance.model.GetTowerFromId("MortarMonkey-003")
        .GetDescendant<AddBehaviorToBloonModel>().overlayType;

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        var spriteAnimator = node.GetComponentInChildren<CustomSpriteFrameAnimator>();
        if (spriteAnimator)
        {
            if (!plasma2dMaterial)
            {
                plasma2dMaterial = spriteAnimator.spriteRenderer.sharedMaterial.Duplicate();
                plasma2dMaterial.color = new Color(1, 0, 4);
                plasma2dMaterial.name = "Plasma2DMaterial";
            }

            spriteAnimator.spriteRenderer.material = plasma2dMaterial;
            return;
        }
        
        foreach (var renderer in node.GetMeshRenderers())
        {
            renderer.SetMainTexture(GetTexture("Plasma"));
            //renderer.material.mainTexture.TrySaveToPNG(Path.Combine(Application.persistentDataPath, node.name + node.genericRenderers.IndexOf(renderer) + ".png"));
        }
        
        //node.genericRenderers[0].material.mainTexture = GetTexture("Plasma");
    }
}

public class PlasmaDart : ModDisplay2D
{
    protected override string TextureName => "PlasmaDart";
}

public class HotPlasmaDart : ModDisplay2D
{
    protected override string TextureName => "HotPlasmaDart";
}

public class FieryPlasmaDart : ModDisplay2D
{
    protected override string TextureName => "FieryPlasmaDart";
}

public class PlasmaBall : ModDisplay2D
{
    protected override string TextureName => "PlasmaBall";
}

public class FieryPlasmaBall : ModDisplay2D
{
    protected override string TextureName => "FieryPlasmaBall";
}

/*public class SolarPlasmaBall : ModDisplay2D
{
    public override Vector3 PositionOffset => new Vector3(0, 0, 5);
    protected override string TextureName => "SolarPlasmaBall";
}*/
public class SolarPlasmaBall : ModCustomDisplay
{
    public override string AssetBundleName => "darkstowers";
    public override string PrefabName => "PlasmaBallFixed";

    public override float Scale => 1;
}

public class PlasmaBeam : ModDisplay2D
{
    protected override string TextureName => "PlasmaBeam";
}
public class SolarPlasmaBeam : ModDisplay2D
{
    protected override string TextureName => "SolarPlasmaBeam";
}

public class CyberBeam : ModDisplay2D
{
    protected override string TextureName => "CyberBeam";
}

public class BoltOfLight : ModDisplay2D
{
    protected override string TextureName => "BoltOfLight";

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        base.ModifyDisplayNode(node);
        var trail = Main.GetObject("LightTrail").Duplicate();
        trail.transform.SetParent(node.transform);
        node.gameObject.AddComponent<TrailHelper>();
    }
}