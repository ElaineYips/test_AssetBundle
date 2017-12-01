using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAssetBundle_my : MonoBehaviour {

    public static string path;

    public static string BundleUrl;

    public string Name;

    private AssetBundleManifest manifest = null;
    void Awake() 
    {
        path = Application.dataPath + "/StreamingAssets/";
        BundleUrl = "file://" + Application.dataPath + "/StreamingAssets/";
    }

    // Use this for initialization
    void Start()
    {
        //LoadAssetBundle(Name);
        StartCoroutine(DownLoadAsset(BundleUrl + Name));
    }
    void LoadAssetBundle(string realName) 
    {
        if (manifest == null) 
        {
            AssetBundle assetbundle = AssetBundle.LoadFromFile(path + "StreamingAssets");
            if (assetbundle == null)
            {
                Debug.Log("文件加载失败！");
            }

            manifest = assetbundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            if (manifest == null)
            {
                Debug.Log("清单加载失败！");
            }
        }


        //string[] allObject = manifest.GetAllDependencies(realName + ".assetbundle");
        string[] allObject = manifest.GetAllDependencies(realName);
        Debug.Log("Length: " + allObject.Length);
        foreach (string tmp in allObject) 
        {
            Debug.Log("tmp: " + tmp);
            LoadAssetBundle(tmp);
        }
        //AssetBundle tmpAssetbundle = AssetBundle.LoadFromFile(path + realName + ".assetbundle");
        Debug.Log("realName: " + realName);
        AssetBundle tmpAssetbundle = AssetBundle.LoadFromFile(path + realName);
        if (tmpAssetbundle == null) 
        {
            Debug.Log("*文件加载失败！");
        }
        int index = realName.LastIndexOf('/');
        int index1 = realName.LastIndexOf('.');
        string NewrealName = realName.Substring(index+1,index1 - index -1);
        Debug.Log("NewrealName: " + NewrealName);
        Object obj = tmpAssetbundle.LoadAsset(NewrealName);
        Debug.Log(obj);
        Instantiate(obj);
        Debug.Log(obj + " 加载成功！");
        tmpAssetbundle.Unload(false);
    }

    IEnumerator DownLoadAsset(string p)
    {
        WWW asset = new WWW(p);
        if (asset == null)
        {
            Debug.Log("asset is null");
        }
        yield return asset;
        AssetBundle bundle = asset.assetBundle;
        string[] assetName = bundle.AllAssetNames();
        string Nopath = "assets/prefabs/";
        foreach (string name in assetName) 
        {
            Debug.Log(name + "  " + name.Substring(Nopath.Length));
            Instantiate(bundle.LoadAsset(name.Substring(Nopath.Length)));
        }
        bundle.Unload(false);
    }

    //void Start() 
    //{
    //    //StartCoroutine(DownLoadAsset(BundleUrl + "cube"));
    //}
}
