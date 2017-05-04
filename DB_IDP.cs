using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;


namespace adminka
{
    class DB_IDP : DB
    {
        public DataTable table_regions;
        public string version = "9.5 build (03/05/2017)";
        public string mycomp = "DESKTOP-KG6CQ0M";

        public DB_IDP()
        {
            string cryptcs= "115 101 114 118 101 114 61 105 100 45 112 111 105 110 116 115 46 114 117 59 117 115 101 114 61 105 100 112 95 114 111 111 116 59 100 97 116 97 98 97 115 101 61 105 100 112 111 105 110 116 115 59 112 97 115 115 119 111 114 100 61 90 82 72 82 73 112 100 116 57 114 103 48 48 102 59 99 104 97 114 115 101 116 61 117 116 102 56 59 32 65 108 108 111 119 32 90 101 114 111 32 68 97 116 101 116 105 109 101 61 116 114 117 101 59 ";
            ConnectionString = Decrypt(cryptcs);
            // ConnectionString = "server=id-points.ru;user=idp_root;database=idpoints;password=KLuBH35W1Zlo;charset=utf8; Allow Zero Datetime=true;";
            con = new MySqlConnection();
            con.ConnectionString = ConnectionString;

            try
            {
                con.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            table_regions = GetRegions();
        }

        public DataTable GetSQL(string q)
        {
            return Get_DataTable(q);
        }

        /* public string GetRegionId(string s)
         {
             string q = @"SELECT id from Region WHERE Name='" + s + "'";
             DataTable dt = Get_DataTable(q);
             if (dt.Rows.Count == 0)
                 return null;
             else
                 return dt.Rows[0][0].ToString();
         }*/

        public string GetRegionIdByName(string s)
        {
            table_regions = GetRegions();
            string id = "";
            for (int i = 0; i < table_regions.Rows.Count; i++)
            {
                if (table_regions.Rows[i][1].ToString() == s)
                    id = table_regions.Rows[i][0].ToString();
            }
            return id;
        }
        
        public DataTable GetPASP(string RksId)
        {
            string queryString = @"SELECT * FROM `Pasp` WHERE RksId='" + RksId + "'";
            return Get_DataTable(queryString);
        }

        public DataTable GetPerezvon(string id)
        {
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

            string q = @"SELECT `Work`.`Id` AS num, `Work`.`RegionId`, `Status`.`Name` AS status, 
                                `Clients`.`Tel` , `Clients`.`Fio`, `Clients`.`LastZakaz`, `Clients`.`LastSum`, `Clients`.`Pass`,  
                                `Work`.`try_total`, `Work`.`PerezvonData`, `Status`.`Id`,`Clients`.`Adr`
						         FROM `Work`
						         INNER JOIN `Clients` ON `Clients`.`Id` = `Work`.`ClientId` 
						         INNER JOIN `Status` ON `Status`.`Id` = `Work`.`StatusId` 
						         WHERE `UserId` = '" + id + "' AND `Status`.`Name`= 'Перезвон' AND `Work`.`PerezvonData` < '" + now + "'";

            return Get_DataTable(q);
        }

        public DataTable GetZakazSite(string id)
        {
            string q = @"SELECT `Work`.`Id` AS num, `Work`.`RegionId`, `Status`.`Name` AS status, 
                                `Clients`.`Tel` , `Clients`.`Fio`, `Clients`.`LastZakaz`, `Clients`.`LastSum`, `Clients`.`Pass`,  
                                `Work`.`try_total`, `Work`.`PerezvonData`, `Status`.`Id`,`Clients`.`Adr`
						         FROM `Work`
						         INNER JOIN `Clients` ON `Clients`.`Id` = `Work`.`ClientId` 
						         INNER JOIN `Status` ON `Status`.`Id` = `Work`.`StatusId` 
						         WHERE `UserId` = '" + id + "' AND `Status`.`Name`= 'Заказ с сайта'";

            return Get_DataTable(q);
        }


        public DataTable GetCurrentOrder(string id,int limit,string regionid)
        {
            int hour = DateTime.Now.Hour;

            string q2 = @"SELECT `Work`.`Id` AS num, `Work`.`RegionId` as rid, `Status`.`Id` as sid, `Status`.`Name` AS status, 
                                `Clients`.`Tel` , `Clients`.`Fio`, `Clients`.`Adr`, `Clients`.`LastZakaz`, `Clients`.`Pass`, `Clients`.`TimeDelta`,    
                                `Work`.`try_total`, `Work`.`PerezvonData` , `Work`.`UpdTime` 
						         FROM `Work`
						         INNER JOIN `Clients` ON `Clients`.`Id` = `Work`.`ClientId` 
						         INNER JOIN `Status` ON `Status`.`Id` = `Work`.`StatusId` 
						         WHERE `UserId` = " + id + " AND (`Status`.`Id`='1' OR `Status`.`Id`='2' OR `Status`.`Id`='3') \r\n";  

            string q = q2 + Environment.NewLine;

            if(hour<14)
            q += " AND `Clients`.`TimeDelta` >= " + (9-hour).ToString();  
            else
            q += " AND `Clients`.`TimeDelta` < " + 0;  

            q += " ORDER BY Status.Id, RAND() ";

            if (limit==1)
            q += " LIMIT 1";

            DataTable dt = Get_DataTable(q);
            return dt;
        }


        public DataTable GetOrderByWorkId(string id)
        {
            string q2 = @"SELECT `Work`.`Id` AS num, `Work`.`RegionId` as rid, `Status`.`Id` as sid, `Status`.`Name` AS status, 
                                `Clients`.`Tel` , `Clients`.`Fio`, `Clients`.`Adr`, `Clients`.`LastZakaz`, `Clients`.`LastSum`, `Clients`.`Pass`,  
                                `Work`.`try_total`, `Work`.`PerezvonData` , `Work`.`UpdTime`, 
						         FROM `Work`
						         INNER JOIN `Clients` ON `Clients`.`Id` = `Work`.`ClientId` 
						         INNER JOIN `Status` ON `Status`.`Id` = `Work`.`StatusId` 
						         WHERE `Work`.`Id` = " + id + "\r\n";

            string q = q2 + Environment.NewLine;

            q += " ORDER BY `Status`.`Id`, CAST(`index` AS SIGNED) DESC "; //
            q += " LIMIT 0, 1";

            //MessageBox.Show(q);
            return Get_DataTable(q);
        }


        public DataTable GetZakazi(string id, string status, string limit) //`Clients`.`LastZakaz`, `Clients`.`LastSum`,
        {
            string q2 = @"SELECT `Work`.`Id` AS num, `Work`.`RegionId` as rid, `Status`.`Id` as sid, `Status`.`Name` AS status, 
                                `Clients`.`Tel` , `Clients`.`Fio`, `Clients`.`Adr`, `Clients`.`Pass`,  `Clients`.`TimeDelta` as `Час. пояс`,
                                `Work`.`try_total`, `Work`.`PerezvonData` , `Work`.`UpdTime`
						         FROM `Work`
						         INNER JOIN `Clients` ON `Clients`.`Id` = `Work`.`ClientId` 
						         INNER JOIN `Status` ON `Status`.`Id` = `Work`.`StatusId` 
						         WHERE `UserId` = '" + id + "' \r\n";

            string q = q2 + Environment.NewLine;
            
            if (status != "")
                q += " AND `Status`.`Name`= '" + status + "' \r\n";

            if(status == "Не обработан")
                q += " ORDER BY `Status`.`Id`";

            if (status == "Недозвон")
                q += " ORDER BY `Status`.`Id`, `Work`.`UpdTime` ";
            
            if(status != "Не обработан" && status != "Недозвон")
                q += " ORDER BY `Status`.`Id`, `Work`.`UpdTime` DESC"; 

            if (limit != "")
                q += " LIMIT 0, " + limit;

            return Get_DataTable(q);
        }

        public DataTable GetTovars()
        {
            string queryString = @"SELECT `Id`, `Name`, `Price`, `Header`,`Descr` FROM `Tovar` ORDER by Name";
            return Get_DataTable(queryString);
        }

        public DataTable GetRegions()
        {
            string queryString = @"SELECT Id, Name FROM `Region` WHERE Name!='del'";
            return Get_DataTable(queryString);
        }

        public DataTable GetOstatki(string rid)
        {
            string queryString = @"SELECT SUM(`Sklad`.`Income`) - SUM(`Sklad`.`Outcome`) AS Vsego,`sel` ,`rsel`, `Sklad`.`SkladTypeId`, `Tovar`.`Name`, `Tovar`.`Id` AS tid, `Sklad`.`SkladTypeId` AS sid, `tovarcomment`.`comment` AS cmt FROM `Sklad` INNER JOIN `Tovar` ON `Sklad`.`TovarId` = `Tovar`.`Id` 
                                   LEFT JOIN `tovarcomment` ON (`Sklad`.`RegionId` = `tovarcomment`.`RegionId` AND `Sklad`.`TovarId` = `tovarcomment`.`TovarId`) 
                                   LEFT JOIN ( SELECT SUM(`PriceLst`.`Kol`) AS sel, `PriceLst`.`TovarId` AS stid, `Work`.`ByPost` AS ololo FROM `Work` INNER JOIN `WorkTovar` ON `Work`.`Id` = `WorkTovar`.`WorkId` INNER JOIN `PriceLst` ON `WorkTovar`.`PriceId` = `PriceLst`.`Id` 
                                   WHERE `StatusId` IN (7) AND `Work` .`RegionId` = " + rid + " GROUP BY `PriceLst`.`TovarId`,`Work`.`ByPost` ) ebana ON `ebana`.`stid` = `Tovar`.`Id` AND `ebana`.`ololo` +1 = `Sklad`.`SkladTypeId` " +
                                   "LEFT JOIN ( SELECT SUM(`PriceLst`.`Kol`) AS rsel, `PriceLst`.`TovarId` AS rstid, `Work`.`ByPost` AS rololo FROM `Work` INNER JOIN `WorkTovar` ON `Work`.`Id` = `WorkTovar`.`WorkId` INNER JOIN `PriceLst` ON `WorkTovar`.`PriceId` = `PriceLst`.`Id` WHERE `StatusId` IN (6) AND `Work` .`RegionId` = " + rid + " GROUP BY `PriceLst`.`TovarId`,`Work`.`ByPost` ) rebana ON `rebana`.`rstid` = `Tovar`.`Id` AND `rebana`.`rololo` +1 = `Sklad`.`SkladTypeId`	WHERE `Sklad`.`RegionId` = " + rid + " GROUP BY `Sklad`.`TovarId`,`Sklad`.`SkladTypeId`";
            return Get_DataTable(queryString);
        }

        public DataTable GetSpisaniya()
        {
            string queryString = @"SELECT  `Users`.`Id` ,  `Users`.`Name`,  `Zp`.`Data` ,  `Zp`.`Sum`  FROM  `Users`  INNER JOIN  `Zp` ON  `Zp`.`UserId` =  `Users`.`Id` ORDER BY   `Zp`.`Data` DESC, `Users`.`Name`";
            return Get_DataTable(queryString);
        }

        public DataTable GetLimits()
        {
            string queryString = @"SELECT `lim`.`id`, `us1`.`name`, `us2`.`name`, `reg`.`Name`, `lim`.`lim` FROM `Limits` as `lim`                              
                                   LEFT JOIN `Region` as `reg` ON(`lim`.RegionId = `reg`.`id`)
                                   LEFT JOIN `Users`  as `us1` ON(`lim`.UserId = `us1`.`Id`)
                                   LEFT JOIN `Users`  as `us2` ON(`lim`.RksId = `us2`.`Id`) ORDER by `lim`.`id` DESC";

            return Get_DataTable(queryString);
        }

        public DataTable GetUsers(string s)
        {
            string q = @"SELECT `Users`.`Id`, `Users`.`Name` as `Логин`, `UserType`.`Name` as `Статус`, `Users`.`Stavka`  
                                   FROM `Users`
                                   INNER JOIN `UserType` ON `UserType`.`Id` = `Users`.`UserTypeId` WHERE `Users`.`Name` LIKE '%U2%' ";
            if (s != "")
                q += " AND `UserType`.`Id`= " + s;
            q += " ORDER by `Users`.`Id` DESC";

            return Get_DataTable(q);
        }

        
        public DataTable GetHistory(string id)
        {
            string queryString = @"SELECT `Status`.`Name`,`History`.`Data`,`History`.`Comment` 
		                           FROM History 
                                   INNER JOIN `Status` ON `Status`.`Id` = `History`.`StatusId`
		                           WHERE `History`.`WorkId` = '" + id + "' ORDER BY `Data`;";
            return Get_DataTable(queryString);
        }
        
        public void InsertToHistory(string statusid,string workid,string comment)
        {
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            string q = @"INSERT into `History` SET Data='" + now + "', StatusId='" + statusid + "', WorkId='" + workid + "', " + "Comment='" + comment + "'";
            SqlQuery(q, ""); //Добавили комент в хистори
        }
        
        public DataTable GetOstatkiAdmin(string RegionId, bool hide_null)
        {
            string q = @"SELECT SUM(Sklad.Income)-SUM(Sklad.Outcome) as ostatok,  Tovar.Name, Tovar.Id as tid FROM `Sklad` INNER JOIN `Tovar` ON Sklad.TovarId = Tovar.Id GROUP BY Tovar.Name ORDER by Tovar.Name";

            DataTable dt = Get_DataTable(q);
            if (dt.Rows.Count > 0)
            {
                if (hide_null)
                {
                    DataRow[] foundRows;
                    foundRows = dt.Select("ostatok > 0");//ostatok > 0

                    if(foundRows.Length>0)
                    dt = foundRows.CopyToDataTable();
                }
                return dt;
            }
            else
            {
                return null;
            }
        }


        public DataTable GetOstatkiOper(string RegionId, bool hide_null)
        {
            string q = @"SELECT SUM(Sklad.Income)-SUM(Sklad.Outcome) as ostatok,  Tovar.Name, Tovar.Id as tid FROM `Sklad` INNER JOIN `Tovar` ON Sklad.TovarId = Tovar.Id GROUP BY Tovar.Name ORDER by Tovar.Name";

            DataTable dt = Get_DataTable(q);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    if (hide_null)
                    {
                        DataRow[] foundRows;
                        foundRows = dt.Select("ostatok > 0");//ostatok > 0
                        dt = foundRows.CopyToDataTable();
                    }
                    return dt;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        

        public DataTable GetUserInfo(string uid)
        {
            string queryString = @"SELECT  `Work`.`Id` ,  `Work`.`complete` , `Work`.`UserId` AS id, `Work`.`RegionId` AS region, COUNT( `Work`.`statusId`) AS dost,
                                   `Users`.`Name` AS name, `Users`.`UserTypeId` AS type, `Users`.`Stavka` AS stavka, SUM(price)AS summ
                                    FROM  `Work` 
                                    LEFT JOIN(
                                    SELECT  `WorkTovar`.`WorkId` AS idd,  `WorkTovar`.`PriceId` ,  `PriceLst`.`Id` , SUM(  `PriceLst`.`Price` * `PriceLst`.`Kol`) AS price
                                    FROM  `WorkTovar` 
                                    LEFT JOIN  `PriceLst` ON  `WorkTovar`.`PriceId` =  `PriceLst`.`Id` 
                                    GROUP BY  `WorkTovar`.`WorkId`
                                    )lol ON lol.idd =  `Work`.`Id`
                                    LEFT JOIN `Users` ON `Work`.`UserId` = `Users`.`Id`
                                    WHERE `Work`.`statusId`= 7 AND `Work`.`UserId` = " + uid;

            return Get_DataTable(queryString);
        }

        public string GetUserPaid(string id)
        {
            string q = @"SELECT SUM(`Sum`) AS p FROM `Zp` WHERE `UserId` = " + id + ";";
            DataTable dt = Get_DataTable(q);
            return dt.Rows[0][0].ToString();
        }

        public string GetUserAccr(string id)
        {
            string q = @"SELECT  SUM(`accr`) FROM `accruals` WHERE `userid`=" + id + ";";
            DataTable dt = Get_DataTable(q);
            return dt.Rows[0][0].ToString();
        }

        public string GetUserAllOrdersCount(string id)
        {
            string q = @"SELECT COUNT(`Work`.`statusId`) FROM `Work` WHERE `UserId` = " + id;
            DataTable dt = Get_DataTable(q);
            if (dt != null)
                return dt.Rows[0][0].ToString();
            else
                return "";
        }

        public string GetProcent(string s1, string s2)
        {
            double n = Convert.ToDouble(s2) / Convert.ToDouble(s1) * 100;
            return "(" + String.Format("{0:0.00}",n) + "%)";
        }

        public string GetUserStat(string id)
        {
            string all_orders = GetUserAllOrdersCount(id);
            string s = "Всего: " + all_orders + "\r\n";
            string q = @"SELECT `Status`.`Name`, COUNT(`Work`.`statusId`) AS cn
                        FROM `Work` 
                        INNER JOIN `Status` ON `Work`.`statusId` = `Status`.`Id`
                        WHERE `UserId` = " + id + " GROUP BY `Work`.`statusId`";

            DataTable dt = Get_DataTable(q);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                s = s + dt.Rows[i][0].ToString() + " : " + dt.Rows[i][1].ToString() + " " + GetProcent(all_orders, dt.Rows[i][1].ToString()) + "\r\n";
            }
            return s;
        }

        public DataTable Get_idpoints_ZP(string RegionName, bool show_benuk)
        {
            string q = @"SELECT Id, Name, dost as `Дост.`, summ as `На сумму`, ROUND (summ/dost) as `Ср. чек` , accr as `Начислено`, paid as `Оплачено` ,accr-paid as `Остаток`, rname,valuta FROM Users 
                         LEFT JOIN (SELECT SUM(`Sum`) AS paid, `UserId` AS us FROM `Zp` GROUP BY `UserId`) z ON z.us = Id 
                         LEFT JOIN (SELECT SUM(`accr`) AS accr, `userid` AS usd FROM `accruals` GROUP BY `userid`) a ON a.usd = Id 
                         LEFT JOIN (SELECT COUNT(`Work`.`statusId`) AS dost, 
                         SUM(`Work`.`try_total`) AS summ, 
                         `Work`.`userId`, `Region`.`Name` AS rname,  
                         `Valuta`.`abbr` as valuta
                         FROM `Work` 
                         INNER JOIN `Region` ON  `Region`.`Id` = `Work`.`RegionId` 
                         INNER JOIN `Valuta` ON  `Region`.`ValId` = `Valuta`.`Id` ";

            if (show_benuk)
                q = q + " WHERE(`statusId`= 7)"; // OR `statusId`= 11

            q = q + "GROUP BY `Work`.`userId`) s ON s.userId = Id ORDER BY `Users`.`Name`";

            DataTable dt = Get_DataTable(q);
            DataRow[] foundRows = null;

            if (RegionName == "Все")
                foundRows = dt.Select("`На сумму`>0");
            else
                foundRows = dt.Select("rname='" + RegionName + "'");

            DataTable dt2 = new DataTable();
            dt2 = foundRows.CopyToDataTable();

            return dt2;
        }

        public DataTable GetRKS()
        {
            string queryString = @"SELECT Id, Name FROM `Users` WHERE UserTypeId=3";
            return Get_DataTable(queryString);
        }

        public DataTable GetComments()
        {
            string queryString = @"SELECT DISTINCT(`Sklad`.`TovarId`) AS tid, `Sklad`.`RegionId`, `Tovar`.`Name` AS tin, `Region`.`Name` AS rin,`comment` 
                                   FROM `Sklad` 
                                   INNER JOIN `Tovar` ON  `Sklad`.`TovarId` = `Tovar`.`Id` 
                                   INNER JOIN `Region` ON  `Sklad`.`RegionId` = `Region`.`Id`
                                   LEFT JOIN `tovarcomment` ON(`Sklad`.`RegionId` = `tovarcomment`.`RegionId` AND `Sklad`.`TovarId` = `tovarcomment`.`TovarId`) 
                                   WHERE `Sklad`.`RegionId` IN(SELECT DISTINCT(`Sklad`.`RegionId`) FROM `Sklad`) ORDER BY `Sklad`.`RegionId`";

            return Get_DataTable(queryString);
        }

        public DataTable GetPriceLst(string tovar, string ValId)
        {
            string q = @"SELECT `PriceLst`.`Id`, `Tovar`.`Id` as TOVARID, `Tovar`.`Name` , `Kol`, `PriceLst`.`Price`, `ValId` 
                                   FROM `PriceLst`
                                   INNER JOIN `Tovar` ON `PriceLst`.`TovarId`=`Tovar`.`Id` WHERE `PriceLst`.`ValId`=" + ValId + " ORDER by `Tovar`.`Name`";

            if (tovar != "")
            {
                q = @"SELECT `PriceLst`.`Id`, `Tovar`.`Name` , `Kol`, `PriceLst`.`Price`, `ValId` 
                                FROM `PriceLst`
                                INNER JOIN `Tovar` ON `PriceLst`.`TovarId`=`Tovar`.`Id` WHERE `Tovar`.`Name`='" + tovar + "' AND `PriceLst`.`ValId`=" + ValId + " AND `PriceLst`.`Hidden`=0";
                q += " ORDER by `PriceLst`.`Price`";
            }
            //MessageBox.Show(q);

            return GetDataTable2(q);
        }

        public void InsertTovar(string Name, string Header, string Price, string Descr)
        {
            string sql = "INSERT INTO Tovar (Name, Header, Price, Descr, Image, Prim, Code, Category )" +
                             "VALUES ('" + Name + "', '" + Header + "', " + Price + ", '" + Descr + "', '../lib/img/tovar/Без имени-1.jpg', 0 , 0 , 0 )";
            SqlQuery(sql, "Товар добавлен!");
        }

        public void InsertQueue(string Name)
        {
            string sql = "INSERT INTO Region (Id, Name, ValId, OtkazDost )" +
                             "VALUES (NULL, '" + Name + "', 0 , 0 )";
            SqlQuery(sql, "Очередь добавлена!");
        }

        public void InsertPriceForTovar(string TovarId, string Kol, string Price, string VallId)
        {
            string sql = "INSERT INTO PriceLst VALUES ('NULL', '" + TovarId + "', '" + Kol + "', '" + Price + "', " + VallId + " , 0)";
            MessageBox.Show(sql);
            SqlQuery(sql, "Ценник добавлен!");
        }

        public void DeletePriceForTovar(string id)
        {
            string sql = "DELETE from PriceLst WHERE Id='" + id + "'";
            MessageBox.Show(sql);
            SqlQuery(sql, "Ценник удален!");
        }

        public void ResetTime(string id)
        {
            string sql = "DELETE from LastGetQueue where UserId=" + id + " and ((UNIX_TIMESTAMP()-UNIX_TIMESTAMP(LastGetQueue))<86400)";
            MessageBox.Show(sql);
            SqlQuery(sql, "Время сброшено!");
        }
        
        public void DeleteTovar(string id)
        {
            string sql = @"DELETE from Tovar where id='" + id + "' ";
            SqlQuery(sql, "Товар удален!");
        }

        public void InsertUser(string User, string Pass, string UserType, string Stavka)
        {
            string sql = @"INSERT INTO `Users` VALUES ('0','" + User + "','" + UserType + "','" + GetMD5ofMD5(Pass) + "','" + Stavka + "',0,0," + Stavka + ",1200," + Stavka + ",1800," + Stavka + ",3000," + Stavka + ")";
            SqlQuery(sql, "Пользователь добавлен!");
        }

        public void DeleteUser(string id)
        {
            string sql = @"DELETE from Users where Id='" + id + "' ";
            SqlQuery(sql, "Пользователь удален!");
        }

        public void DeleteQueue(string id)
        {
            string sql = "DELETE from Limits where Id='" + id + "' ";
            SqlQuery(sql, "Очередь удалена!");
        }

        public void ChangeUserPassword(string User, string Pass)
        {
            string sql = @"UPDATE `Users` SET `Pass`='" + GetMD5ofMD5(Pass) + "' WHERE Name='" + User + "' ";
            SqlQuery(sql, "Пароль изменен!");
        }

        public void SetQueue(string UserId, string RksId, string RegionId, string lim)
        {
            string sql = @"INSERT INTO `Limits` VALUES( NULL, " + UserId + ", " + RksId + ", " + RegionId + ", " + lim + "); ";
            SqlQuery(sql, "Очередь назначена");
        }

        public DataTable FindOrder(string id, string queue, string Status, string phone, string fio, string Adress)
        {
            string q = @"SELECT Clients.Id, Clients.Fio, Clients.Tel, Clients.Adr, Work.StatusId, Work.RegionId FROM `Work` LEFT JOIN `Clients` on Work.ClientId=Clients.Id WHERE 1=1";

            if (Status != "")
            {
                q += " AND `StatusName` = '" + Status + "'";
            }

            if (id != "")
            {
                q += " AND `Clients`.`WorkId` LIKE '%" + id + "%'";
            }

            if (phone != "")
            {
                q += " AND `Clients`.`Tel` LIKE '%" + phone + "%'";
            }

            if (fio != "")
            {
                q += " AND `Clients`.`Fio` LIKE '%" + fio + "%'";
            }

            if (Adress != "")
            {
                q += " AND `Clients`.`Adr` LIKE '%" + Adress + "%'";
            }

            MessageBox.Show(q);
            return Get_DataTable(q);
        }

        public DataTable GetQueueStat(string id)
        {
            string q = @"SELECT distinct `r`.`Id`,
                         `r`.`Name` as `Тип очереди`, 
                         Sum(1) as `Всего`,
            
            sum(if(w.UserId is NULL,1,0)) as Свободно,
			sum(if(w.StatusId = 1 AND w.UserId is NOT NULL,1,0)) as `Необработано`, 
			sum(if(w.StatusId = 6,1,0)) as `Одобрено`, 
			sum(if(w.StatusId = 5,1,0)) as `Отклонено`, 
			sum(if(w.StatusId = 7,1,0)) as `Доставлено`, 
			sum(if(w.StatusId = 3,1,0)) as `Недозвон`, 
			sum(if(w.StatusId = 4,1,0)) as `Перезвон`, 
			sum(if(w.StatusId = 8,1,0)) as `Утв. выкуп`,
            sum(w.Total)/(sum(if(w.StatusId = 6,1,0))+sum(if(w.StatusId = 7,1,0))) as `Ср. чек`

			from `Work` as w 
				left join `Users` as u on `w`.`UserId`=`u`.`Id` 
				left join `Region` as r on `r`.`Id`=`w`.`RegionId`
                                
                where `r`.`Id`='" + id + "' group by w.RegionId order by r.Name";

            //((`u`.`Hidden`= 0) OR(`u`.`Hidden` IS NULL)) and `w`.`UserId` IS NOT NULL
            //    and
            return Get_DataTable(q);
        }


        public DataTable GetQueueStatFull(string id)
        {
            string q = @"SELECT SUM( 1 ) `Всего`,
            SUM( IF(`Statusid`='1',1,0) ) `Необработаных`,
            SUM( IF(`Statusid`='2',1,0) ) `Заказ с сайта`,
            SUM( IF(`Statusid`='3',1,0) ) `Недозвон`,
            SUM( IF(`Statusid`='4',1,0) ) `Перезвон`,
            SUM( IF(`Statusid`='5',1,0) ) `Отказ`,
            SUM( IF(`Statusid`='6',1,0) ) `Одобрен`,
            SUM( IF(`Statusid`='7',1,0) ) `Доставлен`, 
            SUM( IF(`Statusid`='8',1,0) ) `Не доставлен`, 
            SUM( IF(`Statusid`='9',1,0) ) `Утверждает выкуп`, 
            SUM( IF(`Statusid`='10',1,0) ) `Расчет начат`, 
            SUM( IF(`Statusid`='11',1,0) ) `Расчет проивзеден`,
            SUM( IF(`Statusid`='12',1,0) ) `SMS` 
            FROM `Work` as `w`";

            if (id != "0")
                q = q + " LEFT JOIN `Region` as r on `r`.`Id`=`w`.`RegionId` where `r`.`Id`='" + id + "' group by w.RegionId order by r.Name";

            return Get_DataTable(q); ;
        }

        public string GetQueueStatFullText(string id)
        {
            string s = "";
            DataTable dt = GetQueueStatFull(id);

            if (dt != null)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    s += dt.Columns[i].ToString() + ": " + dt.Rows[0][i].ToString() + " " + GetProcent(dt.Rows[0][0].ToString(), dt.Rows[0][i].ToString()) + "\r\n";
                }
            }
            return s;
        }

