using UnityEngine;
using _25_06_04_AR_First.Mapping;

namespace _25_06_04_AR_First.Database
{
    public class Monster
    {
        public MapLocation location;
        public Vector3 position;
        public double spawnTimestamp;
        public double lastHeardTimestamp;
        public double lastSeenTimestamp;
        public GameObject gameObject;
        public int footstepRange;
    }
}