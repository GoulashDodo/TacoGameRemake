using TacoGameRemake.Scripts.Utils;
using UnityEngine;


namespace TacoGameRemake.Scripts
{
    public class MainMenuEnterParameters : SceneEnterParameters
    {
        public string Result { get; }

        public MainMenuEnterParameters(string result) : base(Scenes.MAIN_MENU)
        {
            Result = result;
        }
    }

}
