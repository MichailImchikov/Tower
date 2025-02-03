using System.Collections.Generic;

namespace Client {
    struct MoveToPointComponent {
        public List<PointMap> WayToPoint;
        public int IndexCurrentPoint;
        public PointMap CurrentPoint()
        {
            return WayToPoint[IndexCurrentPoint];
        }
        public bool NextPoint()
        {
            IndexCurrentPoint++;
            if (IndexCurrentPoint <= WayToPoint.Count - 1)
                return true;
            return false;
        }
    }
}