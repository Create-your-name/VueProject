﻿using webapi.Service.SaveExcel;
using webapi.Service.SendEmail;

namespace webapi.Service.CMP_SPTS_Email
{
    public class CmpEmail
    {
        public void SendCmpEmail()
        {

            String sBody = EmailBody();

            List<String> sendTO = new List<string>();
            sendTO.Add("liuhai82@rxgz.crmicro.com");
            /*            sendTO.Add("songxingxing15@rxgz.crmicro.com");
                        sendTO.Add("zhangzhenglu1@crmicro.com");
                        sendTO.Add("caolianying1@rxgz.crmicro.com");*/

            sendTO.Add("runxin_shengchanyingyunbudep@rxgz.crmicro.com");
            sendTO.Add("guojiahui@csmc.crmicro.com");
            sendTO.Add("gaojw@csmc.crmicro.com");

            List<String> SendCC = new List<string>();
            /*            SendCC.Add("zhangzhenglu1@crmicro.com");*/
            SendCC.Add("liuhai82@rxgz.crmicro.com");
            String fromEmail = "crmic_nc_MES_zy@rxgz.crmicro.com";
            String fromPwd = "Rxmes@2023";
            String fromName = "键合、CMP报表";
            String title = "键合、CMP报表";
            String Emailbody = sBody;
            String ExcelPath = @"C:\TEST\月结模版.xlsx";

            // List<String> sendTO,List<String> SendCC, String fromEmail, string fromPwd, String fromName, String title, String Emailbody,String ExcelName
            //EmailServer SendEmail = new EmailServer()
            EmailServer.SendEmail(sendTO, SendCC, fromEmail, fromPwd, fromName, title, Emailbody, ExcelPath);

        }





        public string EmailBody()
        {
            string EmailBody = "";

            

            //   數據編碼   針對潤興數據進行Show        


            DateTime currentDate = DateTime.Now;
            DateTime LastDate = currentDate.AddDays(-2);
            string ExcelPath = @"C:\TEST\月结模版.xlsx";
            string Sheet = "关键设备组";
            List<String> RowList = new List<string>();
            EmailBody += ExcelServer.SendExcel(ExcelPath, Sheet, RowList, currentDate, LastDate);
            // 數據結束 
            EmailBody += "</tr>   ";
            EmailBody += "</table><br><br>";
            EmailBody += "</BODY>";

            return EmailBody;

        }


    }
}
