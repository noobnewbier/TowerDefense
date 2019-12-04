using System.Diagnostics;
using Common.Constant;
using UnityEngine;

namespace Common.Class
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            SetToRunInBackground();
        }

        [Conditional(GameConfig.TrainingMode)]
        private static void SetToRunInBackground()
        {
            Application.runInBackground = true;
        }
    }
}