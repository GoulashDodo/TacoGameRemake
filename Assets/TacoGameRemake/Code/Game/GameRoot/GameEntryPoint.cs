using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using R3;
using TacoGameRemake.Scripts.Utils;
using TacoGameRemake.Scripts.UI;
using Zenject;

namespace TacoGameRemake.Scripts
{
    public class GameEntryPoint
    {

        private static GameEntryPoint _instance;
        
        private readonly Coroutines _coroutines;
        private readonly UIRootView _uiRoot;
        
        private readonly DiContainer _rootContainer = new DiContainer();
        private readonly DiContainer _cachedSceneContainer;
        
            
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutoStartGame()
        {
            _instance = new GameEntryPoint();

            _instance.RunGame();
        }

        private GameEntryPoint()
        {
            _coroutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
            Object.DontDestroyOnLoad(_coroutines.gameObject);

            var prefabUIRoot = Resources.Load<UIRootView>("UIRoot");

            _uiRoot = Object.Instantiate(prefabUIRoot);
            Object.DontDestroyOnLoad(_uiRoot.gameObject);
            
            _rootContainer.Bind<UIRootView>().FromInstance(_uiRoot).AsSingle();
            

        }

        private void RunGame()
        {
#if UNITY_EDITOR
            var sceneName = SceneManager.GetActiveScene().name;

            if (sceneName == Scenes.GAMEPLAY)
            {
                GameplayEnterParameters temp = new GameplayEnterParameters(TemporaryConstants.SAVE_FILE_PATH_PLACEHOLDER, TemporaryConstants.CURRENT_LEVEL_PLACEHOLDER);
                _coroutines.StartCoroutine(LoadAndStartGameplay(temp));
                return;
            }

            if (sceneName == Scenes.MAIN_MENU)
            {
                _coroutines.StartCoroutine(LoadAndStartMainMenu());
                return;
            }

            if(sceneName != Scenes.BOOT)
            {
                return;
            }

#endif
            _coroutines.StartCoroutine(LoadAndStartMainMenu());


        }

        private IEnumerator LoadAndStartGameplay(GameplayEnterParameters gameplayEnterParameters)
        {
            _uiRoot.ShowLoadingScreen();
            
            _cachedSceneContainer?.UnbindAll();
            
            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.GAMEPLAY);

            yield return new WaitForSeconds(1);
            

            GameplayEntryPoint sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
        
            DiContainer gameplayContainer = new DiContainer(_rootContainer);
            
            Observable<GameplayExitParameters> exitMainMenuSignal = sceneEntryPoint.Run(gameplayContainer, gameplayEnterParameters);

            exitMainMenuSignal.Subscribe(_ => {
                _coroutines.StartCoroutine(LoadAndStartMainMenu());
            });
            
            _uiRoot.HideLoadingScreen();
           
        }

        private IEnumerator LoadAndStartMainMenu(MainMenuEnterParameters mainMenuEnterParameters = null)
        {
            _uiRoot.ShowLoadingScreen();
            
            _cachedSceneContainer?.UnbindAll();
            
            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.MAIN_MENU);

            yield return new WaitForSeconds(1);
            
        
            DiContainer mainMenuContainer = new DiContainer(_rootContainer);

            MainMenuEntryPoint sceneEntryRoot = Object.FindFirstObjectByType<MainMenuEntryPoint>();
            Observable<MainMenuExitParameters> exitMainMenuSignal = sceneEntryRoot.Run(mainMenuContainer, mainMenuEnterParameters);

            exitMainMenuSignal.Subscribe(mainMenuExitParameters =>
            {
                var targetSceneName = mainMenuExitParameters.TargetSceneEnterParameters.SceneName;
 
                if (targetSceneName == Scenes.GAMEPLAY)
                {
                    _coroutines.StartCoroutine(LoadAndStartGameplay(mainMenuExitParameters.TargetSceneEnterParameters.As<GameplayEnterParameters>()));
                }
            });

            _uiRoot.HideLoadingScreen();

        }
        
        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }

    }
}

