using UnityEngine;
using R3;
using TacoGameRemake.Scripts.UI;


namespace TacoGameRemake.Scripts
{
    public class GameplayEntryPoint : SceneEntryPoint<GameplayEnterParameters, GameplayExitParameters, UIGameplayRootBinder>
    {
        protected override Observable<GameplayExitParameters> SetupScene(UIGameplayRootBinder uiBinder, GameplayEnterParameters enterParams)
        {
            Subject<Unit> exitSceneSubject = new Subject<Unit>();
            uiBinder.Bind(exitSceneSubject);

            MainMenuEnterParameters mainMenuEnterParameters = new MainMenuEnterParameters("Wow");
            GameplayExitParameters exitParameters = new GameplayExitParameters(mainMenuEnterParameters);

            return exitSceneSubject.Select(_ => exitParameters);
        }
    }

}
