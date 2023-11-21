﻿using System;
using System.Collections.Generic;

namespace webapi.Model;

public partial class RepEqpsStartEnd
{
    public string? Eqpid { get; set; }

    public string? Eqptyp { get; set; }

    public string? Status { get; set; }

    public DateTime Changedt { get; set; }

    public DateTime? Enddate { get; set; }

    public decimal? Usetime { get; set; }

    public DateTime? Repdate { get; set; }

    public string? Iscr { get; set; }
}
