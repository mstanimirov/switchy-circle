using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem{

    public static void SaveData(GameManager data) {

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player_data.scs";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData playerData = new PlayerData(data);

        formatter.Serialize(stream, playerData);
        stream.Close();

    }

    public static PlayerData LoadData() {

        string path = Application.persistentDataPath + "/player_data.scs";

        if (File.Exists(path))
        {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData playerData = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return playerData;

        }
        else {

            Debug.Log("No data found!");
            return null;

        }

    }

}
