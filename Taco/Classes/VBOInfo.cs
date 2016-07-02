namespace Taco.Classes
{
    class VboInfo
    {
        public int SystemVbo = 0;
        public int SystemVao = 0;
        public int ColorVao = 0;

        public int ConnectionVbo = 0;
        public int ConnectionVao = 0;
        public int ConnectionColor = 0;

        public int TextQuadVbo = 0;
        public int TextQuadVao = 0;
        public int TextQuadColor = 0;

        public int TextLineVbo = 0;
        public int TextLineVao = 0;
        public int TextLineColor = 0;

        public bool AllVbOsGenerated
        {
            get
            {
                return (
                    (SystemVbo > 0) && 
                    (SystemVao > 0) && 
                    (ColorVao > 0) && 
                    (ConnectionVbo > 0) && 
                    (ConnectionVao > 0) && 
                    (ConnectionColor > 0) &&
                    (TextQuadVbo > 0) &&
                    (TextQuadVao > 0) &&
                    (TextQuadColor > 0) &&
                    (TextLineVbo > 0) &&
                    (TextLineVao > 0) &&
                    (TextLineColor > 0));
            }
        }
    }
}
