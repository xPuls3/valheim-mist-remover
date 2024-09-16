using System;
using System.Reflection;
using BepInEx;
using HarmonyLib;

namespace Puls3.Valheim {

    [BepInPlugin(PluginInfo.ID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin {

        public struct PluginInfo {

            public const string ID = "dev.puls3.valheim.mist-remover";

            public const string Name = "Mist Remover";
            public const string Author = "Puls3";

            public const string Version = "1.0.0.0";

        }
        
        private readonly Harmony harmony = new Harmony(PluginInfo.ID);

        public void Awake() {

            Assembly assembly = Assembly.GetExecutingAssembly();
            harmony.PatchAll(assembly);

        }

    }

    [HarmonyPatch(typeof(MistEmitter), nameof(MistEmitter.SetEmit))]
    public static class MistEmitterAwakePatch {

        public static void Prefix(MistEmitter __instance, ref bool emit) {

            try {
                UnityEngine.Object.DestroyImmediate(__instance.gameObject);
            } catch (Exception error) {
                // Ignored exception
            }

        }

    }

    [HarmonyPatch(typeof(ParticleMist), nameof(ParticleMist.Awake))]
    public static class ParticleMistAwakePatch {

        public static void Postfix(ParticleMist __instance) {

            try {
                if (__instance != null) UnityEngine.Object.DestroyImmediate(__instance.gameObject);
            } catch (Exception error) {
                // Ignored exception
            }

        }

    }

}