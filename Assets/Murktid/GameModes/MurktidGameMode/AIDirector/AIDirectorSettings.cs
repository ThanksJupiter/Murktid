using UnityEngine;

namespace Murktid {

    [CreateAssetMenu(fileName = "AIDirectorSettings", menuName = "Murktid/Settings/AIDirectorSettings")]
    public class AIDirectorSettings : ScriptableObject {
        public int waveSpawnKillsThreshold = 30;
        public int minWaveCount = 3;
        public int maxWaveCount = 10;

        public float minWaveSpawnDelay = 20f;
        public float maxWaveSpawnDelay = 30f;

        public float minSpawnDelay = .1f;
        public float maxSpawnDelay = 2f;

        [Header("Enemies spawned")]
        public int baseMaxEnemies = 10;

        [Header("Player Stress Level")]
        public float stressLevelIncreasePerKill = 5f;
        public float stressDecayRate = .25f;
        public float relaxStressDecayRate = 10f;

        public float roundMultiplier = .1f;

        [Header("Player Experience")]
        public float baseRequiredExperienceToLevelUp = 100f;
        public float experienceRequiredMultiplier = 1.25f;
        public float experienceGainedOnKill = 25f;

        /*
         * increase intensity with round number
         * increase multiplier
         */

        /* during buildup
         * increase the rate of spawned enemies
         * increase the size of waves spawned
         * until player stress has reached threshold
         * enter peak sustain state
         */

        [Header("Sustain Peak")]
        public float sustainPeakDuration = 4f;

        [Header("Relax")]
        public float relaxDuration = 5f;

        //[Header("Peak Fade")]
        //public float peakFadeDuration = 10f;
    }
}
