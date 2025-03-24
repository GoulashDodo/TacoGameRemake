using R3;
using UnityEngine;
 
namespace TacoGameRemake.Scripts.UI
{
    public class UIMainMenuRootBinder : MonoBehaviour
    {
        private Subject<Unit> _exitSceneSignalSubject;
 
        public void HandleGoToGameplayButtonClick()
        {
            _exitSceneSignalSubject?.OnNext(Unit.Default);
        }
 
        public void Bind(Subject<Unit> exitSceneSignalSubj)
        {
            _exitSceneSignalSubject = exitSceneSignalSubj;
        }
    }
}