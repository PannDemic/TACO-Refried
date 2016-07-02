namespace Taco.Classes
{
    class PathInfo
    {
        public int TotalJumps { get; set; }
        public int[] PathSystems { get; set; }
        public int FromSystem { get; set; }
        public int ToSystem { get; set; }

        public int PathId
        {
            get
            {
                return (FromSystem * 10000) + ToSystem;
            }
        }
    }
}
