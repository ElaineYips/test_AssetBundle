using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleConfig_my : MonoBehaviour {
    //将来打包要存放的地方
    public static string ASSETBUNDLE_PATH = Application.dataPath + "/StreamingAssets";
    //需要打包的资源地址
    public static string APPLICATION_PATH = Application.dataPath + "/";
    //工程根目录地址
    public static string PROJECT_PATH = APPLICATION_PATH.Substring(0,APPLICATION_PATH.Length - 7);
    //打包文件的后缀名
    public static string SUFFIX = ".assetbundle";
}
