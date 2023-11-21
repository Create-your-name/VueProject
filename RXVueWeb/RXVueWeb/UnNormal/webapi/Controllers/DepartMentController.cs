﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using webapi.Models;
using webapi.Bean;
using static webapi.Bean.FormData;
using webapi.Entity;
using webapi.Util;
using webapi.Model;

namespace webapi.Controllers
{
    [Route("api/DepartMent")]
    [ApiController]
    public class DepartMentController : ControllerBase
    {

        private readonly ModelContext _context;

        [HttpGet]
        public List<FromShow> GetAll()
        {
            using (Models.ModelContext ctx = new Models.ModelContext())
            {
                var Department = ctx.RxFormProductUnusuals
                                    .Where(e => String.IsNullOrEmpty(e.No) ? false : true)
                                    .ToList();

                List<FromShow> rusult = new List<FromShow>();
                String[] strings = new string[] { };
                String[] strings2 = new string[] { };
                Data data = new Data("", "", "", "", "", "", "", "", "", "", "", "", "", "","","","","","","","","","");

                rusult = SelectFormData(Department ,data  );

                return rusult;
            }
        }

        [HttpPost("Lot")]
        public List<FromShow> GetLotALL(Bean.FormData.Data Data)
        {
            using (Models.ModelContext ctx = new Models.ModelContext())
            {

                var Test = ctx.BpmiTokenlasts.ToList();

                var Find_DePartMent = ctx.RxFormProductUnusuals
               .Join(ctx.BpmiTokens, x => x.Taskid, y => y.Taskid, (x, y) => x).Distinct()
                .Where(e => String.IsNullOrEmpty(e.No) ? false : true)
                .Where(e => String.IsNullOrEmpty(Data.f_Cause_Range) ? true : e.Ycyygl == Data.f_Cause_Range)
                .Where(e => String.IsNullOrEmpty(Data.f_Equip_ID) ? true : e.Equipid == Data.f_Equip_ID)
                .Where(e => String.IsNullOrEmpty(Data.f_Idea) ? true : e.CpownerQryj == Data.f_Idea)
                .Where(e => String.IsNullOrEmpty(Data.f_LOT) ? true : e.Ph == Data.f_LOT)
                .Where(e => String.IsNullOrEmpty(Data.f_OP_Class) ? true : e.YcclFl == Data.f_OP_Class)
                .Where(e => String.IsNullOrEmpty(Data.f_OP_Depart) ? true : e.YcclBm == Data.f_OP_Depart)
                .Where(e => String.IsNullOrEmpty(Data.f_OP_Level) ? true : e.Ycclcc == Data.f_OP_Level)
                .Where(e => String.IsNullOrEmpty(Data.f_OP_Pheno) ? true : e.Ycms == Data.f_OP_Pheno)
                .Where(e => String.IsNullOrEmpty(Data.f_Pro) ? true : e.Gy == Data.f_Pro)
                .Where(e => String.IsNullOrEmpty(Data.f_Pro_Kind) ? true : true)
                .Where(e => String.IsNullOrEmpty(Data.f_Product_name) ? true : e.Cppz == Data.f_Product_name)
                .Where(e => String.IsNullOrEmpty(Data.f_Res_Depart) ? true : e.Zrbm1 == Data.f_Res_Depart)
                .Where(e => String.IsNullOrEmpty(Data.f_Res_Depart_2) ? true : e.Zrbm2 == Data.f_Res_Depart_2)
                .Where(e => String.IsNullOrEmpty(Data.f_Yc_Odd) ? true : e.No == Data.f_Yc_Odd)
                 .ToList();

                /*  modelBuilder.Entity<RxFormProductUnusual>().HasMany(T => T.bpmiTokens);*/

                /* var Find_DePartMent = ctx.RxFormProductUnusuals
                         .Include(t => t.bpmiTokens.Select(c => c.Taskname))

                  .ToList();*/

                List<FromShow> rusult2 = new List<FromShow>();

                rusult2 = SelectFormData(Find_DePartMent, Data);
                return rusult2;
            }
        }
	[HttpPost("Depart")]

        public List<Depart> GetDepart(Bean.FormData.Data Data)
        {
            List<Depart> rusult = new List<Depart>();
            List<FromShow> u = new List<FromShow>();
            u = GetLotALL(Data);
            foreach (var i in u) {
                Depart r = new Depart(i.No,"",i.Location,i.C1,i.C2,i.C3,i.C4,"",i.YcclBm,i.Z1,i.Z2,i.Z3,i.Z4,"","");
                rusult.Add(r);
            }
            return rusult;
        }

