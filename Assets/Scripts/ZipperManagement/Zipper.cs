namespace ZipTheZipper.ZipperManagement
{
    public struct Zipper
    {
        public ZipperPin[] LeftSidePins;
        public ZipperPin[] RightSidePins;

        public Zipper(int zipperLength)
        {
            LeftSidePins = new ZipperPin[zipperLength];
            RightSidePins = new ZipperPin[zipperLength];
        }
    }
}