        public DataTable GetApprovedByDate(string month)
        {
            string q = @" ";

            for (int i = 1; i < 10; i++)
            {
                q += "SELECT '2016-" + month + "-" + i + "' as `Дата` , COUNT(*) as `Одобренных` from `Work` WHERE `Statusid`= '6' AND `UpdTime`> '2016-" + month + "-" + i + " 00:00:00' AND `UpdTime`< '2016-" + month + "-" + i + " 23:59:00' ";
                if (i != 9)
                    q += "UNION ";
            }

            q += "";
            MessageBox.Show(q);
            return Get_DataTable(q);
        }

        public void GiveZP(string uid, string summa)
        {
            string dt = DateTime.Now.ToString("yyyy-MM-dd");
            string sql = @"INSERT into `Zp` VALUES(NULL, '" + dt + "' , '" + uid + "' , '" + summa + "','comment')";
            MessageBox.Show(sql);
            SqlQuery(sql, "Зарплата начислена!");
        }
        
        public DataTable GetAudit(string wid, string rid, string username, string status, string tel, string fio)
        {
            string q = @"SELECT `Work`.`Id` AS WorkId, `Work`.`RegionId`, `Users`.`Name` AS uin, `Status`.`Name` AS status, `Clients`.`Tel`, `Clients`.`Fio`, `Work`.`try_total`, `Work`.`PerezvonData`

                        FROM `Work` 
						INNER JOIN `Clients` ON `Clients`.`Id` = `Work`.`ClientId`
						INNER JOIN `Status` ON `Status`.`Id` = `Work`.`StatusId`
						INNER JOIN `Users` ON `Users`.`Id` = `Work`.`UserId`
						WHERE `Work`.`UserId` IS NOT NULL";

            if (wid != "")
                q += " AND `Work`.`Id` LIKE '%" + wid + "%'";
            if (rid != "")
                q += " AND `Work`.`RegionId` LIKE '%" + rid + "%'";
            if (username != "")
                q += " AND `Users`.`Name` LIKE '%" + username + "%'";
            if (status != "")
                q += " AND `Status`.`Name` LIKE '%" + status + "%'";
            if (fio != "")
                q += " AND `Clients`.`Fio` LIKE '%" + fio + "%'";
            if (tel != "")
                q += " AND `Clients`.`Tel` LIKE '%" + tel + "%'";
            q += " LIMIT 500";

            return Get_DataTable(q);
        }

