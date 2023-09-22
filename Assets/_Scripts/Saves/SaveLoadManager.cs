using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoadManager
{
    public static void Save(string path, SaveData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void Save()
    {
        SaveData Data = GameManager.GetProgress();

        Save(GetPath(), Data);
    }

    public static SaveData Load()
    {
        SaveData result = Load(GetPath());

        if (result == null)
        {
            result = CreateNewSave();
        }

        return result;
    }

    public static SaveData Load(string path)
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = null;
            try
            {
                data = (SaveData)formatter.Deserialize(stream);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            finally
            {
                stream.Close();
            }
            return data;
        }
        else
        {
            return default;
        }
    }

    public static bool FileExists(string path)
    {
        return File.Exists(path);
    }

    public static string GetPath()
    {
        //C:\Users\<userprofile>\AppData\LocalLow\<companyname>\<productname>
        return Application.persistentDataPath + $"data.save";
    }

    public static SaveData CreateNewSave()
    {
        var newSave = new SaveData();
        Save(GetPath(), newSave);
        return newSave;
    }
}
