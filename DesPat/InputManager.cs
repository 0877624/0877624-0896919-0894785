using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesPat
{
    class InputManager : Input
    {
        Input inputOne;
        Input inputTwo;

        public InputManager(Input inputOne, Input inputTwo)
        {
            this.inputOne = inputOne;
            this.inputTwo = inputTwo;
        }

        public bool quit
        {
            get
            {
                return (inputOne.quit || inputTwo.quit);
            }
        }

        public bool up
        {
            get
            {
                return (inputOne.up || inputTwo.up);
            }
        }

        public bool left
        {
            get
            {
                return (inputOne.left || inputTwo.left);
            }
        }

        public bool down
        {
            get
            {
                return (inputOne.down || inputTwo.down);
            }
        }

        public bool right
        {
            get
            {
                return (inputOne.right || inputTwo.right);
            }
        }

        public bool shooting
        {
            get
            {
                return (inputOne.shooting || inputTwo.shooting);
            }
        }

        public void update()
        {
            inputOne.update();
            inputTwo.update();
        }
        public void vibrate()
        {
            inputOne.vibrate();
            inputTwo.vibrate();
        }
    }
}
