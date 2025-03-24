using R3;
using TacoGameRemake.Scripts.UI;
using UnityEngine;


namespace TacoGameRemake.Scripts
{
    public class MainMenuEntryPoint : SceneEntryPoint<MainMenuEnterParameters, MainMenuExitParameters, UIMainMenuRootBinder>
    {
        protected override Observable<MainMenuExitParameters> SetupScene(UIMainMenuRootBinder uiBinder, MainMenuEnterParameters enterParameters)
        {
            var exitSignalSubject = new Subject<Unit>();
            uiBinder.Bind(exitSignalSubject);
            
            Debug.Log($"MAIN MENU ENTRY POINT: Run main menu scene. Results: {enterParameters?.Result}");
 
            var gameplayEnterParams = new GameplayEnterParameters(TemporaryConstants.SAVE_FILE_PATH_PLACEHOLDER, TemporaryConstants.CURRENT_LEVEL_PLACEHOLDER);
            var mainMenuExitParams = new MainMenuExitParameters(gameplayEnterParams);
            var exitToGameplaySceneSignal = exitSignalSubject.Select(_ => mainMenuExitParams);
             
            return exitToGameplaySceneSignal;
            
        }

    }
   
}