using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesPat
{
    class Next : Instruction
    {
        Instruction One, Two;
        bool isOneDone = false, isTwoDone = false;
        public Next(Instruction One, Instruction Two)
        {
            this.One = One;
            this.Two = Two;
        }

        public override IResult Execute(float dt)
        {
            if (!isOneDone)
            {
                var Oneres = One.Execute(dt);
                switch (Oneres)
                {
                    case IResult.Done:
                        isOneDone = true;
                        return IResult.Running;
                    case IResult.DoneAndCreate:
                        isOneDone = true;
                        return IResult.RunningAndCreate;
                    default:
                        return Oneres;
                }
            }
            else
            {
                if (!isTwoDone)
                {
                    var Twores = Two.Execute(dt);
                    switch (Twores)
                    {
                        case IResult.Done:
                            isTwoDone = true;
                            break;
                        case IResult.DoneAndCreate:
                            isTwoDone = true;
                            break;
                    }
                    return Twores;
                }
                else
                {
                    return IResult.Done;
                }
            }
        }

        public override Instruction Reset()
        {
                return new Next(One.Reset(), Two.Reset());
        }
    }
}
