using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class StartSceneFromSettings : MonoBehaviour
{
    [MenuItem("Tools/PlayGame %@")] // ショートカットキーの指定
    public static void PlayFromPrelaunchScene()
    {
        // プレイ中の場合は停止する       
        if (EditorApplication.isPlaying == true)
        {
            EditorApplication.isPlaying = false;
            return;
        }

        // 先頭のシーンパスを取得 
        string startScenePath = EditorBuildSettings.scenes[0].path; // ここを変更してね
        EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(startScenePath);

        // 再生開始
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) EditorApplication.isPlaying = true;
    }
}