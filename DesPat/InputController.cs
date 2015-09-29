using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesPat
{
    class InputController : Input
    {
        PlayerIndex playerIndex;
        GamePadState GPS;
        

        public InputController(PlayerIndex playerIndex)
        {
            this.playerIndex = playerIndex;
            this.GPS = GamePad.GetState(playerIndex);
        }

        public bool quit
        {
            get
            {
                return GPS.Buttons.Back == ButtonState.Pressed;
            }
        }

        public bool up
        {
            get
            {
                return GPS.ThumbSticks.Left.Y > 0f;
            }
        }

        public bool left
        {
            get
            {
                return GPS.ThumbSticks.Right.X < 0f;
            }
        }

        public bool down
        {
            get
            {
                return GPS.ThumbSticks.Left.Y < 0f;
            }
        }

        public bool right
        {
            get
            {
                return GPS.ThumbSticks.Right.X > 0f;
            }
        }

        public bool shooting
        {
            get
            {
                return GPS.Triggers.Right == 1.0f;
            }
        }

        public void update()
        {
            GPS = GamePad.GetState(playerIndex);
            
        }
    }
}
