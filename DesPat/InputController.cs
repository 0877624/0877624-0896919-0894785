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

        public static SharpDX.XInput.Controller controller;
        public static SharpDX.XInput.Vibration vibration = new SharpDX.XInput.Vibration();
        bool doVibrate;
        float vibrateTime = 0.5f;
        float vibrateTimeLeft;

        public InputController(PlayerIndex playerIndex)
        {
            this.playerIndex = playerIndex;
            if (playerIndex == PlayerIndex.One)
            {
                controller = new SharpDX.XInput.Controller(SharpDX.XInput.UserIndex.One);
            }
            if (playerIndex == PlayerIndex.Two)
            {
                controller = new SharpDX.XInput.Controller(SharpDX.XInput.UserIndex.Two);
            }
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

        public void vibrate()
        {
            if (GamePad.GetState(playerIndex).IsConnected)
            {
                System.Diagnostics.Debug.Print("PlayerIndex: " + playerIndex + ", IS GAMEPAD CONNECTED");
            }
            if (controller.IsConnected)
            {
                System.Diagnostics.Debug.Print("PlayerIndex: " + playerIndex + ", IS CONTROLLER CONNECTED");

                vibrateTimeLeft = vibrateTime;
                doVibrate = true;
            }
        }
        public void update(float deltaTime)
        {
            GPS = GamePad.GetState(playerIndex);
            if(doVibrate)
            {
                vibrateTimeLeft -= deltaTime;

                vibration.LeftMotorSpeed = 65000;
                vibration.RightMotorSpeed = 65000;
                controller.SetVibration(vibration);
                if(vibrateTimeLeft <= 0)
                {
                    doVibrate = false;
                    vibration.LeftMotorSpeed = 0;
                    vibration.RightMotorSpeed = 0;
                    controller.SetVibration(vibration);
                }
            }
        }
    }
}
