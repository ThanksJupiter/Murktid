using System;
using UnityEngine;
using UnityEngine.Windows.WebCam;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Murktid {

    public enum EAIDirectorState {
        Buildup,
        SustainPeak,
        PeakFade,
        Relax
    }

    public class AIDirector {

        public AIDirectorSettings settings;
        public EAIDirectorState currentState = EAIDirectorState.Buildup;

        private AIDirectorDebugMenu menu;
        private EnemySystem enemySystem;

        private UpgradeWindow upgradeWindow;

        public int currentRound = 0;

        private float buildUpTime = 0f;

        private int enemiesSpawnedThisRound = 0;
        private int maxEnemiesSpawnedThisRound = 10;

        private int enemyCount = 0;
        private int totalPlayerKills = 0;
        private int playerKillCounter = 0;

        private float spawnTimer = 0f;
        private float waveSpawnTimer = 0;

        private float sustainPeakTimer = 0f;

        private float playerStressLevel = 0f;
        private float stressLevel => playerStressLevel / 100f;

        private float currentExperience = 0f;
        private float currentExperienceThreshold = 100f;
        private int currentPlayerLevel = 1;
        private int availableUpgradePoints = 0;

        private CursorHandler cursorHandler;

        public AIDirector(AIDirectorReference reference, EnemySystem enemySystem, CursorHandler cursorHandler) {
            settings = reference.settings;
            this.enemySystem = enemySystem;
            this.cursorHandler = cursorHandler;
        }

        public void Initialize() {
            waveSpawnTimer = Random.Range(settings.minWaveSpawnDelay, settings.maxWaveSpawnDelay);
            waveSpawnTimer = 2f;
            spawnTimer = Random.Range(settings.minSpawnDelay, settings.maxSpawnDelay);

            AIDirectorDebugMenuReference debugMenuReference = Object.FindFirstObjectByType<AIDirectorDebugMenuReference>();
            menu = new(debugMenuReference);
            menu.UpdateCurrentStateText(currentState);

            currentRound = 1;
            menu.UpdateCurrentRoundText(currentRound);

            menu.UpdateCurrentPlayerLevel(currentPlayerLevel);

            upgradeWindow = Object.FindFirstObjectByType<UpgradeWindow>();
            upgradeWindow.Initialize();
        }

        public void OnEnemyKilled() {
            totalPlayerKills++;
            playerKillCounter++;

            currentExperience += settings.experienceGainedOnKill;
            if(currentExperience >= currentExperienceThreshold) {

                currentPlayerLevel++;
                menu.UpdateCurrentPlayerLevel(currentPlayerLevel);

                availableUpgradePoints++;

                float leftOverExperience = currentExperience - currentExperienceThreshold;
                currentExperienceThreshold *= settings.experienceRequiredMultiplier;

                if(leftOverExperience > 0f) {
                    currentExperience = leftOverExperience;
                }
                else {
                    currentExperience = 0f;
                }
            }

            menu.UpdateExperience(currentExperience, currentExperienceThreshold);

            // TODO base stress level on more than kills
            // proximity to kills
            // damage taken
            playerStressLevel += settings.stressLevelIncreasePerKill;

            enemyCount--;
            menu.UpdateEnemyCountText(enemyCount);

            /*if(playerKillCounter >= settings.waveSpawnKillsThreshold) {
                playerKillCounter = 0;
                SpawnWave();
            }*/
        }

        public void SpawnEnemy(int amount = 1, bool aggressive = true) {
            enemyCount += amount;
            enemiesSpawnedThisRound += amount;
            menu.UpdateEnemyCountText(enemyCount);
            enemySystem.SpawnEnemies(amount, aggressive);
        }

        public void Tick(float deltaTime) {

            playerStressLevel = Mathf.Clamp(playerStressLevel, 0f, 100f);
            menu.UpdateStressLevelText(stressLevel);

            switch(currentState) {
                case EAIDirectorState.Buildup:
                    BuildUpTick(deltaTime);
                    break;
                case EAIDirectorState.SustainPeak:
                    SustainPeakTick(deltaTime);
                    break;
                case EAIDirectorState.PeakFade:
                    PeakFadeTick(deltaTime);
                    break;
                case EAIDirectorState.Relax:
                    RelaxTick(deltaTime);
                    break;
                default:
                    break;
            }
        }

        public void ChangeState(EAIDirectorState newState) {
            currentState = newState;
            menu.UpdateCurrentStateText(currentState);
        }

        public void BuildUpTick(float deltaTime) {
            spawnTimer -= deltaTime;
            buildUpTime += deltaTime;

            if(playerStressLevel > 0f) {
                playerStressLevel -= settings.stressDecayRate * deltaTime;
            }

            if(spawnTimer <= 0f) {
                spawnTimer = Mathf.Lerp(settings.maxSpawnDelay, settings.minSpawnDelay, stressLevel);

                SpawnEnemy();
            }

            waveSpawnTimer -= deltaTime;

            if(waveSpawnTimer <= 0f) {
                waveSpawnTimer = Mathf.Lerp(settings.maxWaveSpawnDelay, settings.minWaveSpawnDelay, stressLevel);
                int enemiesToSpawn = Mathf.RoundToInt(Mathf.Lerp(settings.minWaveCount, settings.maxWaveCount, stressLevel));
                SpawnEnemy(enemiesToSpawn);
            }

            // if(playerKillCounter >= settings.waveSpawnKillsThreshold) {
            if(enemiesSpawnedThisRound >= maxEnemiesSpawnedThisRound) {
                enemiesSpawnedThisRound = 0;
                maxEnemiesSpawnedThisRound *= Mathf.RoundToInt(1f + settings.roundMultiplier);
                playerKillCounter = 0;
                int enemiesToSpawn = Mathf.RoundToInt(Mathf.Lerp(settings.minWaveCount, settings.maxWaveCount, stressLevel));
                SpawnEnemy(enemiesToSpawn);
                buildUpTime = 0f;
                ChangeState(EAIDirectorState.SustainPeak);
            }

            // track player stress level
            // change to sustain peak when threshold stress level reached
        }

        public void SustainPeakTick(float deltaTime) {
            if(spawnTimer <= 0f) {
                spawnTimer = settings.minSpawnDelay;

                SpawnEnemy();
            }

            sustainPeakTimer += deltaTime;

            if(sustainPeakTimer >= settings.sustainPeakDuration) {
                ChangeState(EAIDirectorState.PeakFade);
            }
        }

        public void PeakFadeTick(float deltaTime) {
            if(enemyCount <= 0) {
                ChangeState(EAIDirectorState.Relax);
                relaxTimer = 0f;

                if(availableUpgradePoints > 0) {
                    upgradeWindow.onCompleted += HideUpgradeWindow;
                    upgradeWindow.Display(availableUpgradePoints);
                    cursorHandler.PushState(CursorHandler.CursorState.Free, this);
                    upgradeMenuDisplayed = true;
                }
            }
        }

        private float relaxTimer = 0f;
        private bool upgradeMenuDisplayed = false;

        public void RelaxTick(float deltaTime) {

            // display upgrade menu
            // let player pick 1 upgrade
            // when upgrade is selected return to buildup state

            // increase amount of enemies spawned in subsequent waves

            relaxTimer += deltaTime;

            playerStressLevel -= settings.relaxStressDecayRate * deltaTime;

            if(stressLevel <= 0f && !upgradeMenuDisplayed) {
                ChangeState(EAIDirectorState.Buildup);
                playerKillCounter = 0;
                currentRound++;
                menu.UpdateCurrentRoundText(currentRound);
            }
        }

        private void HideUpgradeWindow(int upgradePoints) {
            upgradeMenuDisplayed = false;
            upgradeWindow.onCompleted -= HideUpgradeWindow;
            availableUpgradePoints = upgradePoints;
            upgradeWindow.Hide();
            cursorHandler.ClearInstigator(this);
        }
    }
}
