using TacoGameRemake.Scripts.Utils;


namespace  TacoGameRemake.Scripts
{
    public class GameplayEnterParameters : SceneEnterParameters
    {
        public string SaveFileName { get; }
        public int LevelNumber { get; } 
        
        public GameplayEnterParameters(string saveFileName, int levelNumber) : base(Scenes.GAMEPLAY)
        {
            SaveFileName = saveFileName;
            LevelNumber = levelNumber;
        }
    }

}
