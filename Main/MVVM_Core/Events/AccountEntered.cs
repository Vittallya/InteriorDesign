﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Main.MVVM_Core
{
    public class AccountEntered : IEvent
    {
        public string Name { get; set;  }
    }
}
