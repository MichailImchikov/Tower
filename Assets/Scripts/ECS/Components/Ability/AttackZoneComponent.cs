using System.Collections.Generic;
using UnityEngine;

namespace Client {
    struct AttackZoneComponent 
    {
        public List<PointMap> pointAttack;
        public PointMap Center;
        public int Direction;
        public void TurnByDirection(int newDirection )
        {
            if (newDirection <= 0 || newDirection >= 5)
            {
                Debug.LogError("B ATTACK AREA ONLI 0,1,2,3,4,5");
                return;
            }
            while (newDirection!= Direction)
            {
                Direction++;
                if (Direction >= 5) Direction = 1;
                Trun();
            }
            Direction = newDirection;
        }
        public void Trun()
        {
            List<PointMap> rotatedArea = new List<PointMap>();
            foreach (PointMap point in pointAttack)
            {
                // —мещаем точку относительно центра
                var relativePoint = point.PointToMap - Center.PointToMap;

                // ѕоворачиваем точку на 90 градусов
                Vector3Int rotatedPoint = new Vector3Int(-relativePoint.y, relativePoint.x);

                // ¬озвращаем точку обратно в мировые координаты
                rotatedArea.Add(GameState.Instance.GetNewPoint(rotatedPoint + Center.PointToMap));
            }
            pointAttack = rotatedArea;
        }
        public void NewCenter(Vector3Int newCenter)
        {
            List<PointMap> newPointList = new List<PointMap>();
            var deltaVector = newCenter - Center.PointToMap;
            foreach(var point in pointAttack)
            {
                var newPosition = point.PointToMap + deltaVector;
                newPointList.Add(GameState.Instance.GetNewPoint(newPosition));
            }
            Center = GameState.Instance.GetNewPoint(newCenter);
            pointAttack = newPointList;
        }
    }
}