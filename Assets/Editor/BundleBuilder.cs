using Mirror;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        if(!ValidatePrefabsInProject())
        {
            Debug.LogError("Asset validation failed. Asset bundles were not generated.");
            return;
        }

        ChangeAssetBundleNamesToUniqueNames();

        BuildPipeline.BuildAssetBundles(
            outputPath,
            BuildAssetBundleOptions.None,
            BuildTarget.StandaloneWindows64
        );
        HandleBuildFiles();
        ChangeAssetBundleNamesBackToGeneric();
    }
    #region assetBundleNames

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
    #endregion
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


    #region validatePrefabs
    public static uint[] reservedAssetIDs = new uint[] { 3516282168, 802227567, 0 };
    public static bool ValidatePrefabsInProject()
    {
        string[] prefabsGUIDs = AssetDatabase.FindAssets("t:prefab b:*");
        Debug.Log($"Assets to validate: {prefabsGUIDs.Length}");
        bool validationOK = true;
        foreach (string assetGUID in prefabsGUIDs)
        {

            GladioMoriAsset gmAsset = new GladioMoriAsset
            {
                GUID = assetGUID,
                Path = AssetDatabase.GUIDToAssetPath(assetGUID)
            };
            
            try
            {
                GameObject gameObject = AssetDatabase.LoadAssetAtPath<GameObject>(gmAsset.Path);
                NetworkIdentity identity = gameObject.GetComponent<NetworkIdentity>();
                if(identity != null)
                {
                    
                    Debug.Log($"assetID:{identity.assetId}"); 

                    if (reservedAssetIDs.Contains(identity.assetId))
                    {
                        Debug.LogError($"Asset uses a reserved GUID. Recreate the asset by copying it and delete the old copy: {gmAsset.Path}");
                        validationOK = false;
                    }
                }

            }
            catch(Exception ex)
            {
                Debug.LogWarning($"Validation failed for asset {assetGUID}");
                Debug.LogWarning(ex);
            }

            
        }


        return validationOK;
    }

    #endregion
}