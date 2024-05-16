using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Drawer {
    public class LineDrawerArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler {
        [SerializeField] private LineDrawer LineDrawer;
        
        private bool _isPressed = false;
        
        public event Action<List<Vector3>> OnDrawLine;
        public void OnPointerDown(PointerEventData eventData) {
            _isPressed = true;
            LineDrawer.Draw(eventData.position);
        }

        public void OnPointerUp(PointerEventData eventData) {
            _isPressed = false;
            OnDrawLine?.Invoke(LineDrawer.GetPositions());
            LineDrawer.Clear();
        }

        public void OnPointerMove(PointerEventData eventData) {
            if(_isPressed == false)
                return;
        
            LineDrawer.Draw(eventData.position);
        }
    }
}
