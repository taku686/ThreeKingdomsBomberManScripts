using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class SaveSystem
{
    private static SaveSystem instance = new SaveSystem();
    public static SaveSystem Instance => instance;

    private SaveSystem() { Load(); }

    public string Path => Application.persistentDataPath + "/data.json";


    public UserData UserData { get; private set; }

    public void Save()
    {
        string jsonData = JsonUtility.ToJson(UserData);
        StreamWriter writer = new StreamWriter(Path, false);
        writer.WriteLine(jsonData);
        writer.Flush();
        writer.Close();
    }

    public void Load()
    {
        if (!File.Exists(Path))
        {
            Debug.Log("初回起動");
            UserData = new UserData();
            Save();
            return;
        }
        StreamReader reader = new StreamReader(Path);
        string jsonData = reader.ReadToEnd();
        UserData = JsonUtility.FromJson<UserData>(jsonData);
        reader.Close();
    }
    /*
    public void Save()
    {
        string jsonData = JsonUtility.ToJson(UserData);
        SaveText(GetWritableDirectoryPath(), "data.json", jsonData);
    }

    public void Load()
    {
        _path = GetWritableDirectoryPath() + "/" + "data.json";
        Debug.Log(_path);
        if (!File.Exists(_path))
        {
            Debug.Log("初回起動");
            UserData = new UserData();
            // CharacterData characterData = Resources.Load<CharacterData>("Default/Default");
            // UserData._currentCharacter = characterData;
            Save();
            return;
        }
        /*
        StreamReader reader = new StreamReader(Path);
        string jsonData = reader.ReadToEnd();
        UserData = JsonUtility.FromJson<UserData>(jsonData);
        reader.Close();
        
#if UNITY_EDITOR
        StreamReader reader = new StreamReader(_path);
        string jsonData = reader.ReadToEnd();
        UserData = JsonUtility.FromJson<UserData>(jsonData);
        reader.Close();
#elif UNITY_ANDROID
　　　　StreamReader reader = new StreamReader(_path);
        string jsonData = reader.ReadToEnd();
        UserData = JsonUtility.FromJson<UserData>(jsonData);
        reader.Close();
#endif
    }

    private void SaveText(string filePath, string fileName, string textToSave)
    {
        var combinedPath = Path.Combine(filePath, fileName);
        using (var writer = new StreamWriter(combinedPath))
        {
            writer.WriteLine(textToSave);
            writer.Flush();
            writer.Close();
        }
    }

    public static string GetWritableDirectoryPath()
{
    // Androidの場合、Application.persistentDataPathでは外部から読み出せる場所に保存されてしまうため
    // アプリをアンインストールしてもファイルが残ってしまう
    // ここではアプリ専用領域に保存するようにする
#if !UNITY_EDITOR && UNITY_ANDROID
        using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        using (var currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
        using (var getFilesDir = currentActivity.Call<AndroidJavaObject>("getFilesDir"))
        {
            return getFilesDir.Call<string>("getCanonicalPath");
        }
#else
    return Application.persistentDataPath;
#endif
}
*/

}