        public DataTable GetCalls(string nomer, string nabrannii, string status, string flov, string date)
        {
            string q = @"SELECT `CallerExtension` as `Номер`, `CalledNumber` as `Набранный` , `CallStatus` as `Статус`, `CallFlow` as `Flow`, `date`, `length`, `filename`  FROM `calls` WHERE CallStatus!='UNKNOWN' ";

            if (nomer != "")
                q += " AND `CallerExtension` LIKE '%" + nomer + "%'";
            if (nabrannii != "")
                q += " AND `CalledNumber` LIKE '%" + nabrannii + "%'";
            if (status != "")
                q += " AND `CallStatus` LIKE '%" + status + "%'";
            if (flov != "")
                q += " AND `CallFlow` LIKE '%" + flov + "%'";
            if (date != "")
                q += " AND `date` LIKE '%" + date + "%'";

            q += " LIMIT 500";
            return Get_DataTable(q);
        }

        public DataTable GetSklad(string region, bool hideopers)
        {
            string q = @"SELECT `Sklad`.`Data` as `Дата`, 
                         `Tovar`.`Name` as `Наименование`, 
                         `Sklad`.`Income` as `Приход`, 
                         `Sklad`.`Outcome` as `Расход`, 
                         `Region`.`Name` as `Регион` 
                         FROM `Sklad` 
                         LEFT JOIN `Tovar` ON `Tovar`.`Id`=`Sklad`.`TovarId` 
                         LEFT JOIN `Region` ON  `Region`.`Id`=`Sklad`.`RegionId`";

            if (region != "")
                q += " WHERE `Region`.`Name`='" + region + "' ";

            if (hideopers == true)
                q += " AND `Sklad`.`Outcome`!=1 ";

            q += " ORDER by `Sklad`.`Data` DESC";
            return Get_DataTable(q);
        }


