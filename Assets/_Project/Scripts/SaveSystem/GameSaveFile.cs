[System.Serializable]
public class GameSaveFile
{
    public int currentLevelId = 1;

    public void Save()
    {
        new SaveSystem().SaveGame();
    }
}