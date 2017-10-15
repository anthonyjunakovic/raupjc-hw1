using Microsoft.Xna.Framework;
using System;

namespace Assignment4
{
    /// <summary>
    /// Contains all valid ball directions, and they can be multiplied with a Vector2
    /// </summary>
    public class BallDirection
    {
        /// <summary>
        /// Direction vector of (-1, 1) value
        /// </summary>
        public static BallDirection NorthWest = new BallDirection(new Vector2(-1, 1));
        /// <summary>
        /// Direction vector of (1, 1) value
        /// </summary>
        public static BallDirection NorthEast = new BallDirection(new Vector2(1, 1));
        /// <summary>
        /// Direction vector of (-1, -1) value
        /// </summary>
        public static BallDirection SouthWest = new BallDirection(new Vector2(-1, -1));
        /// <summary>
        /// Direction vector of (1, -1) value
        /// </summary>
        public static BallDirection SouthEast = new BallDirection(new Vector2(1, -1));

        private Vector2 vector;

        private BallDirection(Vector2 vector)
        {
            this.vector = vector;
        }

        /// <summary>
        /// Turns the vector horizontally to opposite direction
        /// </summary>
        public void TurnHorizontal()
        {
            vector.X *= -1.0F;
        }

        /// <summary>
        /// Turns the vector vertically to opposite direction
        /// </summary>
        public void TurnVertical()
        {
            vector.Y *= -1.0F;
        }

        public static Vector2 operator *(Vector2 vector, BallDirection direction)
        {
            return vector * direction.vector;
        }

        public static Vector2 operator *(BallDirection direction, Vector2 vector)
        {
            return vector * direction.vector;
        }

        public static Vector2 operator /(Vector2 vector, BallDirection direction)
        {
            return vector / direction.vector;
        }

        public static Vector2 operator /(BallDirection direction, Vector2 vector)
        {
            return vector / direction.vector;
        }
    }

    /// <summary >
    /// Game ball object representation
    /// </ summary >
    public class Ball : Sprite
    {
        /// <summary>
        /// Defines mas ball speed
        /// </summary>
        public float MaxSpeed;
        /// <summary >
        /// Defines current ball speed in time.
        /// </ summary >
        public float Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                if (value > MaxSpeed)
                {
                    _speed = MaxSpeed;
                } else
                {
                    _speed = value;
                }
            }
        }

        private float _speed;

        public float BumpSpeedIncreaseFactor { get; set; }

        /// <summary >
        /// Defines ball direction .
        /// Valid values ( -1 , -1) , (1 ,1) , (1 , -1) , ( -1 ,1).
        /// Using Vector2 to simplify game calculation . Potentially
        /// dangerous because vector 2 can swallow other values as well .
        /// OPTIONAL TODO : create your own , more suitable type
        /// </ summary >
        public BallDirection Direction { get; set; }

        public Ball(int size, float speed, float defaultBallBumpSpeedIncreaseFactor, float MaxSpeed = GameConstants.MaxBallSpeed) : base(size, size)
        {
            this.MaxSpeed = MaxSpeed;
            Speed = speed;
            BumpSpeedIncreaseFactor = defaultBallBumpSpeedIncreaseFactor;
            // Initial direction
            Direction = BallDirection.NorthEast;
        }
    }

}
