﻿ using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using webapi.Bean.FabRep;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/Spc")]
    [ApiController]
    public class SpcLotController : Controller
    {
        private BeforeRep num;

        [HttpPost("SpcLot")]

        public List<BeforeRep> GetDepart()
        {
            List<BeforeRep> result = new List<BeforeRep>();
            using (Rxrept2Context ctx = new Rxrept2Context())
            {
                var list = ctx.RepActlHs
                    .FromSqlInterpolated($@"select * from rep_actl_h A where PRIORITY>=1100 AND PRIORITY<=1300  OR PRIORITY=4500")
                    .ToList();
                foreach (var i in list)
                {
                    BeforeRep num = new BeforeRep();
                    num.Lotid = i.Lotid;
                    num.Partname = i.Partname;
                    num.Partid = i.Partid;
                    num.Createtime = i.Createtime.ToString().Replace("T", " ");
                    num.Starttime = i.Starttime.ToString().Replace("T", " ");
                    num.Curmainqty = i.Curmainqty;
                    num.Priority = (int)(i.Priority);
                    num.Location = i.Location;
                    num.Stage = i.State;
                    num.outdate = i.Outdate.ToString().Replace("T", " ");


                    //string sql = " select t1.*  from rep_hist_tot t1, rep_actl_all t2 where t1.lotid = t2.lotid(+) and t1.lotid = '"+num.Lotid+"'order by t1.trackouttime ";
                    ////FormattableString sql = @$" select t1.*  from rep_hist_tot t1, rep_actl_all t2 where t1.lotid = t2.lotid(+) and t1.lotid = {0}order by t1.trackouttime ";
                    //var NowStep = ctx.RepHistTots.FromSqlRaw(sql).ToList();
                    //num.NowStep = NowStep.Count;
                    //num.CapaGroup = NowStep.Count+"_"+i.Stage+"_"+ i.CapaGroup;
                    num.NowStep = int.Parse(i.Curprcdcurinstnum.Replace("R","")) /10;
                    num.CapaGroup = int.Parse(i.Curprcdcurinstnum.Replace("R",""))/10+ "_"+i.Stage+"_"+ i.Flag;
                    using (RxmesContext ctx2 = new RxmesContext())
                    {
                        double number = 0;
                        string sql2 = "SELECT C.* FROM FW_PROCESS_SPEC C ,(SELECT NAME,ACTIVEVERSION FROM FWPROCESSPLAN WHERE SYSID = ((SELECT B.TOID FROM FWPRODUCTVERSION_N2M B WHERE FROMID = (SELECT C.SYSID FROM FWPRODUCTVERSION C WHERE PRODUCTNAME = '"+num.Partname+"') AND B.LINKNAME = 'processes')))D WHERE C.PROCESS = D.NAME AND C.PROCESSVER = D.ACTIVEVERSION ORDER BY C.STEPSEQ ASC";
                        var TotalStep = ctx2.FwProcessSpecs.FromSqlRaw(sql2).ToList();
                        num.Totalstep = TotalStep.Count;

                        //计算spd 时间
                        if (num.NowStep != 0) {
                        string sqll= "SELECT C.* FROM FW_PROCESS_SPEC C ,(SELECT NAME,ACTIVEVERSION FROM FWPROCESSPLAN WHERE SYSID = ((SELECT B.TOID FROM FWPRODUCTVERSION_N2M B WHERE FROMID = (SELECT C.SYSID FROM FWPRODUCTVERSION C WHERE PRODUCTNAME = '"+num.Partname+"') AND B.LINKNAME = 'processes')))D WHERE C.PROCESS = D.NAME AND C.PROCESSVER = D.ACTIVEVERSION AND stepseq <='" + num.NowStep.ToString() +"' ORDER BY C.STEPSEQ ASC;";
                        var gg = ctx2.FwProcessSpecs.FromSqlRaw(sqll).ToList();

                            foreach (var nc in gg)
                            {
                                string NCSQL = "select * from cust_eqp_spt_data  where stepname ='" + nc.Recipe + "'";
                                var sptd = ctx2.CustEqpSptData.FromSqlRaw(NCSQL).ToList();
                                if (sptd.Count != 0)
                                { 
                                    if (num.Priority == 1100)
                                        number += double.Parse(sptd[0].Spt)*1.5;
                                    else if (num.Priority == 1200)
                                        number += double.Parse(sptd[0].Spt)*2.3;
                                    else
                                        number += double.Parse(sptd[0].Spt)*3;
                                }
                            }

                        }
                        DateTime dateTime;
                        if (DateTime.TryParseExact(num.Starttime, "yyyy/M/d HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                        {
                             num.Starttime = dateTime.ToString("yyyy/MM/dd HH:mm:ss");
                        }
                        num.endline = DateTime.ParseExact(num.Starttime, "yyyy/M/d H:mm:ss", CultureInfo.InvariantCulture).AddMinutes(number * 1.5).ToString("yyyy-MM-dd HH:mm:ss");
                     


                        string time = "select * from (select * from FWWIPTRANSACTION where WIPID='"+num.Lotid+"' order by TXNTIMESTAMP DESC ) where ROWNUM=1 ";
                        var time1 = ctx2.Fwwiptransactions.FromSqlRaw(time).ToList();
                        var nowtime = time1[0].Txntimestamp.ToString();
                        string time2 = "select * from (select * from FWWIPTRANSACTION where WIPID='" + num.Lotid + "'order by TXNTIMESTAMP DESC ) where   TXNTIMESTAMP<='" + nowtime + "' AND  ROWNUM=1";
                        var time3= ctx2.Fwwiptransactions.FromSqlRaw(time2).ToList();
                        var beforetime = time3[0].Txntimestamp.ToString();

                        DateTime data = DateTime.ParseExact(beforetime, "yyyyMMdd HHmmssfff", null);
                        TimeSpan timeSpan = DateTime.Now - data;
                        double hours = timeSpan.TotalHours;
                        num.nexttime = Math.Round(hours,2);

                    }
                    num.RemStep = num.Totalstep - num.NowStep;

                    num.bfb = string.Format("{0:F2}%",(((float)num.NowStep / num.Totalstep) * 100));

                    result.Add(num);
                }
            }

            return result;
        }
    }
}
