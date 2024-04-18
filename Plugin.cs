using System;
using BepInEx;
using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;
namespace paycheckCompany;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);

    private void Awake()
    {
        // Plugin load logic goes here!
        // This script acts like a unity object.
        Logger.LogInfo($"PaycheckToPaycheckCompany Active");
        harmony.PatchAll(typeof(paycheckCompany));
    }

    //This defines the paycheckCompany class to run on the TimeOfDay object on the Awake function
    [HarmonyPatch(typeof(TimeOfDay), "Awake")]
    
    class paycheckCompany
    {
        //This is a postfix - it runs AFTER the normal awake function for all TimeOfDay objects
        //Something to note is that there is only one TimeOfDay object - it's not like an enemy where there's many
        private static void Postfix(ref TimeOfDay __instance)
        {
          if(GameNetworkManager.Instance.isHostingGame){ // Only runs if the user running the mod is the host
            //Modifies a variety of relevant variables within the TimeOfDay instance
            __instance.quotaVariables.startingCredits = 30;
            __instance.quotaVariables.startingQuota = 100;
            __instance.quotaVariables.deadlineDaysAmount = 2;
            __instance.quotaVariables.increaseSteepness = 5f;
            __instance.quotaVariables.baseIncrease = 100;
          }
        }  
    }
}