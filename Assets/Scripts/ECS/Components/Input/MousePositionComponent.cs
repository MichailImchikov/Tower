
using UnityEngine;

namespace Client {
    struct MousePositionComponent {
        public Vector3 position;
        public PointMap pointMap => GameState.Instance.GetNewPoint(position); 
    }
}