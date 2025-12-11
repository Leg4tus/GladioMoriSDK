using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BundleBuilder
{
    public static string outputPath = "AssetBundleOutput";
    public static string baseEquipmentBundleName = "equipment";
    public static string baseMapsBundleName = "maps";
    public static string manifestExtension = ".manifest";


    [MenuItem("Tools/Build AssetBundles")]
    public static void BuildAllBundles()
    {
        

        if (!System.IO.Directory.Exists(outputPath))
            System.IO.Directory.CreateDirectory(outputPath);

        ChangeAssetBundleNamesToUniqueNames();

        BuildPipeline.BuildAssetBundles(
            outputPath,
            BuildAssetBundleOptions.None,
            BuildTarget.StandaloneWindows64
        );
        HandleBuildFiles();
        ChangeAssetBundleNamesBackToGeneric();
    }

    public static List<GladioMoriAsset> equipmentAssets = new List<GladioMoriAsset>(); 
    public static List<GladioMoriAsset> mapAssets = new List<GladioMoriAsset>();
    public static string equipmentAssetBundleName;
    public static string mapsAssetBundleName;
    public static void ChangeAssetBundleNamesToUniqueNames()
    {
        equipmentAssets.Clear();
        mapAssets.Clear();

        equipmentAssetBundleName = $"equipment_{System.Guid.NewGuid().ToString("N")}";
        mapsAssetBundleName = $"maps_{System.Guid.NewGuid().ToString("N")}";


        string[] equipmentAssetGUIDs = AssetDatabase.FindAssets("b:equipment");
        string[] mapAssetsGUIDs = AssetDatabase.FindAssets("b:maps");
        HandleAssetGUIDs(equipmentAssetGUIDs, equipmentAssets, equipmentAssetBundleName);
        HandleAssetGUIDs(mapAssetsGUIDs, mapAssets, mapsAssetBundleName);
        
    }

    public static void HandleAssetGUIDs(string[] assetGUIDs, List<GladioMoriAsset> assetList, string bundleName)
    {
        foreach (string assetGUID in assetGUIDs)
        {

            GladioMoriAsset gmAsset = new GladioMoriAsset
            {
                GUID = assetGUID,
                Path = AssetDatabase.GUIDToAssetPath(assetGUID)
            };
            assetList.Add(gmAsset);
            Debug.Log($"{bundleName}: {gmAsset.GUID} {gmAsset.Path}");

            AssetImporter assetImporter = AssetImporter.GetAtPath(gmAsset.Path);
            assetImporter.assetBundleName = bundleName;
        }
    }



    public static void ChangeAssetBundleNamesBackToGeneric()
    {
        foreach (GladioMoriAsset gmAsset in equipmentAssets)
        {
            AssetImporter assetImporter = AssetImporter.GetAtPath(gmAsset.Path);
            assetImporter.assetBundleName = baseEquipmentBundleName;
        }
        foreach (GladioMoriAsset gmAsset in mapAssets)
        {
            AssetImporter assetImporter = AssetImporter.GetAtPath(gmAsset.Path);
            assetImporter.assetBundleName = baseMapsBundleName;
        }

        AssetDatabase.RemoveUnusedAssetBundleNames();
    }

    public static void HandleBuildFiles()
    {
        HandleBuildFile(Path.Combine(outputPath, equipmentAssetBundleName), Path.Combine(outputPath, baseEquipmentBundleName));
        HandleBuildFile(Path.Combine(outputPath, equipmentAssetBundleName+ manifestExtension), Path.Combine(outputPath, baseEquipmentBundleName + manifestExtension));


        HandleBuildFile(Path.Combine(outputPath, mapsAssetBundleName), Path.Combine(outputPath, baseMapsBundleName));
        HandleBuildFile(Path.Combine(outputPath, mapsAssetBundleName+ manifestExtension), Path.Combine(outputPath, baseMapsBundleName+ manifestExtension));
    }
    public static void HandleBuildFile(string currentFilePath, string newFilePath)
    {
        if (File.Exists(newFilePath))
        {
            System.IO.File.Delete(newFilePath);
        }
        if (File.Exists(currentFilePath))
        {
            System.IO.File.Move(currentFilePath, newFilePath);
        }
    }
}