using UnityEditor;
using UnityEngine;

public class CreateAssetBundleTool
{
    public static string assetBundleDirectoryPath = Application.dataPath + "/../ASSET_BUNDLES";
    private static void Build(in string path, in BuildAssetBundleOptions buildAssetBundleOptions, in BuildTarget buildTarget)
    {
        if (!System.IO.Directory.Exists(path))
        {
            System.IO.Directory.CreateDirectory(path);
        }
        try
        {
            BuildPipeline.BuildAssetBundles(path, buildAssetBundleOptions, buildTarget);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e);
        }
    }


    [MenuItem("UROD Engine/Asset Bundles/Create Assets Bundles Android")]
    private static void BuildAssetBundlesAndroid()=> Build(assetBundleDirectoryPath, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.Android);

    [MenuItem("UROD Engine/Asset Bundles/Create Assets Bundles Windows")]
    private static void BuildAssetBundlesWindows()
    {
        Build(assetBundleDirectoryPath, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows);
    }

    [MenuItem("UROD Engine/Asset Bundles/Create Assets Bundles Windows64")]
    private static void BuildAssetBundlesWindows64()
    {
        Build(assetBundleDirectoryPath, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows);
    }

}
