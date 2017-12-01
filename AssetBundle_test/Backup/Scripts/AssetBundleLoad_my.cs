using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleLoad_my : MonoBehaviour {
    public static AssetBundleManifest manifest = null;

    private static Dictionary<string, AssetBundle> assetBundleDic = new Dictionary<string, AssetBundle>();

    public AssetBundle LoadAssetBundle(string url) 
    {
        if (assetBundleDic.ContainsKey(url))
            return assetBundleDic[url];
        if (manifest == null) 
        {
            string[] objectDependUrl = manifest.GetAllDependencies(url);
            foreach (string tmpurl in objectDependUrl) 
            {
                LoadAssetBundle(tmpurl);
            }
            Debug.Log(AssetBundleConfig_my.ASSETBUNDLE_PATH + url);
            assetBundleDic[url] = AssetBundle.LoadFromFile(AssetBundleConfig_my.ASSETBUNDLE_PATH + url);
            return assetBundleDic[url];
        }
        return null;

    }

    private IEnumerator InstancesAsset(string assetBundleName) 
    {
        string assetBundlePath = assetBundleName + AssetBundleConfig_my.SUFFIX;

        int index = assetBundleName.LastIndexOf('/');

        //这个才是对应文件的名称
        string realName = assetBundleName.Substring(index+1,assetBundleName.Length - index -1);

        yield return LoadAssetBundle(assetBundlePath);

        if (assetBundleDic.ContainsKey(assetBundlePath) && assetBundleDic[assetBundlePath] != null) 
        {
            Object tmpObj = assetBundleDic[assetBundlePath].LoadAsset(realName);
            yield return Instantiate(tmpObj);
            assetBundleDic[assetBundlePath].Unload(false);
        }
        yield break;
    }
}
