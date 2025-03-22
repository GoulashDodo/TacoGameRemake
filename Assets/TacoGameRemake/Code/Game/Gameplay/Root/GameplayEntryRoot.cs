using UnityEngine;
using R3;
using TacoGameRemake.Scripts.UI;



namespace TacoGameRemake.Scripts
{
    public class GameplayEntryRoot : MonoBehaviour
    {
        [SerializeField] private UIGameplayRootBinder _sceneRootBinderPrefab;



        public Observable<GameplayExitParameters> Run(UIRootView uiRootView, GameplayEnterParameters gameplayEnterParameters)
        {

            UIGameplayRootBinder UIScene = Instantiate(_sceneRootBinderPrefab);

            uiRootView.AttachSceneUI(UIScene.gameObject);


            Subject<Unit> exitSceneSubject = new Subject<Unit>();

            _sceneRootBinderPrefab.Bind(exitSceneSubject);

            MainMenuEnterParameters mainMenuEnterParameters = new MainMenuEnterParameters("Wow");
            GameplayExitParameters exitParameters = new GameplayExitParameters(mainMenuEnterParameters);

            Observable<GameplayExitParameters> exitMainMenuSceneSignal = exitSceneSubject.Select(_ => exitParameters);

            return exitMainMenuSceneSignal;

        }





    }
}
