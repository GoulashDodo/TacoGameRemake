using UnityEngine;


namespace TacoGameRemake.Scripts.UI
{

    public class UIRootView : MonoBehaviour
    {

        [SerializeField] private GameObject _loadingScreen;
        [SerializeField] private Transform _UISceneContainter;

        private void Awake()
        {
            HideLoadingScreen();
        }

        public void ShowLoadingScreen()
        {
            _loadingScreen.SetActive(true);
        }

        public void HideLoadingScreen()
        {
            _loadingScreen.SetActive(false);
        }


        public void AttachSceneUI(GameObject sceneUI)
        {
            ClearSceneUI();

            sceneUI.transform.SetParent(_UISceneContainter, false);
        }


        public void ClearSceneUI()
        {
            var childCount = _UISceneContainter.childCount;


            for (int i = 0; i < childCount; i++)
            {
                Destroy(_UISceneContainter.GetChild(i).gameObject);
            }
        }

    }

}