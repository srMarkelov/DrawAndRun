using System;
using System.Collections.Generic;
using Drawer;
using Dreamteck.Splines;
using UnityEngine;

namespace PathStd {
    public class PathConstructor : MonoBehaviour {
        [SerializeField] private Vector3 Offset;
        [SerializeField] private SplineFollower PointExample;
        [SerializeField] private LineDrawerArea Area;
        [SerializeField] private SplineComputer SplineComputer;

        private List<SplineFollower> _points = new(); 
        
        public event Action OnPathUpdate;
    
        private void Start() {
            for (var i = 0; i < ConfigConstants.StartEnemyCount; i++) {
                var point = Instantiate(PointExample, Vector3.zero, Quaternion.identity);
                point.gameObject.SetActive(true);
                _points.Add(point);
            }
        }

        private void OnEnable() {
            Area.OnDrawLine += UpdateFormation;
        }

        private void OnDisable() {
            Area.OnDrawLine -= UpdateFormation;
        }

        private void UpdateFormation(List<Vector3> positions) {
            if(positions.Count < ConfigConstants.MinPointsToConstructPath)
                return;
        
            var points = new SplinePoint[positions.Count];
            for (var i = 0; i < positions.Count; i++) {
                points[i] = new SplinePoint(positions[i] + Offset);
            }
            SplineComputer.SetPoints(points);
            SetPointsPosition();
            OnPathUpdate?.Invoke();
        }

        private void SetPointsPosition() {
            for (var i = 0; i < _points.Count; i++) {
                _points[i].SetPercent(((float) i / _points.Count));
            }
        }

        public Transform GetTarget(int index) {
            return _points[index].transform;
        }
    }
}
