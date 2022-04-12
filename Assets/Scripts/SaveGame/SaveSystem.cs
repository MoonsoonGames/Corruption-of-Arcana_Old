using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveInformation(LoadSettings loadSettings)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string savePath = Application.persistentDataPath + "/saveData.file";
        FileStream saveStream = new FileStream(savePath, FileMode.Create);

        SaveData data = new SaveData(loadSettings);

        formatter.Serialize(saveStream, data);
        saveStream.Close();
    }

    public static SaveData LoadSaveData()
    {
        string path = Application.persistentDataPath + "/saveData.file";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
}
