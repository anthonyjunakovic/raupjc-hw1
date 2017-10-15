namespace Assignment4
{
    public class Wall : IPhysicalObject2D
    {
        public Paddle Player { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Wall(Paddle player, float x, float y, int width, int height)
        {
            Player = player;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }
}
