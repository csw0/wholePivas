using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace qqClient
{
    class SQL
    {
        //public string GetFriend(string DEmployId)
        //{
        //    StringBuilder str = new StringBuilder();
        //    str.Append(" select def.FriendId,(case def.groupno when 0 then de.DEmployeeName when 1 then d.WardName end) as 'Name', ");
        //    str.Append(" GroupNo from DEFriend def ");
        //    str.Append(" left join DEmployee de on CONVERT(varchar(64), de.DEmployeeID)=def.FriendId ");
        //    str.Append(" left join DWard d on d.WardCode=def.FriendId ");
        //    str.Append(" where def.DEmployeeID='");
        //    str.Append(DEmployId);
        //    str.Append("' ");
        //    str.Append(" ");

        //    return str.ToString();        
        //}

        public string GetInfor(string demployId,string wardcode)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select DEmployeeName from DEmployee where DEmployeeID='");
            str.Append(demployId);
            str.Append("' ");

            str.Append("select WardName from DWard where WardCode='");
            str.Append(wardcode);
            str.Append("' ");
            str.Append(" ");

            return str.ToString();
        }
      
       
        public string GetWardTalk(string wardcode, string type, bool IsHistory,DateTime dt1,DateTime dt2)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" select id,de.DEmployeeID,(case SWardCode when 'PivasMate' then '配置中心' else d.WardName end) as 'WardName'   ");
            str.Append(" , de.DEmployeeName, [Content] ,[InsertTime],stype  from QQLog ");
            str.Append(" left join DEmployee de on de.DEmployeeID=QQLog.DEmployeeID ");
            str.Append("left join DWard d on d.WardCode=QQLog.SWardCode ");         
            str.Append("  where  1=1");
            if (type == "0")
            {
                str.Append(" and ToDEmployid='AllWard'  ");          
            }
            else if (type == "1" )
            {
                str.Append(" and ((ToDEmployid='PivasMate' and SWardCode='");
                str.Append(wardcode);
                str.Append("') or  (ToDEmployid='");
                str.Append(wardcode);
                str.Append("' and SWardCode='PivasMate')) ");
            }
            else if (type == "2")
            {
                str.Append(" and SWardCode=ToDEmployid and SWardCode='");
                str.Append(wardcode);
                str.Append("'");
            }

            if (!IsHistory)
            {
                str.Append(" and DATEDIFF(DD,InsertTime,GETDATE())<3 ");
            }
            else
            {
                str.Append(" and DATEDIFF(DD,InsertTime,'");
                str.Append(dt1);
                str.Append("')<=0");
                str.Append(" and DATEDIFF(DD,InsertTime,'");
                str.Append(dt2);
                str.Append("')>=0");
            }

            str.Append(" order by InsertTime");
            return str.ToString();
        
        }
       
        //插入聊天记录
       public string InsertTalkLog(string DemployId,string content,string friendId,string wardcode,string type)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" insert into QQLog([DEmployeeID],SWardCode,[Content],[ToDEmployid],[InsertTime],[Stype]) ");
            str.Append("values('"+DemployId);
            str.Append("','"+wardcode);
            str.Append("','"+content);
            str.Append("','"+friendId);
            str.Append("',GETDATE(),"+type);
            str.Append(")");
            str.Append(" ");
            str.Append(" ");
            str.Append(" ");
            str.Append(" ");

            return str.ToString();
        }
     
     
        /// <summary>
       /// 搜索好友
        /// </summary>
        /// <param name="demployee"></param>
        /// <returns></returns>
       public string GetEmployee(string demployee)
       {
           StringBuilder str = new StringBuilder();
           str.Append("select DEmployeeName,DEmployeeID from DEmployee where DEmployeeID like '%");
           str.Append(demployee);
           str.Append("' or DEmployeeCode like '%");
           str.Append(demployee);
           str.Append("%' or DEmployeeName like '%");
           str.Append(demployee);
           str.Append("%' ");

           str.Append(" ");

           return str.ToString();
       
       }

       public string GetWard(string ward)
       {
           StringBuilder str = new StringBuilder();
           str.Append("select WardCode,WardName from DWard where WardCode like '%");
           str.Append(ward);
           str.Append("%' or WardName like '%");
           str.Append(ward);
           str.Append("%' ");
           str.Append("  ");
          
           return str.ToString();
       
       }

        //判断好友是否存在
       public string IsFriend(string demployeeId, string friendId)
       {
           StringBuilder str = new StringBuilder();
           str.Append("  select * from DEFriend where DEmployeeID='");
           str.Append( demployeeId);
           str.Append("' and FriendId='" + friendId);
           str.Append("'");
           str.Append("  ");
           return str.ToString();
       }

        //添加好友
       public string AddFriend(string demployeeId,string friendId,int a)
       {
           
           StringBuilder str = new StringBuilder();
           str.Append("  insert into DEFriend ([DEmployeeID],[FriendId],[InserDT],[GroupNo]) ");
           str.Append("values('"+demployeeId);
           str.Append("','"+friendId);
           str.Append("',GETDATE(),  ");
           str.Append(a);
           str.Append(")  ");
           str.Append("  ");
           str.Append("  ");
           return str.ToString();
       }
        
        /// <summary>
        /// 得到所有信息
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
       public string GetAllInfor(string demployId,string login,string wardcode)
       {
           StringBuilder str = new StringBuilder();
           str.Append(" select qqlog.ID, de.DEmployeeID,de.DEmployeeName, ToDEmployid,SWardCode,");
           str.Append(" (case SWardCode when 'PivasMate' then '配置中心' else d.WardName end) as 'WardName' ");
           str.Append(" , [Content] ,[InsertTime],stype  from QQLog ");
           str.Append("left join DEmployee de on de.DEmployeeID=QQLog.DEmployeeID ");
           str.Append(" left join DWard d on d.WardCode=QQLog.SWardCode ");
           //str.Append(" left join ReadLog rl on rl.ReadId=QQLog.ID ");
           str.Append("where not exists(select  ReadId from ReadLog where RdemployId='");
           str.Append(demployId);
           str.Append("' and qqlog.id=ReadLog.ReadId )  and qqlog.DEmployeeID!=");
           str.Append(demployId);
           if (login == "PivasMate")
           {
               str.Append(" and SWardCode!=ToDEmployid ");
           }
           else
           {
               str.Append("  and (ToDEmployid='AllWard'or (ToDEmployid='PivasMate' and SWardCode='");
               str.Append(wardcode);
               str.Append("') or ToDEmployid='");
               str.Append(wardcode);
               str.Append("')  ");

           }
           str.Append(" and DATEDIFF(DD,InsertTime,GETDATE())<3 ");
           str.Append(" order by InsertTime");

           return str.ToString();
       }
    
        /// <summary>
       /// 标记配置中心已读
        /// </summary>
        /// <param name="demployId">第一个读取消息的人</param>
        /// <param name="wardcode">发送到配置中心的病区</param>
        /// <returns></returns>
       public string UpdateQQLog(string demployId, string id)
       {
           StringBuilder str = new StringBuilder();
           str.Append(" if not exists (select *from ReadLog where ReadId=");
           str.Append(id);
           str.Append("  and RdemployId= ");
           str.Append(demployId);
           str.Append(") insert into ReadLog (ReadId,RdemployId,ReadTime) values(");        
           str.Append(id);
           str.Append(",");
           str.Append(demployId);
           str.Append(",GETDATE())");
           return str.ToString();           
       }

     

    }
}