        public string GetLastTime(string uid, string rid)
        {
            string q = @"SELECT UNIX_TIMESTAMP(Max(LastGetQueue)) FROM LastGetQueue  WHERE `UserId` = " + uid + " AND `RegionId` = " + rid + " LIMIT 1";
            return SqlQueryWithResult(q);
        }

        public string GetLastTime2(string uid, string rid)
        {
            string q = @"SELECT Max(LastGetQueue) FROM LastGetQueue  WHERE `UserId` = " + uid + " AND `RegionId` = " + rid + " LIMIT 1";
            return SqlQueryWithResult(q);
        }

        string GetLimit(string uid, string rid)
        {
            string q = @"SELECT `Lim` FROM `Limits` WHERE `UserId` = " + uid + " AND `RegionId` = " + rid + " LIMIT 1";
            return SqlQueryWithResult(q);
        }

        public void GetRawOrders(string uid, string rid)
        {
            if (rid != "" && uid != "")
            {
                string limit = GetLimit(uid, rid);
                string last_time = GetLastTime(uid, rid);
                //MessageBox.Show("limit=" + limit);
                limit = "150";

                if (last_time == "")
                {
                    mysql_query("INSERT `LastGetQueue` (`UserId`, `RegionId`, `LastGetQueue`) VALUE( '" + uid + "', '" + rid + "', NOW())");
                    mysql_query("UPDATE `Work` SET `UserId`=" + uid + " WHERE `RegionId`=" + rid + " AND `UserId` IS NULL ORDER BY `Id`  LIMIT " + limit);
                    //MessageBox.Show("Заказы добавлены.");
                }

                else
                {
                    mysql_query("UPDATE `LastGetQueue` SET LastGetQueue=NOW() WHERE `UserId` = " + uid + " AND `RegionId` = " + rid + " LIMIT 1");
                    mysql_query("UPDATE `Work` SET `UserId`=" + uid + " WHERE `RegionId`=" + rid + " AND `UserId` IS NULL ORDER BY `Id`  LIMIT " + limit);
                    //MessageBox.Show("Заказы добавлены.");
                }
            }
            else
            {
                MessageBox.Show("Нет региона.");
            }
        }
        
