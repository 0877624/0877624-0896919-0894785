using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesPat
{
    class Repeat : Instruction
    {
        Instruction body;
        public Repeat(Instruction body)
        {
            this.body = body;
        }

        public override IResult Execute(float dt)
        {
            switch (body.Execute(dt))
            {
                case IResult.Done:
                    body = body.Reset();
                    return IResult.Running;
                case IResult.DoneAndCreate:
                    body = body.Reset();
                    return IResult.RunningAndCreate;
                case IResult.Running:
                    return IResult.Running;
                case IResult.RunningAndCreate:
                    return IResult.RunningAndCreate;
            }
            return IResult.Running;
        }

        public override Instruction Reset()
        {
            return new Repeat(body.Reset());
        }
    }
}

