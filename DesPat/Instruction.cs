using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesPat
{
    abstract class Instruction
    {
        public abstract IResult Execute(float dt);
        public abstract Instruction Reset();

        public static Instruction operator +(Instruction One, Instruction Two)
        {
            return new Next(One, Two);
        }

    }
}
