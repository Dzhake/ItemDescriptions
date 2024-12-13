using System.Collections.Generic;

namespace ItemDescriptions
{
    public class Description
    {
        public static Dictionary<string, string> Descriptions = new()
        {
            //ws - from workshop
            //dz - by dzhake


            //2emerald

            //*ai
            {"2emerald/ai/AiDuck","Runs to players, and kills on touch"},

            //*equipment
            //**hats
            {"2emerald/equipment/hats/Ghastly Gibus","Accuracy is extremely lowered, just like a real Ghastly Gibus player"}, //ws
            {"2emerald/equipment/hats/Mr.Bronze","Gives you lazer weapon"}, //dz
            {"2emerald/equipment/hats/Mr.Silver","Gives you shotgun weapon"}, //dz
            {"2emerald/equipment/hats/Mr.Gold","Gives you AK-like weapon"}, //dz
            {"2emerald/equipment/hats/Mr.Platinum","Gives you rocket launcher-like weapon"}, //dz
            {"2emerald/equipment/hats/MU mask","When equipped you become faster"}, //dz

            //**misc
            {"2emerald/equipment/misc/Bubble Shield","You bounce a lot. Basically it's just annoying"}, //ws
            {"2emerald/equipment/misc/Glitch Shield","Allows for infinite flight in any direction"}, //ws
            {"2emerald/equipment/misc/Gravity Shield","Pressing up or down increases or decreases gravity. Jump to reset to normal"}, //ws
            {"2emerald/equipment/misc/Starman","You aren't invincible, you can't use weapons, but touching anyone kills them instantly"}, //ws
            {"2emerald/equipment/misc/Thwomp Shield","It's the Starman but bouncy and friccin' stupid"}, //ws
            
            {"2emerald/equipment/Air Dash","Press jump in the air to dash in any direction, up to two times before you hit the ground"}, //ws
            {"2emerald/equipment/Black Hole Armor","Any bullets spawned on the map are destroyed except for your own\n Only melee items or special projectiles can kill you"}, //ws
            {"2emerald/equipment/Maximum Overdrive","Initial speed is lower, but builds up as you run. Stopping loses all your built up speed"}, //ws
            {"2emerald/equipment/MU Chestplate","When equipped you become faster and are equipped with the MU Sword"}, //ws
            {"2emerald/equipment/Slider Boots","When equipped you get high horizontal speed when you start sliding"}, //dz

            //*explosives

            {"2emerald/explosives/Bomb","Blows up blocks in a small radius"}, //ws
            {"2emerald/explosives/Grenade Grenade","Explodes into grenades"}, //ws
            {"2emerald/explosives/Grenade Grenade Grenade","Explodes into grenade grenades"}, //ws
            {"2emerald/explosives/Mic Grenade","Stuns ducks for a while"}, //ws
            {"2emerald/explosives/Rocket Grenade","Looks like a grenade, but when the pin is pulled you better run"}, //ws

            //*melee
            {"2emerald/melee/MU Sword","Retextured Sword"}, //dz

            //*misc
            {"2emerald/Misc/1UP","Runs right, flips direction when collides with wall, shows \"1UP\" Text when touches duck"}, //dz

            //*props
            {"2emerald/props/Big Crate","Woah that's pretty big"}, //dz
            {"2emerald/props/Bigger Crate","*notices ur bigger crate* OwO whats this?"}, //dz
            {"2emerald/props/Trash Can","Runs right, flips direction when collides with wall, shows \"1UP\" Text when touches duck"}, //dz
        };
    }
}
