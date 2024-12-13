using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DuckGame;
using ItemDescriptions.PatchSystem;

namespace ItemDescriptions
{
    
    /// <summary>
    /// The very core class, the actual mod class. Handles some internal functionality and basic calls to load the rest of the mod.
    /// </summary>
    public class Core : ClientMod
    {

        /// <summary>
        /// The duck-game generated mod config. This holds useful information and settings about the mod.
        /// </summary>
        public static ModConfiguration Config;
        
        public static string ReplaceData
        {
            get
            {
                var result = !Config.isWorkshop ? "LOCAL" : SteamIdField.GetValue(Config, new object[0]).ToString();
                return result;
            }
        }

        public static bool Disabled
        {
            get => (bool) DisabledField.GetValue(Config, new object[0]);
            set => DisabledField.SetValue(Config, value, new object[0]);
        }

        private static readonly PropertyInfo SteamIdField =
            typeof(ModConfiguration).GetProperty("workshopID", BindingFlags.Instance | BindingFlags.NonPublic);

        private static readonly PropertyInfo DisabledField =
            typeof(ModConfiguration).GetProperty("disabled", BindingFlags.Instance | BindingFlags.NonPublic);

        /// <summary>
        /// Called by Duck Game while this mod is being loaded.
        /// </summary>
        protected override void OnPreInitialize()
        {
            Config = configuration;
            DependencyResolver.ResolveDependencies();
        }
        
        /// <summary>
        /// Called by Duck Game after all mods have been loaded.
        /// </summary>
        protected override void OnPostInitialize()
        {
            AutoPatchHandler.Patch();
        }

        public static FieldInfo _itemsInfo = typeof(EditorGroupMenu).GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance);
        public static FieldInfo _ownerInfo = typeof(ContextMenu).GetField("_owner", BindingFlags.NonPublic | BindingFlags.Instance);

        public static Editor editor;

        public static bool debug = false;

        [AutoPatch(typeof(EditorGroupMenu),"InitializeGroups",MPatchType.Postfix)]
        public static void PatchPost_EditorGroupMenu_InitializeGroups(EditorGroupMenu __instance)
        {
            editor = Level.current as Editor;

            try
            {
                foreach (ContextMenu menu in (List<ContextMenu>)_itemsInfo.GetValue(__instance))
                {
                    if (menu.text != "" && !menu.root)
                    {
                        string path = menu.text;
                        ContextMenu menu1 = (ContextMenu)_ownerInfo.GetValue(menu);
                        path += "/" + menu1.text;
                        while (menu1.root == false)
                        {
                            menu1 = _ownerInfo.GetValue(menu1) as ContextMenu;
                            if (!string.IsNullOrEmpty(menu1.text))
                            {
                                path += "/" + menu1.text;
                            }
                        }

                        path = ReversStringBySymbols(path, '/');

                        if (menu.tooltip == null) continue;

                        if (Description.Descriptions.TryGetValue(path, out string description))
                        {
                            menu.tooltip = "|GREEN|[ID]|WHITE| " + description;//description;
                            DevConsole.Log(path + " : " + description, Color.Yellow);
                        }

                        if (debug)
                        {
                            menu.tooltip = path + " |YELLOW|Orig: " + menu.tooltip; // allows to detect path
                        }

                    }
                }
            }
            catch (Exception e)
            {
                DevConsole.Log("|RED|" + e);
            }
        }


        public static string ReversStringBySymbols(string text, char symbol)
        {
            string result = "";

            string[] words = text.Split(symbol);

            foreach (string word in words.Reverse())
            {
                result += word + symbol;
            }

            result = result.Substring(0, result.Length - 1);

            return result;
        }
        
    }
}
