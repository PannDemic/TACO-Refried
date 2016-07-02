using System;
using System.Drawing;

namespace Taco.Classes
{
    class Utility
    {
        public static int ColorToRgba32(Color c)
        {
            return ((c.A << 24) | (c.B << 16) | (c.G << 8) | c.R);
        }
    }

    class IntelBuffer
    {
        public string Prefix { get; set; }
        public string LogLine { get; set; }
        public bool ParseForSystemLinks { get; set; }
    }

    class IntelUpdate
    {
        public LogEntry Entry { get; set; }
        public int PathId { get; set; }
        public IntelUpdateType Type { get; set; }
        public int FromSystem { get; set; }
        public int ToSystem { get; set; }
    }

    class SystemStats
    {
        public SystemStats(int systemId = -1)
        {
            SystemId = systemId;
            ReportCount = 1;
            LastReport = DateTime.Now;
            Expired = false;
        }

        public int SystemId { get; set; }
        public int ReportCount { get; set; }
        public DateTime LastReport { get; set; }
        public bool Expired { get; set; }

        public void Update()
        {
            LastReport = DateTime.Now;
            ReportCount++;
            Expired = false;
        }
    }

    enum IntelUpdateType
    {
        Home,
        Ranged,
        Game
    }

    enum RangeAlertOperatorv1
    {
        LessThanOrEqual,
        Equal
    }

    enum RangeAlertOperator
    {
        Equal,
        LessThan,
        GreaterThan,
        LessThanOrEqual,
        GreaterThanOrEqual
    }

    enum AlertType
    {
        Ranged,
        Custom
    }

    enum RangeAlertType
    {
        Home,
        System,
        Character,
        AnyCharacter,
        None
    }

    public enum eType
    {
        Boolean,
        Int,
        IntEnum,
        IntArray2,
        IntArray4,
        Float,
        FloatArray2,
        FloatArray4,
    }
}
