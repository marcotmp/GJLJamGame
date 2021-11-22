using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MarcoTMP
{
    public class DebugConsole : MonoBehaviour
    {
        public static DebugConsole instance;
        public Text text;

        private void Awake()
        {
            instance = this;
            text.text = "";
        }

        public void _Log(string message)
        {
            text.text += message + "\n";
        }

        public static void Log(string message)
        {
            instance?._Log(message);
        }
    }
}