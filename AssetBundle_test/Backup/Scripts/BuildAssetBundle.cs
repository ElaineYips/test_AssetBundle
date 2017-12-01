using UnityEditor;
using UnityEngine;

public class BuildAssetBundle
{

    /// <summary>
    /// 点击后，所有设置了AssetBundle名称的资源会被 分单个打包出来
    /// </summary>
    [MenuItem("AssetBundle/Build (Single)")]
    static void Build_AssetBundle()
    {
        BuildPipeline.BuildAssetBundles(Application.dataPath + "/Test_AssetBundle", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
        //刷新
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 选择的资源合在一起被打包出来
    /// </summary>
    [MenuItem("AssetBundle/Build (Collection)")]
    static void Build_AssetBundle_Collection()
    {
        AssetBundleBuild[] buildMap = new AssetBundleBuild[1];
        //打包出来的资源包名字
        buildMap[0].assetBundleName = "enemybundle";

        //在Project视图中，选择要打包的对象  
        Object[] selects = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        string[] enemyAsset = new string[selects.Length];
        for (int i = 0; i < selects.Length; i++)
        {
            //获得选择 对象的路径
            enemyAsset[i] = AssetDatabase.GetAssetPath(selects[i]);
        }
        buildMap[0].assetNames = enemyAsset;

        BuildPipeline.BuildAssetBundles(Application.dataPath + "/Test_AssetBundle", buildMap, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
        //刷新
        AssetDatabase.Refresh();
    }
}
