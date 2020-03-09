using System.IO;
using UnityEditor;

namespace TestJob.Editor
{
    public class CreateAssetBundles
    {
        private const string AssetBundlesPathWindows = "Assets/StreamingAssets/AssetBundles/Standalone";
        private const string AssetBundlesPathAndroid = "Assets/StreamingAssets/AssetBundles/Android";

        [MenuItem("Assets/Build AssetBundles")]
        private static void BuildAllAssetBundles()
        {
            BuildBundlesStandalone();
            BuildBundlesAndroid();
        }

        private static void BuildBundlesStandalone()
        {
            PrepareDirectory(AssetBundlesPathWindows);
            BuildPipeline.BuildAssetBundles(AssetBundlesPathWindows, BuildAssetBundleOptions.None,
                BuildTarget.StandaloneWindows64);
        }

        private static void BuildBundlesAndroid()
        {
            PrepareDirectory(AssetBundlesPathAndroid);
            BuildPipeline.BuildAssetBundles(AssetBundlesPathAndroid, BuildAssetBundleOptions.None, BuildTarget.Android);
        }

        private static void PrepareDirectory(string path, bool dontCreate = false)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }

            if (!dontCreate) Directory.CreateDirectory(path);
        }
    }
}