using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

// Requires the Addressables package (com.unity.addressables) to be installed,
// since it's used to gather scene paths from Addressable groups.
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;

/// <summary>
/// Editor utility to find and remove "Missing (Mono Script)" components
/// (e.g. the lftraceLightmaps warnings) across every scene the project
/// knows about — both scenes listed in Build Settings AND scenes registered
/// as Addressables (including ones inside Addressable folder entries).
///
/// Usage: Tools > Cleanup > Remove Missing Scripts In All Scenes
///
/// NOTE: This edits and SAVES every scene it processes. Make sure your
/// work is committed / backed up in version control before running it.
/// </summary>
public static class MissingScriptCleaner
{
    [MenuItem("Tools/Cleanup/Remove Missing Scripts In All Scenes")]
    public static void CleanAllScenes()
    {
        var scenePaths = GetAllKnownScenePaths();

        if (scenePaths.Count == 0)
        {
            Debug.LogWarning("[MissingScriptCleaner] No scenes found in Build Settings or Addressables. " +
                              "Nothing to clean.");
            return;
        }

        int totalRemoved = 0;
        int scenesChanged = 0;

        foreach (var path in scenePaths)
        {
            var scene = EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
            int removedInScene = CleanScene(scene);

            if (removedInScene > 0)
            {
                EditorSceneManager.MarkSceneDirty(scene);
                EditorSceneManager.SaveScene(scene);
                scenesChanged++;
                totalRemoved += removedInScene;
                Debug.Log($"[MissingScriptCleaner] {path}: removed {removedInScene} missing script component(s). Scene saved.");
            }
            else
            {
                Debug.Log($"[MissingScriptCleaner] {path}: clean, nothing to remove.");
            }
        }

        Debug.Log($"[MissingScriptCleaner] Done. {totalRemoved} missing component(s) removed across {scenesChanged} scene(s) " +
                  $"(checked {scenePaths.Count} total, from Build Settings + Addressables).");
    }

    [MenuItem("Tools/Cleanup/Remove Missing Scripts In Active Scene")]
    public static void CleanActiveScene()
    {
        var scene = SceneManager.GetActiveScene();
        int removed = CleanScene(scene);

        if (removed > 0)
        {
            EditorSceneManager.MarkSceneDirty(scene);
            Debug.Log($"[MissingScriptCleaner] Active scene: removed {removed} missing script component(s). " +
                      "Remember to save the scene (Ctrl+S).");
        }
        else
        {
            Debug.Log("[MissingScriptCleaner] Active scene: clean, nothing to remove.");
        }
    }

    /// <summary>
    /// Combines scene paths from Build Settings and from every Addressable
    /// group (including scenes nested inside folder entries), de-duplicated.
    /// </summary>
    private static List<string> GetAllKnownScenePaths()
    {
        var paths = new HashSet<string>();

        // 1) Build Settings scenes
        foreach (var s in EditorBuildSettings.scenes)
        {
            if (s.enabled)
                paths.Add(s.path);
        }

        // 2) Addressable scenes
        var settings = AddressableAssetSettingsDefaultObject.Settings;
        if (settings == null)
        {
            Debug.LogWarning("[MissingScriptCleaner] Addressables package found but no AddressableAssetSettings asset " +
                              "exists in the project — skipping Addressables scan.");
        }
        else
        {
            // GetAllAssets flattens folder entries (e.g. an Addressable group that
            // points at a whole folder of scenes) into individual entries for us.
            var allEntries = new List<AddressableAssetEntry>();
            settings.GetAllAssets(allEntries, false,
                groupFilter: null,
                entryFilter: entry => entry.AssetPath != null && entry.AssetPath.EndsWith(".unity"));

            foreach (var entry in allEntries)
                paths.Add(entry.AssetPath);
        }

        return paths.Where(p => !string.IsNullOrEmpty(p)).ToList();
    }

    private static int CleanScene(Scene scene)
    {
        int removedCount = 0;

        foreach (var root in scene.GetRootGameObjects())
        {
            // true = include inactive children too
            var allTransforms = root.GetComponentsInChildren<Transform>(true);
            foreach (var t in allTransforms)
            {
                int removedHere = GameObjectUtility.RemoveMonoBehavioursWithMissingScript(t.gameObject);
                if (removedHere > 0)
                {
                    Debug.Log($"[MissingScriptCleaner]   - '{t.gameObject.name}' in '{scene.name}': removed {removedHere}");
                    removedCount += removedHere;
                }
            }
        }

        return removedCount;
    }
}