﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI.FSM
{
    public enum PatrolMode
    {
        //单次  123
        Once,

        //循环 123123
        Loop,

        //往返 12321
        PingPong,
    }
}
