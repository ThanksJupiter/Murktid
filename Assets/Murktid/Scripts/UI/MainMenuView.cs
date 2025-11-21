using System;
using UnityEngine;

namespace Murktid {

    public class MainMenuView : IApplicationLifecycle {
        private readonly MenuPrefabsContainer menuPrefabsContainer;
        private readonly MenuApplicationStateData menuApplicationStateData;
        private MainMenuReference mainMenuReference;
        public MainMenuView(MenuPrefabsContainer menuPrefabsContainer, MenuApplicationStateData menuApplicationStateData) {
            this.menuPrefabsContainer = menuPrefabsContainer;
            this.menuApplicationStateData = menuApplicationStateData;
        }

        public void Initialize() {
            if(mainMenuReference == null) {
                mainMenuReference = GameObject.Instantiate(menuPrefabsContainer.mainMenuReference);
            }

            mainMenuReference.exitButton.onClick.AddListener(Application.Quit);
            mainMenuReference.optionsButton.onClick.AddListener(() => throw new NotImplementedException());
            mainMenuReference.playButton.onClick.AddListener(() => menuApplicationStateData.startGameRequests.Invoke(GameMode.Murktid));
        }

        public void Tick() { }
        public void Dispose() { }
    }
}
