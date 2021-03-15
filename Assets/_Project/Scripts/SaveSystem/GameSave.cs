public static class GameSave
{
    private static readonly SaveSystem SaveSystem = new SaveSystem();
    public static readonly GameSaveFile Save = SaveSystem.LoadGame();

    public static void SaveGame()
    {
        SaveSystem.SaveGame();
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
    
}