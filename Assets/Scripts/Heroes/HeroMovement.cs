using UnityEngine;

namespace Heroes {
    public class HeroMovement : MonoBehaviour {
        private float _tollerance = 0;
        private float _speed;
        private Transform _target;
        public void SetSpeed(float speed) {
            _speed = speed;
        }
        public void SetTarget(Transform target) {
            _target = target;
        }
    
        private void Update() {
            if(_target == null)
                return;
        
            if(Vector3.Distance(transform.position, _target.position) > _tollerance)
                transform.position = Vector3.Lerp(transform.position, _target.position, Time.deltaTime * _speed);
        }
    }
}
