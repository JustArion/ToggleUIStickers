extern alias MelonLoader3;
using System;
using Dawn.Stickers;
using MelonLoader3::MelonLoader;

[assembly: MelonInfo(typeof(Start), Start.MODID, "1.0.1",  "arion#1223", "https://github.com/Arion-Kun/ToggleUIStickers/releases/latest")]
[assembly: MelonColor(ConsoleColor.DarkCyan)]
[assembly: MelonGame("VRChat", "VRChat")]


namespace Dawn.Stickers
{
    using System.Collections;
    using UnityEngine;

    internal sealed class Start : MelonMod
    {
        internal const string MODID = "ToggleUIStickers";
        #region Preferences

        static MelonPreferences_Entry<bool> _UseVRCPlusStickers;
        internal static bool UseVRCPlusStickers => _UseVRCPlusStickers.Value;
        static MelonPreferences_Entry<bool> _UseBuildInfoStickers;
        internal static bool UseBuildInfoStickers => _UseBuildInfoStickers.Value;
        static MelonPreferences_Entry<bool> _UseEarlyAccessStickers;
        internal static bool UseEarlyAccessStickers => _UseEarlyAccessStickers.Value;
        static MelonPreferences_Entry<bool> _UseNewStickers;
        internal static bool UseNewStickers => _UseNewStickers.Value;
        #endregion
        public override void OnApplicationStart()
        {
            RegisterSettings();
            Toggler.RegisterStateListeners();
        }

        static void RegisterSettings()
        {
            var modCategory = MelonPreferences.CreateCategory(MODID, "UI Stickers");
            
            
            _UseVRCPlusStickers = modCategory.CreateEntry("VRCPlus", false, "VRC+") as MelonPreferences_Entry<bool>;
            _UseBuildInfoStickers = modCategory.CreateEntry("BuildInfo", false, "Build Info") as MelonPreferences_Entry<bool>;
            _UseEarlyAccessStickers = modCategory.CreateEntry("EarlyAccess", false, "Early Access") as MelonPreferences_Entry<bool>;
            _UseNewStickers = modCategory.CreateEntry("New", false, "New") as MelonPreferences_Entry<bool>;
        }

        //Thanks Bono! <3
        [Credit("DDAkebono", "https://github.com/ddakebono/BTKSAImmersiveHud/blob/0e1e169288abc45ecb5e92b143f90a657d921d16/BTKSAImmersiveHud.cs#L60")]
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (scenesLoaded is null or > 2) return;
            scenesLoaded++;
            if (scenesLoaded != 2) return;
            scenesLoaded = null;
            MelonCoroutines.Start(DelayUpdate()); // To allow other mods to effectively copy those objects in their initial states.
        }

        private static IEnumerator DelayUpdate() { yield return new WaitForSeconds(15f); Toggler.ForceUpdateListeners(); }
        static byte? scenesLoaded = 0;

        public override void OnPreferencesSaved() { if (scenesLoaded != null) return; Toggler.UpdateListeners(); }
    }
}