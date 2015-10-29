using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesPat
{
    class For : Instruction
    {
        int start, end, i;
        Func<int, Instruction> getBody;
        Instruction body;
        public For(int start, int end, Func<int, Instruction> getBody)
        {
            this.i = start;
            this.start = start;
            this.end = end;
            this.getBody = getBody;
            this.body = getBody(i);
        }

        public override IResult Execute(float dt)
        {
            if (i >= end)
                return IResult.Done;
            else
            {
                switch (body.Execute(dt))
                {
                    case IResult.Done:
                        i++;
                        body = getBody(i);
                        return IResult.Running;
                    case IResult.DoneAndCreate:
                        i++;
                        body = getBody(i);
                        return IResult.RunningAndCreate;
                    case IResult.Running:
                        return IResult.Running;
                    case IResult.RunningAndCreate:
                        return IResult.RunningAndCreate;
                }
                return IResult.Done;
            }
        }

        public override Instruction Reset()
        {
            return new For(start, end, getBody);
        }
    }
}
