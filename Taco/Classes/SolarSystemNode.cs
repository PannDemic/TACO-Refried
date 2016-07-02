using OpenTK;

namespace Taco.Classes
{
    class SolarSystemNode : PriorityQueueNode
    {
        private SolarSystemNode _parent;

        public int SystemId { get; set; }

        public Vector2 Position { get; set; }

        public SolarSystemConnectionData[] ConnectTo { get; set; }

        public SolarSystemNode Parent { get { return _parent; } set { _parent = value; } }

        public double Cost { get; set; }

        public double PathCost { get; set; }

        public bool HasParent
        {
            get
            {
                if (_parent != null)
                    return true;
                
                return false;
            }
        }

        public SolarSystemNode(int systemId, Vector2 position, double cost, double pathCost, SolarSystemConnectionData[] connectedTo, SolarSystemNode parent)
        {
            SystemId = systemId;
            Position = position;
            Cost = cost;
            PathCost = pathCost;
            ConnectTo = connectedTo;
            _parent = parent;
        }
    }
}
