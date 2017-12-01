using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BuildAssebundle{
    [MenuItem("Custom Editor/BuildAssetbundle")]
    static void BuildAssetbundle() 
    {
        BuildPipeline.BuildAssetBundles(Application.dataPath + "/StreamingAssets",
            BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);

        AssetDatabase.Refresh();
        Debug.Log("资源打包成功");
    }

    [MenuItem("Custom Editor/Another BuildAssetbundle")]
    static void AssetBundlesBuild()
    {
        //注：第一个存放AssetBundle的路径取相对地址
        BuildPipeline.BuildAssetBundles(AssetBundleConfig.ASSETBUNDLE_PATH.Substring(AssetBundleConfig.PROJECT_PATH.Length),
            BuildAssetBundleOptions.DeterministicAssetBundle | BuildAssetBundleOptions.ChunkBasedCompression,
            BuildTarget.StandaloneWindows64
            );

        AssetDatabase.Refresh();
        Debug.Log("资源打包成功");
    }

    [MenuItem("Custom Editor/Together BuildAssetbundle")]
    static void Together_AssetBundleBuild() 
    {
        AssetBundleBuild[] buildMap = new AssetBundleBuild[1];

        buildMap[0].assetBundleName = "all.assetbundle";

        //在Project视图中，选择要打包的对象
        Object[] selects = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        string[] allassets = new string[selects.Length];
        for (int i = 0; i < selects.Length; i++) 
        {
            allassets[i] = AssetDatabase.GetAssetPath(selects[i]);
        }
        buildMap[0].assetNames = allassets;

        BuildPipeline.BuildAssetBundles(Application.dataPath + "/StreamingAssets", buildMap,
            BuildAssetBundleOptions.ChunkBasedCompression | BuildAssetBundleOptions.CollectDependencies
            , BuildTarget.StandaloneWindows64);

        AssetDatabase.Refresh();
        Debug.Log("资源打包成功");
    }
}
