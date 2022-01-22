using System;
using UnityEngine;
using UnityEngine.UI;

namespace Examples.Scripts
{
    public class Door : MonoBehaviour
    {
        public Button button;
        private void OnTriggerEnter2D(Collider2D other)
        {
            button.gameObject.SetActive(true);
        }
    }
}