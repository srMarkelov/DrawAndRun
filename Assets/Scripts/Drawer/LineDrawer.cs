using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Drawer {
    public class LineDrawer : MonoBehaviour {
        [SerializeField] private Image PointTemplate;
        [SerializeField] private float TolleranceFromExport;
        [SerializeField] private Camera Camera;
        [SerializeField] private Canvas Canvas;
        [SerializeField] private Transform CanvasForPaint;

        private List<Image> DrawPoints = new(999);

        private void Start() {
            PointTemplate.gameObject.SetActive(false);
        }

        public void Draw(Vector3 position) {
            var positionConvertToCamera = position;
            positionConvertToCamera = new Vector3(positionConvertToCamera.x, positionConvertToCamera.y, 0);
            var newPoint = Instantiate(PointTemplate, CanvasForPaint);
            newPoint.rectTransform.localPosition = positionConvertToCamera;
            newPoint.gameObject.SetActive(true);
            DrawPoints.Add(newPoint);
        }

        public List<Vector3> GetPositions() {
            var result = new List<Vector3>(DrawPoints.Count);
            var lastPos = new Vector3();
            foreach (var drawPoint in DrawPoints) {
                RectTransformUtility.ScreenPointToWorldPointInRectangle((RectTransform)Canvas.transform, drawPoint.transform.localPosition, Camera, out var CanvasPoint);
                CanvasPoint = new Vector3(CanvasPoint.x, 0, CanvasPoint.y);
                if (Vector3.Distance(lastPos, CanvasPoint) > TolleranceFromExport) {
                    lastPos = CanvasPoint;
                    result.Add(CanvasPoint);
                }
            }

            return result;
        }

        public void Clear() {
            foreach (var point in DrawPoints) {
                Destroy(point.gameObject);
            }
            DrawPoints.Clear();
        }
    }
}