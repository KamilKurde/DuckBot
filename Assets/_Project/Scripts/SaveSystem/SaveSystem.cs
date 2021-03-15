using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem
{
    private string filePath = Application.persistentDataPath + "/save.data";

    public void SaveGame()
    {
        var dataStream = new FileStream(filePath, FileMode.Create);
        var converter = new BinaryFormatter();
        converter.Serialize(dataStream, GameSave.Save);
    }

    public GameSaveFile LoadGame()
    {
        if (File.Exists(filePath))
        {
            var dataStream = new FileStream(filePath, FileMode.Open);

            var converter = new BinaryFormatter();
            var saveData = converter.Deserialize(dataStream) as GameSaveFile;

            dataStream.Close();
            return saveData;
        }
        else
        {
            return new GameSaveFile();
        }
    }
}
