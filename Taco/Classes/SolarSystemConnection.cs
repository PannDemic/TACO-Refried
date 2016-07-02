namespace Taco.Classes
{
    class SolarSystemConnection
    {
        public SolarSystemConnection(int toSystemId, int toSystemNativeId, bool isRegional)
        {
            ToSystemId = toSystemId;
            ToSystemNativeId = toSystemNativeId;
            IsRegional = isRegional;
        }

        public int ToSystemId { get; set; }
        public int ToSystemNativeId { get; set; }
        public bool IsRegional { get; set; }
    }
}
