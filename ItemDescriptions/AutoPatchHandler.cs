using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Harmony;
using DuckGame;

namespace ItemDescriptions.PatchSystem {
    /// <summary>
    /// The handler that does the patching of AutoPatch attributes.
    /// </summary>
    public static class AutoPatchHandler
    {
        /// <summary>
        /// The patching method that finds and executes Auto-Patches.
        /// </summary>
        public static void Patch()
        {

            var harmony = HarmonyLoader.Loader.harmonyInstance;

            foreach (var origInfo in GetAllAutoPatches())
            {
                if (!origInfo.IsStatic)
                {
                    DevConsole.Log("Skipping non-static patch method! (MAutoPatch " + origInfo.Name + " in " + origInfo.DeclaringType?.Name + ")", Color.Orange);
                    continue;
                }

                List<AutoPatchAttribute> attributes = origInfo.GetCustomAttributes(typeof(AutoPatchAttribute), false).Cast<AutoPatchAttribute>().ToList();

                foreach (var attribute in attributes)
                {
                    MethodBase mPatch;

                    if (attribute.Method is ".ctor" or "")
                        mPatch = AccessTools.DeclaredConstructor(attribute.Type, attribute.Params);
                    else
                        mPatch = AccessTools.DeclaredMethod(attribute.Type, attribute.Method, attribute.Params);

                    if (mPatch is null)
                    {
                        DevConsole.Log("Failed to find specified method: " + attribute.Method + ". on type of: " + attribute.Type.Name, Color.Red);
                        continue;
                    }

                    switch (attribute.PatchType)
                    {
                        case MPatchType.Prefix:
                            harmony.Patch(mPatch, new HarmonyMethod(origInfo));
                            break;
                        case MPatchType.Postfix:
                            harmony.Patch(mPatch, null, new HarmonyMethod(origInfo));
                            break;
                        case MPatchType.Transpiler:
                            harmony.Patch(mPatch, null, null, new HarmonyMethod(origInfo));
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }


        }

        private static IEnumerable<MethodInfo> GetAllAutoPatches()
        {
            return Assembly.GetExecutingAssembly().GetTypes().SelectMany(x => x.GetMethods()).Where(x =>
                x.GetCustomAttributes(typeof(AutoPatchAttribute), false).FirstOrDefault() != null);
        }
    }
}