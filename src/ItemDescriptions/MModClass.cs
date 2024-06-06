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
    public class MModClass : ClientMod
    {
        public static Dictionary<string, string> Descriptions = new Dictionary<string, string>
        {
            //ws - from workshop
            //dz - by dzhake



            //2elemerald

            //*ai
            {"2elemerald/ai/AIDuck","Runs to players, and kills on touch"},

            //*equipment
            //**hats
            {"2elemerald/equipment/hats/Ghasly Gibus","Accuracy is extremely lowered, just like a real Ghastly Gibus player"}, //ws
            {"2elemerald/equipment/hats/Mr.Bronze","Gives you lazer weapon"}, //dz
            {"2elemerald/equipment/hats/Mr.Silver","Gives you shotgun weapon"}, //dz
            {"2elemerald/equipment/hats/Mr.Gold","Gives you AK-like weapon"}, //dz
            {"2elemerald/equipment/hats/Mr.Platinum","Gives you rocket launcher-like weapon"}, //dz
            {"2elemerald/equipment/hats/MU mask","When equipped you become faster"}, //dz

            //**misc
            {"2elemerald/equipment/misc/Bubble Shield","You bounce a lot. Basically it's just annoying"}, //ws
            {"2elemerald/equipment/misc/Glitch Shield","Allows for infinite flight in any direction"}, //ws
            {"2elemerald/equipment/misc/Gravity Shield","Pressing up or down increases or decreases gravity. Jump to reset to normal"}, //ws
            {"2elemerald/equipment/misc/Starman","You aren't invincible, you can't use weapons, but touching anyone kills them instantly"}, //ws
            {"2elemerald/equipment/misc/Thwomp Shield","It's the Starman but bouncy and friccin' stupid"}, //ws
            
            {"2elemerald/equipment/Air Dash","Press jump in the air to dash in any direction, up to two times before you hit the ground"}, //ws
            {"2elemerald/equipment/Black Hole Armor","Any bullets spawned on the map are destroyed except for your own. Only melee items or special projectiles can kill you"}, //ws
            {"2elemerald/equipment/Maximum Overdrive","Initial speed is lower, but builds up as you run. Stopping loses all your built up speed"}, //ws
            {"2elemerald/equipment/MU Chestplate","When equipped you become faster and are equipped with the MU Sword"}, //ws
            {"2elemerald/equipment/Slider Boots","When equipped you get high horizontal speed when you start sliding"}, //dz

            //*explosives

            {"2elemerald/explosives/Bomb","Blows up blocks in a small radius"}, //ws
            {"2elemerald/explosives/Grenade Grenade","Explodes into grenades"}, //ws
            {"2elemerald/explosives/Grenade Grenade Grenade","Explodes into grenade grenades"}, //ws
            {"2elemerald/explosives/Mic Grenade","Stuns ducks for a while"}, //ws
            {"2elemerald/explosives/Rocket Grenade","Looks like a grenade, but when the pin is pulled you better run"}, //ws

            //*melee
            {"2elemerald/melee/MU Sword","Retextured Sword"}, //dz

            //*misc
            {"2elemerald/Misc/1UP","Runs right, flips direction when collides with wall, shows \"1UP\" Text when touches duck"}, //dz

            //*props
            {"2elemerald/props/Big Crate","Woah that's pretty big"}, //dz
            {"2elemerald/props/Bigger Crate","*notices ur bigger crate* OwO whats this?"}, //dz
            {"2elemerald/props/Trash Can","Runs right, flips direction when collides with wall, shows \"1UP\" Text when touches duck"}, //dz



            {"Equipment/Jetpack","it works for vanilla only :|"}, //dz

        };



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
        }
        
        /// <summary>
        /// Called by Duck Game after all mods have been loaded.
        /// </summary>
        protected override void OnPostInitialize()
        {
            MAutoPatchHandler.Patch();

            DevConsole.Log("Dzhake is here :)", Color.Green);

#if REBUILT
            DevConsole.Log("You are using rebuilt rn", Color.Green);
#else
            DevConsole.Log("You are using vanilla rn", Color.Green);
#endif
        }

        public static Editor editor;

        public static bool debug = true;

        [MAutoPatch(typeof(EditorGroupMenu),"InitializeGroups",MPatchType.Postfix)]
        public static void PatchPost_EditorGroupMenu_InitializeGroups(EditorGroupMenu __instance)
        {
            editor = Level.current as Editor;

            try
            {
                foreach (ContextMenu menu in (List<ContextMenu>)typeof(EditorGroupMenu).GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(__instance))
                {
                    if (menu.text != "" && !menu.root)
                    {
                        string path = menu.text;
                        ContextMenu menu1 = (ContextMenu)typeof(ContextMenu).GetField("_owner", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(menu);
                        path += "/" + menu1.text;
                        while (menu1.root == false)
                        {
                            menu1 = typeof(ContextMenu).GetField("_owner", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(menu1) as ContextMenu;
                            if (menu1.text != null && menu1.text != "")
                            {
                                path += "/" + menu1.text;
                            }
                        }

                        path = ReversStringBySymbols(path, '/');

                        if (menu.tooltip != null)
                        {
                            if (Descriptions.TryGetValue(path, out string description))
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
