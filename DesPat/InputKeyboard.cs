using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesPat
{
    class InputKeyboard : Input
    {
        KeyboardState KBS = Keyboard.GetState();
        Keys quitKey;
        Keys upKey;
        Keys leftKey;
        Keys downKey;
        Keys rightKey;
        Keys shootKey;

        public InputKeyboard(Keys quitKey, Keys upKey, Keys leftKey, Keys downKey, Keys rightKey, Keys shootKey)
        {
            this.quitKey = quitKey;
            this.upKey = upKey;
            this.leftKey = leftKey;
            this.downKey = downKey;
            this.rightKey = rightKey;
            this.shootKey = shootKey;
        }

        public bool quit
        {
            get
            {
                return KBS.IsKeyDown(quitKey);
            }
        }

        public bool up
        {
            get
            {
                return KBS.IsKeyDown(upKey);
            }
        }

        public bool left
        {
            get
            {
                return KBS.IsKeyDown(leftKey);
            }
        }

        public bool down
        {
            get
            {
                return KBS.IsKeyDown(downKey);
            }
        }

        public bool right
        {
            get
            {
                return KBS.IsKeyDown(rightKey);
            }
        }

        public bool shooting
        {
            get
            {
                return KBS.IsKeyDown(shootKey);
            }
        }

        public void update()
        {
            KBS = Keyboard.GetState();
        }

        public void vibrate()
        {
            //
        }
    }
}
