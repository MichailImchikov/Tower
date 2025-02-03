using System.Collections.Generic;

namespace Client {
    struct AreaWalkingComponent {
        public Dictionary<PointMap, int> areaWalking;
        public Dictionary<PointMap, List<PointMap>> obstacles;
    }
}