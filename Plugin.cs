using BepInEx;
using BepInEx.IL2CPP;
using UnityEngine;
using HarmonyLib;

namespace NMSH
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BasePlugin
    {
        public override void Load()
        {
            Harmony.CreateAndPatchAll(typeof(Plugin));

            Log.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }

        [HarmonyPatch(typeof(Shake), nameof(Shake.Awake))]
        [HarmonyPostfix]
        public static void RemoveMoreShake()
        {
            GameObject.Destroy(GameObject.Find("Camera/Recoil/Shake").GetComponent<MilkShake.Shaker>());
        }

        [HarmonyPatch(typeof(CamBob), nameof(CamBob.BobOnce))]
        [HarmonyPatch(typeof(Recoil), nameof(Recoil.AddRecoil))]
        [HarmonyPrefix]
        public static bool NoMoreShaking()
        {
            return false;
        }
    }

}