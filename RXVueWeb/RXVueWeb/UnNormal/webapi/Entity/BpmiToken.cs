﻿using System;
using System.Collections.Generic;

namespace webapi.Entity;

public partial class BpmiToken
{
    public string Tokenid { get; set; } = null!;

    public string? Tokenname { get; set; }

    public int? Elementtype { get; set; }

    public string? Taskid { get; set; }

    public string? Activityid { get; set; }

    public string? Actorid { get; set; }

    public string? Transitionid { get; set; }

    public string? Result { get; set; }

    public string? Formid { get; set; }

    public int? Tokenstatus { get; set; }

    public DateTime? Receivetime { get; set; }

    public DateTime? Finishtime { get; set; }

    public DateTime? Lastnotifytime { get; set; }

    public DateTime? Expiretime { get; set; }

    public decimal? Notifyfrequence { get; set; }

    public string? Remark { get; set; }

    public byte? Isrecede { get; set; }

    public string? Curusername { get; set; }

    public byte? Progressstatus { get; set; }

    public decimal? Standardtime { get; set; }

    public string Systemid { get; set; } = null!;

    public string? Curdeptname { get; set; }

    public int Recyclelevel { get; set; }

    public string? Fromaccount { get; set; }

    public string? Fromusername { get; set; }

    public byte Showinlist { get; set; }

    public string? Processcode { get; set; }
}