        public DataTable GetPovtory(string fio, string workid)
        {
            string q= @"SELECT `Work`.`Id`, `Clients`.`Fio`, `Clients`.`Tel`, `Status`.`Name` from `Work` 
                       LEFT JOIN `Clients` on `Work`.`ClientId`=`Clients`.`Id`                       
                       LEFT JOIN `Status` on `Work`.`StatusId`=`Status`.`Id`
                       WHERE `Clients`.`Fio`='" + fio + "' AND `Work`.`Id` != '" + workid + "' "; // LIMIT 1
            return Get_DataTable(q);
        }
        
        public DataTable getNeobrabotannie(string regionid)
        {
            string sql = @"SELECT `Work`.`Id` AS num, `Work`.`RegionId`, `Status`.`Name` AS status, `Clients`.`Fio`, 
                         `Clients`.`Tel`, `Clients`.`Adr`, `Clients`.`LastZakaz`,`Clients`.`LastSum`,  
                         `Work`.`try_total`, `Work`.`PerezvonData`, `Status`.`Id`, `Work`.`UpdTime`
						 FROM `Work`
						 INNER JOIN `Clients` ON `Clients`.`Id` = `Work`.`ClientId` 
						 INNER JOIN `Status` ON `Status`.`Id` = `Work`.`StatusId` 
						 WHERE `Work`.`RegionId` = '" + regionid + "' AND `Status`.`Name`='Не обработан'";
            return Get_DataTable(sql);
        }
        
