using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesPat
{
    class Wait : Instruction
    {
        float timeToWait;
        Func<float> getTimeToWait;
        public Wait(Func<float> getTimeToWait)
        {
            this.timeToWait = getTimeToWait();
            this.getTimeToWait = getTimeToWait;
        }

        public override IResult Execute(float dt)
        {
            timeToWait -= dt;
            if (timeToWait <= 0.0f)
                return IResult.Done;
            else
                return IResult.Running;
        }

        public override Instruction Reset()
        {
            return new Wait(getTimeToWait);
        }
    }
}
