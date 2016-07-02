using System.Collections.Generic;

using OpenTK;

namespace Taco.Classes
{
    class SolarSystemPathFinder
    {

        protected int Size;
        protected bool[] IsBlocked;
        private SolarSystemNode _startNode;

        public SolarSystemNode StartNode
        {
            get
            {
                return _startNode;
            }
        }

        private SolarSystemData[] _solarSystems;


        public SolarSystemPathFinder(SolarSystemData[] systemData)
        {
            Size = systemData.Length;
            IsBlocked = new bool[Size];
            _solarSystems = systemData;
        }
 
        protected double GetSquaredDistance(Vector2 start, Vector2 end)
        {
            Vector2 temp = end - start;
            return temp.LengthSquared;
        }

        public void SetBlocked(int index, bool value)
        {
            IsBlocked[index] = value;
        }

        public void SetBlocked(int index) { SetBlocked(index, true); }


        public PathInfo FindPath(int start, int end)
        {         
            return FindPathReversed(end, start);
        }
 
        private PathInfo FindPathReversed(int start, int end)
        {
            _startNode = new SolarSystemNode(start, new Vector2((float)_solarSystems[start].X, (float)_solarSystems[start].Y), 0, 0, _solarSystems[start].ConnectedTo, null);

            HeapPriorityQueue<SolarSystemNode> openList = new HeapPriorityQueue<SolarSystemNode>(Size);
            openList.Enqueue(_startNode, 0);
 
            bool[] brWorld = new bool[Size];
            brWorld[start] = true;

            //Vector2 endPosition = new Vector2((float)_solarSystems[end].X, (float)_solarSystems[end].Y);
 
            while (openList.Count != 0)
            {
                SolarSystemNode current = openList.Dequeue();

                if (current.SystemId == end)
                    return GetPathInfo(current);
                    //return new SolarSystemNode(end, endPosition, current.PathCost + 1, current.Cost + 1, _solarSystems[end].ConnectedTo, current);

                SolarSystemConnectionData[] surrounding = _solarSystems[current.SystemId].ConnectedTo;


                if (surrounding != null)
                {
                    foreach (var surroundingSystem in surrounding)
                    {
                        var tempId = surroundingSystem.ToSystemId;
                        var tmp = new Vector2((float)_solarSystems[tempId].X, (float)_solarSystems[tempId].Y);
                        var brWorldIdx = tempId;

                        if (!PositionIsFree(brWorldIdx) || brWorld[brWorldIdx]) continue;
                        brWorld[brWorldIdx] = true;

                        var pathCost = current.PathCost;
                        var cost = pathCost + 1;
                        var node = new SolarSystemNode(tempId, tmp, cost, pathCost, _solarSystems[tempId].ConnectedTo, current);
                        openList.Enqueue(node, cost);
                    }
                }
                else if (openList.Count == 0)
                {
                    return GetEmptyPath(_startNode.SystemId);
                }
            }
            return GetEmptyPath(_startNode.SystemId); //no path found
        }

        private PathInfo GetEmptyPath(int startingSystemId)
        {
            var tempInfo = new PathInfo
            {
                TotalJumps = 0,
                PathSystems = new[] {startingSystemId}
            };


            return tempInfo;
        }

        private PathInfo GetPathInfo(SolarSystemNode endNode)
        {
            var tempInfo = new PathInfo();

            var tempNode = endNode;

            var pathSystems = new List<int>();

            var i = 0;

            while (tempNode.HasParent)
            {
                pathSystems.Add(tempNode.SystemId);
                tempNode = tempNode.Parent;
                i++;
            }
            i++;

            var jumpCount = i;

            var pathIDs = new int[jumpCount + 1];

            i = 0;
            foreach (var tempId in pathSystems)
            {
                pathIDs[i] = tempId;
                i++;
            }
            i++;
            pathIDs[i] = _startNode.SystemId;

            tempInfo.PathSystems = pathIDs;
            tempInfo.TotalJumps = jumpCount;

            return tempInfo;
        }
 
        private bool PositionIsFree(int index)
        {
            return !IsBlocked[index];
        }
    }
}
