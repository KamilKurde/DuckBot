using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class GameSave
{
    private static readonly string FilePath = Application.persistentDataPath + "/save.data";
    private static readonly GameSaveFile Save = LoadGame();

    private static GameSaveFile LoadGame()
    {
        if (!File.Exists(FilePath)) return new GameSaveFile();
        var dataStream = new FileStream(FilePath, FileMode.Open);

        var converter = new BinaryFormatter();
        var saveData = converter.Deserialize(dataStream) as GameSaveFile;

        dataStream.Close();
        return saveData;
    }

    public static void LoadAudioSettings()
    {
        GameManager.SetVolume(AudioGroup.Master, Save.audioVolume);
        GameManager.SetVolume(AudioGroup.Sound, Save.sfxVolume);
        GameManager.SetVolume(AudioGroup.Music, Save.musicVolume);
    }

    private static void SaveGame()
    {
        var dataStream = new FileStream(FilePath, FileMode.Create);
        
        var converter = new BinaryFormatter();
        converter.Serialize(dataStream, Save);
        
        dataStream.Close();
    }

    public static int CurrentLevelId
    {
        get => Save.currentLevelId;
        set
        {
            Save.currentLevelId = value;
            SaveGame();
        }
    }

    public static float AudioVolume
    {
        get => Save.audioVolume;
        set
        {
            Save.audioVolume = value;
            SaveGame();
            GameManager.SetVolume(AudioGroup.Master, Save.audioVolume);
        }
    }

    public static float SfxVolume
    {
        get => Save.sfxVolume;
        set
        {
            Save.sfxVolume = value;
            SaveGame();
            GameManager.SetVolume(AudioGroup.Sound, Save.sfxVolume);
        }
    }

    public static float MusicVolume
    {
        get => Save.musicVolume;
        set
        {
            Save.musicVolume = value;
            SaveGame();
            GameManager.SetVolume(AudioGroup.Music, Save.musicVolume);
        }
    }
}