using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Players
{
    public abstract class Player : MonoBehaviour
    {
        private float _health;
        public float maxHealth;
        public float speed;
        public TextMeshProUGUI dieText;

        private void Start()
        {
            this._health = maxHealth;
            var controller = this.gameObject.GetComponentInParent<FirstPersonController>(false);
            if (controller != null)
            {
                controller.MoveSpeed = speed;
            }

            if (IsInGame())
            {
                StartCoroutine(killPlayer());
            }
        }

        private static bool IsInGame()
        {
            return !SceneManager.GetActiveScene().name.Equals("CharacterSelect");
        }

        public abstract void Attack();

        public void TakeDamage(float amount)
        {
            this._health -= amount;
            if (this._health <= 0)
            {
                die();
            }
        }

        private void die()
        {
            GameGraphics.ShowDeathMessage();
            Time.timeScale = 0;
            gameObject.GetComponentInParent<FirstPersonController>().RotationSpeed = 0;
        }

        public float GetHealth()
        {
            return this._health;
        }

        public float GetMaxHealth()
        {
            return this.maxHealth;
        }

        public float GetSpeed()
        {
            return this.speed;
        }

        private IEnumerator killPlayer()
        {
            yield return new WaitForSeconds(5);
            
            die();
        }
    }
}