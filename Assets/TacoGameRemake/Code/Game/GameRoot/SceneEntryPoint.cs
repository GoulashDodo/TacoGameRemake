using UnityEngine;
using R3;
using TacoGameRemake.Scripts.UI;
using Zenject;

namespace TacoGameRemake.Scripts
{
    public abstract class SceneEntryPoint<TEnterParams, TExitParams, TUIBinder> : MonoBehaviour
        where TUIBinder : MonoBehaviour
    {
        [SerializeField] protected TUIBinder _sceneRootBinderPrefab;

        public Observable<TExitParams> Run(DiContainer sceneDIContainer, TEnterParams enterParams)
        {
            
            UIRootView uiRootView = sceneDIContainer.Resolve<UIRootView>(); 
            
            TUIBinder uiScene = Instantiate(_sceneRootBinderPrefab);
            uiRootView.AttachSceneUI(uiScene.gameObject);
            return SetupScene(uiScene, enterParams);
        }

        protected abstract Observable<TExitParams> SetupScene(TUIBinder uiBinder, TEnterParams enterParams);
    }
}
