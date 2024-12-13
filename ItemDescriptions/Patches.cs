using DuckGame;
using ItemDescriptions.PatchSystem;

namespace ItemDescriptions;

public class Patches
{

    [AutoPatch(typeof(Level), "PostDrawLayer", MPatchType.Postfix)]
    public static void PostDrawLayer(Layer layer)
    {
        if (!Core.inGamePreview || layer != Layer.Foreground) return;

        foreach (Profile profile in Profiles.active)
        {
            if ((Network.isActive && !Network.isFakeActive && profile.connection != DuckNetwork.localConnection)
                || profile.duck?.holdObject == null) continue;

            Holdable heldObject = profile.duck.holdObject;

            if (!Description.Items.TryGetValue(heldObject.editorName, out string desc)) return;

            string[] lines = desc.SplitByLength(40);
            Vec2 vec = heldObject.position + new Vec2(28, -32);

            int i = 0;
            foreach (string s in lines)
            {
                Graphics.DrawString(s, vec + new Vec2(3, i * 6 + 3), Color.White, 1, null, 0.5f);
                    
                i++;
            }
            Graphics.DrawRect(vec + new Vec2(2), vec + new Vec2(164, i * 6 + 2), Color.Black, 0.99f);
            Graphics.DrawRect(vec, vec + new Vec2(166, i * 6 + 4), Color.White, 0.98f);
        }
    }
}