        private List<FromShow> SelectFormData(List<RxFormProductUnusual> ForFrom , Data data) {
            String[] Property = { "Ypcl1", "Ypcl2", "Ypcl3", "Ypcl4", "Ypcl5", "Ypcl6", "Ypcl7", "Ypcl8",
                "Ypcl9" , "Ypcl10" ,"Ypcl11" , "Ypcl12" ,"Ypcl13" ,"Ypcl14" ,"Ypcl15" ,"Ypcl16" ,"Ypcl17" ,"Ypcl18","Ypcl19" ,"Ypcl20",
                "Ypcl21" ,"Ypcl22" ,"Ypcl23" , "Ypcl24" ,"Ypcl25"};

            String[] Property2 = { "Yczcl1", "Yczcl2", "Yczcl3", "Yczcl4", "Yczcl5", "Yczcl6", "Yczcl7", "Yczcl8",
                "Yczcl9" , "Yczcl10" ,"Yczcl11" , "Yczcl12" ,"Yczcl13" ,"Yczcl14" ,"Yczcl15" ,"Yczcl16" ,"Yczcl17" ,"Yczcl18","Yczcl19" ,"Yczcl20",
                "Yczcl21" ,"Yczcl22" ,"Yczcl23" , "Yczcl24" ,"Yczcl25"};

            List<FromShow> rusult2 = new List<FromShow>();
            int[] value = { 0, 0, 0, 0 };
            using (ModelContext ctx =new ModelContext()) {

             var Time = ctx.BpmiTasks
              .Where(e => String.IsNullOrEmpty(data.activityBeginTime)? true : e.Begintime > Convert.ToDateTime(data.activityBeginTime))
              .Where(e => String.IsNullOrEmpty(data.activityEndTime) ? true : e.Begintime < Convert.ToDateTime(data.activityEndTime))
               .Where(e => String.IsNullOrEmpty(data.activityBeginTime2) ? true : e.Finishtime > Convert.ToDateTime(data.activityBeginTime2))
                .Where(e => String.IsNullOrEmpty(data.activityEndTime2) ? true : e.Finishtime < Convert.ToDateTime(data.activityEndTime2)).ToList();


                List<RxFormProductUnusual> rusult = new List<RxFormProductUnusual>();
                foreach (var i in ForFrom)
                    foreach (var y in Time)
                        if (i.Taskid == y.Taskid)
                            rusult.Add(i);

                rusult = rusult.Distinct().ToList();
                foreach (var Depart in rusult) if (!String.IsNullOrEmpty(Depart.No)) Depart.No += "_异常单";
                foreach (var i in rusult)
                {
                    rusult2.Add(AutoCopy(i));
                }
                foreach (var i in rusult2)
                {
                    value = BeanUtil.ForeachClassProperties(i, Property);

                    i.C1 = value[0] + "";
                    i.C2 = value[1] + "";
                    i.C3 = value[2] + "";
                    i.C4 = value[3] + "";

                }
                foreach (var i in rusult2)
                {
                    value = BeanUtil.ForeachClassProperties(i, Property2);

                    i.Z1 = value[0] + "";
                    i.Z2 = value[1] + "";
                    i.Z3 = value[2] + "";
                    i.Z4 = value[3] + "";

                }
                foreach (var i in rusult2)
                {

                    i.name = ctx.BpmiTokenlasts.Where(e => e.Taskid == i.Taskid)
                        .Select(e => e.Tokenname).FirstOrDefault();

                    i.Pid = ctx.BpmiTasks.Where(e => e.Taskid == i.Taskid)
                        .Select(e => e.Processid).FirstOrDefault();

                    i.Tid= ctx.BpmiTasks.Where(e => e.Taskid == i.Taskid)
                        .Select(e => e.Taskid).FirstOrDefault();
                }

                rusult2.Where(e => String.IsNullOrEmpty(data.f_bf_NoNull) ? true : int.Parse(e.C1) != 0 && int.Parse(e.Z1) != 0);
           /*         .Where(e => String.IsNullOrEmpty(data.f_ReHu)? true : if( int.Parse(data.f_ReHu)==1) true  );*/

                return rusult2.OrderByDescending(e=> e.ApplyDate).ToList();


            }
               

           
        }

        private static FromShow AutoCopy(RxFormProductUnusual parent)
        {
            FromShow child = new FromShow();


            var ParentType = typeof(RxFormProductUnusual);


            var Properties = ParentType.GetProperties();


            foreach (var Propertie in Properties)
            {
                if (Propertie.CanRead && Propertie.CanWrite)
                {
                    Propertie.SetValue(child, Propertie.GetValue(parent, null), null);
                }
            }


            return child;
        }



        /*        public record FormData( String data);*/
    }
}
