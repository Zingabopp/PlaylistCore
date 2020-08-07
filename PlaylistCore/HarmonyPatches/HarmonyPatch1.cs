using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BeatSaberPlaylistsLib.Types;
using HarmonyLib;
using UnityEngine.UI;

/// <summary>
/// See https://github.com/pardeike/Harmony/wiki for a full reference on Harmony.
/// </summary>
namespace PlaylistCore.HarmonyPatches
{
    /// <summary>
    /// This patches ClassToPatch.MethodToPatch(Parameter1Type arg1, Parameter2Type arg2)
    /// </summary>
    [HarmonyPatch(typeof(AnnotatedBeatmapLevelCollectionTableCell), nameof(AnnotatedBeatmapLevelCollectionTableCell.SetData),
        new Type[] { // List the Types of the method's parameters.
        typeof(IAnnotatedBeatmapLevelCollection)})]
    public class HarmonyPatch1
    {
        /// <summary>
        /// This code is run before the original code in MethodToPatch is run.
        /// </summary>
        /// <param name="__instance">The instance of ClassToPatch</param>
        /// <param name="arg1">The Parameter1Type arg1 that was passed to MethodToPatch</param>
        /// <param name="____privateFieldInClassToPatch">Reference to the private field in ClassToPatch named '_privateFieldInClassToPatch', 
        ///     added three _ to the beginning to reference it in the patch. Adding ref means we can change it.</param>
        static void Postfix(AnnotatedBeatmapLevelCollectionTableCell __instance, ref IAnnotatedBeatmapLevelCollection annotatedBeatmapLevelCollection, ref Image ____coverImage)
        {
            var col = annotatedBeatmapLevelCollection;
            AnnotatedBeatmapLevelCollectionTableCell cell = __instance;
            if (annotatedBeatmapLevelCollection is IDeferredSpriteLoad deferredSpriteLoad)
            {
                var image = ____coverImage;
                EventHandler action = (s, e) =>
                {
                    if (deferredSpriteLoad == s)
                    {
                        Plugin.Log.Info($"Updating image for {col.collectionName}");
                        image.sprite = deferredSpriteLoad.Sprite;
                    }
                    else
                    {
                        if (s is IAnnotatedBeatmapLevelCollection bmc)
                            Plugin.Log.Warn($"Types don't match for deferred sprite load. {bmc.collectionName} != {col.collectionName}");
                        else
                            Plugin.Log.Warn($"Wrong sender type for deferred sprite load: {s?.GetType().Name ?? "<NULL>"}");
                    }
                };
                deferredSpriteLoad.SpriteLoaded -= action;
                deferredSpriteLoad.SpriteLoaded += action;
            }
        }
    }
}