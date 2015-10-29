using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesPat
{
    class CreateMeatball : Instruction
    {
        public override IResult Execute(float dt)
        {
            return IResult.DoneAndCreate;
        }

        public override Instruction Reset()
        {
            return new CreateMeatball();
        }
    }
}