        public string GetGoodsPrice(string s)
        {
            string q = @"SELECT Price from Tovar WHERE Name='" + s + "'";
            return SqlQueryWithResult(q);
        }

        public string GetTovarId(string s)
        {
            string q = @"SELECT Id from `Tovar` WHERE Name = '" + s + "'";
            return SqlQueryWithResult(q);
        }

        public string GetPriceId(string tovar, string price)
        {
            string TovarId = GetTovarId(tovar);
            price = price.Substring(2, price.Length-2);

            string q= @"SELECT Id from `PriceLst` WHERE Price='" + price + "' AND TovarId='" + TovarId + "'";
            return SqlQueryWithResult(q);
        }
        
        public void SpisatTovarOper(string OperName, string TovarId) // при статусе одобрен
        {
            string TovarName = SqlQueryWithResult("SELECT Name from Tovar WHERE Id=" + TovarId);
            string q = "INSERT INTO `Sklad` (`TovarId`, `Outcome`, `RegionId`, `SkladTypeId`, `TovarName`, `OperName`, `Version`) VALUE (" + TovarId + ", 1, 20, 2, '" + TovarName + "','" + OperName + "','" + version + "')";
            SqlQuery(q, "");
        }

        public void VozvratTovar(string OperName, string TovarId) // при статусе отказ
        {       
            string TovarName = SqlQueryWithResult("SELECT Name from Tovar WHERE Id=" + TovarId);
            string q2 = "INSERT INTO `Sklad` (`TovarId`, `Outcome`, `RegionId`, `SkladTypeId`, `TovarName`, `OperName`, `Version`) VALUE (" + TovarId + ", " + "-1, 20, 2,'" + TovarName + "','" + OperName + "','" + version + "')";
            SqlQuery(q2, "Возвращаем на склад товар: " + TovarName + " tovarid=" + TovarId);
        }
        
