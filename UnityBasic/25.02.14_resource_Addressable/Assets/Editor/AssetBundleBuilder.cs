using System.IO;
using UnityEditor;
using UnityEngine;

public class AssetBundleBuilder
{
    [MenuItem("Asset Bundle/build")]
    public static void AssetBundleBuild()
    {
        string directory = "./Bundle";

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        BuildPipeline.BuildAssetBundles(directory, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);

        EditorUtility.DisplayDialog("Asset Bundle Build", "Asset Bundle build Success!!", "Complete");
    }
}