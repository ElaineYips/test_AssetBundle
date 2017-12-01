using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAsseBundle : MonoBehaviour
{
    private static AssetBundleManifest manifest = null;

    private static Dictionary<string, AssetBundle> assetbundleDic = new Dictionary<string, AssetBundle>();

    public string TheName;

    public AssetBundle LoadAssetBundle(string Url)
    {
        if (assetbundleDic.ContainsKey(Url))
        {
            return assetbundleDic[Url];
        }
        if (manifest == null)
        {
            AssetBundle manifestAssetBundle = AssetBundle.LoadFromFile(Application.dataPath + "/StreamingAssets/StreamingAssets");
            if (manifestAssetBundle == null)
            {
                Debug.Log("文件加载失败");
            }
            manifest = manifestAssetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            if (manifest == null)
            {
                Debug.Log("manifest is null!");
            }
        }
        if (manifest != null)
        {
            string[] objectDependUrl = manifest.GetAllDependencies(Url);
            Debug.Log("length: " + objectDependUrl.Length);
            foreach (string tmpUrl in objectDependUrl)
            {
                LoadAssetBundle(tmpUrl);
            }
            AssetBundle a = AssetBundle.LoadFromFile(AssetBundleConfig.ASSETBUNDLE_PATH + Url);
            Debug.Log("assetbundle: " + a);
            assetbundleDic[Url] = a;
            return assetbundleDic[Url];
        }
        return null;
    }

    public IEnumerator InstanceAsset(string assetBundleName)
    {
        string assetBundlePath = assetBundleName + AssetBundleConfig.SUFFIX;
        Debug.Log("assetBundlePath: " + assetBundlePath);
        int index = assetBundleName.LastIndexOf('/');
        Debug.Log("index: " + index);
        string realName = assetBundleName.Substring(index + 1, assetBundleName.Length - index - 1);
        Debug.Log("realName: " + realName);
        yield return LoadAssetBundle(assetBundlePath);

        if (assetbundleDic.ContainsKey(assetBundlePath) && assetbundleDic[assetBundlePath] != null)
        {
            Object tmpObj = assetbundleDic[assetBundlePath].LoadAsset(realName);
            Debug.Log("GameObject: " + tmpObj);
            yield return Instantiate(tmpObj);
            assetbundleDic[assetBundlePath].Unload(false);
        }
        yield break;
    }

    void Start()
    {
        StartCoroutine(InstanceAsset(TheName));
    }
}
