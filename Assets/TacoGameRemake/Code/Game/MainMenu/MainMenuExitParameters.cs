using UnityEngine;

namespace TacoGameRemake.Scripts
{
    public class MainMenuExitParameters
    {
        public SceneEnterParameters TargetSceneEnterParameters { get; }


        public MainMenuExitParameters(SceneEnterParameters targetSceneEnterParameters)
        {
            TargetSceneEnterParameters = targetSceneEnterParameters;
        }
        
    }
}