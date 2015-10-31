﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Styx.GromHSCR.Api.Interfaces
{
    public interface IValidationResult : IBaseItem
    {
        Guid PrintInfoId { get; set; }

        IPrintInfo PrintInfo { get; set; }

        string Comment { get; set; }

        DateTime ValidationDateTime { get; set; }
    }
}