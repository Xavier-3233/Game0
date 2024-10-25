using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using static Game0.Screens.Breakdown;

namespace Game0
{
    public class GameState
    {
        public SerializableVector2 BallPosition { get; set; }
        public SerializableVector2 BallVelocity { get; set; }
        public SerializableVector2 PaddlePosition { get; set; }
        public SerializableVector2[] BrickPositions { get; set; }
        public List<bool> BrickHits { get; set; }
        public int BricksLeft { get; set; }
        public double CurrentTime { get; set; }
        public bool BallHitBottom { get; set; }
    }


}
