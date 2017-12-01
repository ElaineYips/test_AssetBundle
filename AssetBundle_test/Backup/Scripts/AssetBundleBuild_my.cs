using System.Collections;
using UnityEngine;
using UnityEditor;

public class AssetBundleBuild_my : MonoBehaviour {

    [MenuItem("AssetBundle Editor/AssetBundlesBuild_my")]
    static void AssetBundlesBuild_my() 
    {
        BuildPipeline.BuildAssetBundles(AssetBundleConfig_my.ASSETBUNDLE_PATH.Substring(AssetBundleConfig_my.PROJECT_PATH.Length), 
            BuildAssetBundleOptions.ChunkBasedCompression
            | BuildAssetBundleOptions.DeterministicAssetBundle, BuildTarget.StandaloneWindows64);
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
