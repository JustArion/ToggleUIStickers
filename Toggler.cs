namespace Dawn.Stickers
{
    extern alias MelonLoader3;
    using System.Collections.Generic;
    using System.Linq;
    using MelonLoader3::MelonLoader;
    using UnityEngine;
    using UnityEngine.UI;
    using Utilities;

    internal static class Toggler
    {
        private static void ToggleMessage(string array, int count, bool hide) => MelonLogger.Msg(hide ? $"Hid {count} '{array}' Stickers" : $"Showed {count} '{array}' Stickers");
        internal static void RegisterStateListeners()
        {
            VRCPlusStateListener = new PreferencesStateListener(Start.UseVRCPlusStickers, () => {
                foreach (var Sticker in VRCPlusStickers)
                {
                    //Other mods might destroy the obj completely so we remove it.
                    if (Sticker == null) { cachedVRCPlusStickers = cachedVRCPlusStickers.Where(item => item != Sticker).ToArray(); continue; }

                    Sticker.enabled = true;
                }
                ToggleMessage("VRC+", VRCPlusStickers.Count(), false);
            }, () => { foreach (var Sticker in VRCPlusStickers)
                {
                    if (Sticker == null) { cachedVRCPlusStickers = cachedVRCPlusStickers.Where(item => item != Sticker).ToArray(); continue; }

                    Sticker.enabled = false;
                }
                ToggleMessage("VRC+", VRCPlusStickers.Count(), true);
            });
            BuildInfoStateListener = new PreferencesStateListener(Start.UseBuildInfoStickers, () => {
                foreach (var Sticker in BuildInfoStickers)
                {
                    if (Sticker == null) { cachedBuildInfoStickers = cachedBuildInfoStickers.Where(item => item != Sticker).ToArray(); continue; }
                    
                    var y = Sticker.GetComponent<Text>();
                    if (y != null) y.enabled = true;
                }
                ToggleMessage("BuildInfo", BuildInfoStickers.Count(), false);
            }, () => {
                foreach (var x in BuildInfoStickers)
                {
                    if (x == null) { cachedBuildInfoStickers = cachedBuildInfoStickers.Where(item => item != x).ToArray(); continue; }
                    
                    var y = x.GetComponent<Text>();
                    if (y != null) y.enabled = false;
                }
                ToggleMessage("BuildInfo", BuildInfoStickers.Count(), true);
            });
            EarlyAccessStateListener = new PreferencesStateListener(Start.UseEarlyAccessStickers, () => {
                foreach (var Sticker in EarlyAccessStickers)
                {
                    if (Sticker == null) { cachedEarlyAccessStickers = cachedEarlyAccessStickers.Where(item => item != Sticker).ToArray(); continue; }
                    
                    var y = Sticker.GetComponent<Text>();
                    if (y != null)
                    {
                        y.enabled = true;
                        continue;
                    }
                    var z = Sticker.GetComponent<Image>();
                    if (z != null)
                    {
                        z.enabled = true;
                    }
                }
                ToggleMessage("EarlyAccess", EarlyAccessStickers.Count(), false);
            }, () => {
                foreach (var Sticker in EarlyAccessStickers)
                {
                    if (Sticker == null) { cachedEarlyAccessStickers = cachedEarlyAccessStickers.Where(item => item != Sticker).ToArray(); continue; }
                    
                    var y = Sticker.GetComponent<Text>();
                    if (y != null)
                    {
                        y.enabled = false;
                        continue;
                    }
                    var z = Sticker.GetComponent<Image>();
                    if (z != null)
                    {
                        z.enabled = false;
                    }
                }
                ToggleMessage("EarlyAccess", EarlyAccessStickers.Count(), true);
            });
            NewStateListener = new PreferencesStateListener(Start.UseNewStickers, () => {
                foreach (var Sticker in NewStickers)
                {
                    if (Sticker == null) { cachedNewStickers = cachedNewStickers.Where(item => item != Sticker).ToArray(); continue; }

                    Sticker.enabled = true;
                }
                ToggleMessage("New", NewStickers.Count(), false);
            }, () => {
                foreach (var Sticker in NewStickers)
                {
                    if (Sticker == null) { cachedNewStickers = cachedNewStickers.Where(item => item != Sticker).ToArray(); continue; }

                    Sticker.enabled = false;
                }
                ToggleMessage("New", NewStickers.Count(), true);
            });
        }
        
        internal static void UpdateListeners()
        {
            VRCPlusStateListener.Update(Start.UseVRCPlusStickers);
            BuildInfoStateListener.Update(Start.UseBuildInfoStickers);
            EarlyAccessStateListener.Update(Start.UseEarlyAccessStickers);
            NewStateListener.Update(Start.UseNewStickers);
        }
        internal static void ForceUpdateListeners()
        {
            VRCPlusStateListener.ForceUpdate(Start.UseVRCPlusStickers);
            BuildInfoStateListener.ForceUpdate(Start.UseBuildInfoStickers);
            EarlyAccessStateListener.ForceUpdate(Start.UseEarlyAccessStickers);
            NewStateListener.ForceUpdate(Start.UseNewStickers);
        }

        private static PreferencesStateListener VRCPlusStateListener;
        private static PreferencesStateListener BuildInfoStateListener;
        private static PreferencesStateListener EarlyAccessStateListener;
        private static PreferencesStateListener NewStateListener;
        

        // static GameObject[] cachedVRCPlusStickers;
        // static IEnumerable<GameObject> VRCPlusStickers=> cachedVRCPlusStickers ??= Resources.FindObjectsOfTypeAll<GameObject>().Where(o => o.name is "VRC+" or "Icon_VRC+").ToArray();
        static Image[] cachedVRCPlusStickers;
        static IEnumerable<Image> VRCPlusStickers=> cachedVRCPlusStickers ??= Resources.FindObjectsOfTypeAll<Image>().Where(o => o.name is "VRC+" or "Icon_VRC+").ToArray();
        
        static GameObject[] cachedBuildInfoStickers;
        static IEnumerable<GameObject> BuildInfoStickers
        {
            get
            {
                if (cachedBuildInfoStickers != null) return cachedBuildInfoStickers;
                var x = new List<GameObject>();
                var QMBuildNumber = GameObject.Find("UserInterface/QuickMenu/QuickMenu_NewElements/_InfoBar/BuildNumText");
                var MainMenuVersionText = GameObject.Find("UserInterface/MenuContent/Screens").transform.Find("Settings/TitlePanel/VersionText").gameObject;
                    
                if (QMBuildNumber != null) x.Add(QMBuildNumber);
                if (MainMenuVersionText != null) x.Add(MainMenuVersionText);
                return cachedBuildInfoStickers = x.ToArray();
            }
        }

        static GameObject[] cachedEarlyAccessStickers;
        static IEnumerable<GameObject> EarlyAccessStickers
        {
            get
            {
                if (cachedEarlyAccessStickers != null) return cachedEarlyAccessStickers;
                var x = new List<GameObject>();
                var QMEarlyAccess = GameObject.Find("UserInterface/QuickMenu/QuickMenu_NewElements/_InfoBar/EarlyAccessText");
                var MainMenuEarlyAccess = GameObject.Find("UserInterface/MenuContent/").transform.Find("Backdrop/Backdrop/Image").gameObject;
                
                if (QMEarlyAccess != null) x.Add(QMEarlyAccess);
                if (MainMenuEarlyAccess != null) x.Add(MainMenuEarlyAccess);
                return cachedEarlyAccessStickers = x.ToArray();
            }
        }
        static Image[] cachedNewStickers;
        private static IEnumerable<Image> NewStickers => cachedNewStickers ??= Resources.FindObjectsOfTypeAll<Image>()
            .Where(o => o.name is "Image_NEW").ToArray();


    }
}