namespace Assignment4
{
    public static class CollisionDetector
    {
        /// <summary>
        /// Checks for AABB collisions (overlapping)
        /// </summary>
        /// <param name="a">First object</param>
        /// <param name="b">Second object</param>
        /// <returns></returns>
        public static bool Overlaps(IPhysicalObject2D a, IPhysicalObject2D b)
        {
            return ((a.X < b.X + b.Width) && (a.X + a.Width > b.X) && (a.Y < b.Y + b.Height) && (a.Height + a.Y > b.Y));
        }
    }
}
