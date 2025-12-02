using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Murktid {

    public class ShotgunCrosshair : MonoBehaviour {
        [SerializeField]
        private Animator animator;

        [SerializeField]
        private Image[] crosshairComponents;

        [SerializeField]
        private Image[] pellets;

        public Color pelletHitColor = Color.red;
        public Color pelletClearColor = Color.clear;
        public float pelletFadeOutSpeed = 1f;
        public float fadeOutDelay = 1f;

        private float fadeOutTimer = 0f;

        class PelletFadeOut {
            public readonly Image image;
            public float timer;

            public PelletFadeOut(Image image, float delay) {
                this.image = image;
                timer = delay;
            }
        }

        private List<PelletFadeOut> fadeOutPellets = new List<PelletFadeOut>();

        public int PelletAmount => pellets.Length;

        public Ray GetPelletScreenPointRay(int index, Camera camera) {
            return camera.ScreenPointToRay(pellets[index].rectTransform.position);
        }

        public void DisplayPelletHit(int index) {

            Image pelletImage = pellets[index];

            foreach(PelletFadeOut pellet in fadeOutPellets) {
                if(pellet.image == pelletImage) {
                    pellet.image.color = pelletHitColor;
                    pellet.timer = fadeOutDelay;
                    return;
                }
            }

            pelletImage.color = pelletHitColor;
            fadeOutTimer = fadeOutDelay;

            PelletFadeOut fadeOutPellet = new(pelletImage, fadeOutDelay);
            fadeOutPellets.Add(fadeOutPellet);
        }

        private void Update() {

            float deltaTime = Time.deltaTime;
            foreach(PelletFadeOut pellet in fadeOutPellets) {
                if(pellet.timer > 0f) {
                    pellet.timer -= deltaTime;
                    continue;
                }

                pellet.image.color = Color.Lerp(pellet.image.color, pelletClearColor, 1f - Mathf.Exp(-pelletFadeOutSpeed * Time.deltaTime));
            }
        }

        public void Show() {
            foreach(Image image in crosshairComponents) {
                image.enabled = true;
            }
        }

        public void Hide() {
            foreach(Image image in crosshairComponents) {
                image.enabled = false;
            }
        }

        public void SetIsADS() {
            animator.SetBool("IsADS", true);
        }

        public void SetIsHipfire() {
            animator.SetBool("IsADS", false);
        }
    }
}
