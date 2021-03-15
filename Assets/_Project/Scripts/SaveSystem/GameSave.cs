public static class GameSave
{
    public static readonly GameSaveFile Save = new SaveSystem().LoadGame();

    public static int currentLevelId
    {
        get => Save.currentLevelId;
        set => Save.currentLevelId = value;
    }
    
}