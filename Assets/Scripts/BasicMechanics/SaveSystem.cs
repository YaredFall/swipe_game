using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    private static string dataFile = "44617461.dat"; //name of the file for save or load
    public static void SaveData(PlayerData pData)
    {
        string path = Application.persistentDataPath + "/" + dataFile;
        BinaryFormatter bf = new BinaryFormatter();
        
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = pData;

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadData()
    {
        string path = Application.persistentDataPath + "/" + dataFile;
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = bf.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("File is not found!");
            return null;
        }
    }

}
