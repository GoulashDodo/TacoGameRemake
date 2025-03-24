namespace TacoGameRemake.Scripts
{
    public class GameplayExitParameters
    {
        public MainMenuEnterParameters MainMenuEnterParameters { get; }

        public GameplayExitParameters(MainMenuEnterParameters mainMenuEnterParameters)
        {
            MainMenuEnterParameters = mainMenuEnterParameters;
        }
    }
}