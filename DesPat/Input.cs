using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesPat
{
    public interface Input
    {
        bool quit { get; }

        bool up { get; }
        bool left { get; }
        bool down { get; }
        bool right { get; }
        bool shooting { get; }

        void vibrate();
        void update();
    }
}
