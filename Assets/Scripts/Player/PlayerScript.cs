using System;
using UnityEngine;

namespace Player
{
    public class PlayerScript : MonoBehaviour
    {
        public static PlayerScript Instance;

        private void Awake()
        {
            Instance = this;
        }
    }
}