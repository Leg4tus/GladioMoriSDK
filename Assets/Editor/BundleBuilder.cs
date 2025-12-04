using UnityEditor;
using UnityEngine;

public class BundleBuilder
{
    public static string outputPath = "AssetBundleOutput";

    [MenuItem("Tools/Build AssetBundles")]
    public static void BuildAllBundles()
    {
        

        if (!System.IO.Directory.Exists(outputPath))
            System.IO.Directory.CreateDirectory(outputPath);

        BuildPipeline.BuildAssetBundles(
            outputPath,
            BuildAssetBundleOptions.None,
            BuildTarget.StandaloneWindows64  // or your platform
        );
    }
}