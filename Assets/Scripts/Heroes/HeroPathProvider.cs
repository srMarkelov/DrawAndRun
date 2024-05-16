using System.Collections.Generic;
using PathStd;
using UnityEngine;

namespace Heroes {
    public class HeroPathProvider : MonoBehaviour {
        [SerializeField] private PathConstructor PathConstructor;
        [SerializeField] private HeroMovement HeroExample;

        private List<HeroMovement> _heroes = new();
        private void Start() {
            for (var i = 0; i < ConfigConstants.StartEnemyCount; i++) {
                var hero = Instantiate(HeroExample);
                hero.gameObject.SetActive(true);
                hero.SetSpeed(ConfigConstants.StartSpeedHeroes);
                _heroes.Add(hero);
            }
        }

        private void OnEnable() {
            PathConstructor.OnPathUpdate += PathUpdate;
        }

        private void OnDisable() {
            PathConstructor.OnPathUpdate -= PathUpdate;
        }

        private void PathUpdate() {
            for (var i = 0; i < _heroes.Count; i++) {
                _heroes[i].SetTarget(PathConstructor.GetTarget(i));
            }
        }
    }
}
