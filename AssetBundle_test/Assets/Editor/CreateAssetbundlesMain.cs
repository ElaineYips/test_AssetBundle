using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateAssetbundlesMain : MonoBehaviour {
    [MenuItem("Custom Editor/Create AssetBundles Main")]
    static void CreateAssetBundlesMain() 
    {
        Object[] SelectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        //Debug.Log("SelectedAsset.Length: "+ SelectedAsset.Length);

        foreach (Object obj in SelectedAsset) 
        {
            string sourcePath = AssetDatabase.GetAssetPath(obj);

            string targetPath = Application.dataPath + "/StreamingAssets/" + obj.name + ".assetbundle";

            if (BuildPipeline.BuildAssetBundle(obj, null, targetPath, BuildAssetBundleOptions.CollectDependencies, BuildTarget.StandaloneWindows64))
            {
                Debug.Log("资源打包成功");
            }
            else 
            {
                Debug.Log("资源打包不成功");
            }
        }
        AssetDatabase.Refresh();
    }

    [MenuItem("Custom Editor/Create AssetBundles All")]
    static void CreateAssetBunldesAll() 
    {
        Caching.CleanCache();

        string path = Application.dataPath + "/StreamingAssets/All.assetbundle";

        Object[] SelectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

        foreach (Object obj in SelectedAsset) 
        {
            Debug.Log("Create AssetBundles name :" + obj);
        }

        if (BuildPipeline.BuildAssetBundle(null, SelectedAsset, path, BuildAssetBundleOptions.CollectDependencies, BuildTarget.StandaloneWindows64))
        {
            AssetDatabase.Refresh();
            Debug.Log("统一资源打包成功");
        }
        else 
        {
            Debug.Log("统一资源打包失败");
        }
    }
}
