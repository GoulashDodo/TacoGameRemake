using UnityEngine;
using R3;


namespace TacoGameRemake.Scripts.UI
{
    public class UIGameplayRootBinder : MonoBehaviour
    {
        private Subject<Unit> _exitSceneSignalSubject;

        public void HandleGoToMainMenu()
        {
            _exitSceneSignalSubject?.OnNext(Unit.Default);
        }

        public void Bind(Subject<Unit> exitSceneSignalSubject)
        {
            _exitSceneSignalSubject = exitSceneSignalSubject;
        }

    }
}