        public string UznatTovar(string regionid, string WorkId)
        {
            DataTable dt = Get_DataTable(@"SELECT Tovar.Name from WorkTovar 
                                           LEFT JOIN PriceLst on PriceLst.Id = WorkTovar.PriceId 
                                           LEFT JOIN Tovar on Tovar.Id = PriceLst.TovarId
                                           WHERE WorkTovar.WorkId = " + WorkId);

            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string tovar = dt.Rows[i]["Name"].ToString();
                    tovar = tovar.Replace("'", "\\'");
                    return tovar;
                }
            }
            return "";
        }

        public string GetRegionFromPhone(string s)
        {
            if (s.Length > 1)
            {
                if (s[1] == '7')
                {
                    return "Казахстан";
                }
            }

            try
            {
                string s6 = s.Substring(1, 6);
                string q1 = @"SELECT Region from phone_codes WHERE Phone LIKE '" + s6 + "%'";
                //MessageBox.Show(q);
                string r1 = SqlQueryWithResult(q1);
                if (r1 != "0")
                {
                    return r1;
                }

                string s5 = s.Substring(1, 5);
                q1 = @"SELECT Region from phone_codes WHERE Phone LIKE '" + s5 + "%'";
                //MessageBox.Show(q);
                r1 = SqlQueryWithResult(q1);

                if (r1 != "0")
                {
                    return r1;
                }
                else
                {
                    string s4 = s.Substring(1, 4);
                    q1 = @"SELECT Region from phone_codes WHERE Phone LIKE '" + s4 + "%'";
                    r1 = SqlQueryWithResult(q1);
                    return r1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Пустой телефон");
                return "empty_phone";
            }
        }

        public string GetTimeByRegion(string region)
        {
            string q = @"SELECT time_delta from region_time WHERE Region='" + region + "'";
            string r = SqlQueryWithResult(q);
            double time_delta = Convert.ToDouble(r);
            string dt = DateTime.Now.AddHours(time_delta).ToString("HH:mm");
            return dt;
        }
        
        public void SetStatus(string WorkId, string StatusId, string StatusName)
        {
            string q = @"UPDATE `Work` SET StatusId='" + StatusId + "' WHERE Id='" + WorkId + "' ";
            //MessageBox.Show(q);
            SqlQuery(q, "");  //Обновили Work
        }
        
        public string GetServerVersion()
        {
            string remoteUri = "http://admin.id-points.ru/";
            string fileName1 = "version.txt";
            WebClient myWebClient = new WebClient();
            string myStringWebResource1 = null;
            myStringWebResource1 = remoteUri + fileName1;

            try
            {
                myWebClient.DownloadFile(myStringWebResource1, "version.txt");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Не удалось скачать файл с сервера. Возможно мешает Аваст. " + ex.Message);
            }
            string server_version = System.IO.File.ReadAllText(Application.StartupPath + "\\version.txt");
            return server_version;
        }

        public void UpdateExe()
        {
            string s1 = Application.StartupPath + "\\adminka.exe";
            string s2 = Application.StartupPath + "\\adminka.old";
            string s3 = Application.StartupPath + "\\adminka.tmp";

            string remoteUri = "http://admin.id-points.ru/";
            string fileName2 = "adminka.exe";
            string myStringWebResource2 = null;

            WebClient myWebClient = new WebClient();
            myStringWebResource2 = remoteUri + fileName2;

            try
            {
                myWebClient.DownloadFile(myStringWebResource2, "adminka.tmp");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось скачать файл с сервера. Возможно мешает Аваст. " + ex.Message);
            }

            File.Delete(s2);
            File.Move(s1, s2);
            File.Move(s3, s1);
            Process.Start(s1);
            Application.Exit();
        }
        public string GetRegionId_ByName(string s)
        {
            string RegionId="";
            if (s == "Питер")
            {
                RegionId = "1";
            }
            if (s == "Почта рц")
            {
                RegionId = "11";
            }
            if (s == "Ден почта")
            {
                RegionId = "15";
            }
            if (s == "Казахстан")
            {
                RegionId = "17";
            }
            if (s == "Почта улет")
            {
                RegionId = "20";
            }
            if (s == "Новосибирск")
            {
                RegionId = "21";
            }
            return RegionId;
        }

    }
}

