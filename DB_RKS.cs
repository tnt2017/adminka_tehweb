using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Security.Cryptography;


namespace adminka
{
    class DB_RKS : DB
    {
        public DataTable tab_sms;
        
        public DB_RKS()
        {
            string cryptcs = "115 101 114 118 101 114 61 105 100 45 112 111 105 110 116 115 46 114 117 59 117 115 101 114 61 114 107 115 95 114 111 111 116 59 100 97 116 97 98 97 115 101 61 114 107 115 59 112 97 115 115 119 111 114 100 61 71 115 99 114 85 105 88 101 57 117 79 112 71 84 59 99 104 97 114 115 101 116 61 117 116 102 56 59 32 65 108 108 111 119 32 90 101 114 111 32 68 97 116 101 116 105 109 101 61 116 114 117 101 59 ";
            ConnectionString = Decrypt(cryptcs);
            //ConnectionString = "server=id-points.ru;user=rks_root;database=rks;password=7ScGNcYNqQcqj7xf;charset=utf8; Allow Zero Datetime=true;";
            //ConnectionString = "server=id-points.ru;user=rks_root;database=rks;password=PNhYzuHLoAyR;charset=utf8; Allow Zero Datetime=true;";

            con = new MySqlConnection();
            con.ConnectionString = ConnectionString;
            con.Open();
            tab_sms = GetSMS();
        }

        DataTable GetSMS()
        {
            string q = "SELECT * FROM `Sms`";
            return Get_DataTable(q);
        }

        public DataTable GetRKS(RichTextBox richTextBox1, string date1, string date2, string RegionId, string status, string filter, bool show_phones)
        {
            //GoodsPrice=СЕБЕСТОИМОСТЬ
            //DLVPice=СТОИМОСТЬ ДОСТАВКИ
            //Bonus=ЗАРПЛАТА ОПЕРАТОРА ВЫСТАВЛЯЕТСЯ ПОСЛЕ УСТАНОВКИ СТАТУС ДОСТАВДЕН

            string queryString = "";

            if (show_phones)
             queryString = @"SELECT 1, WorkId, UserName, RksId, DateInsert, ClientName, Phone, Phone as 'Tel', Adress, PriceName, TotalSumm, CommentsIM, CommentsKs, DLVPice, Overheads, GoodsPrice, Bonus, StatusName, StatusId, DateChg, TID1, TID2, TID3, TID4, TID5 FROM `RKS` WHERE `DateDelivery`>'" + date1 + "' AND `DateDelivery`<'" + date2 + "'";
            else
                queryString = @"SELECT 1, WorkId, UserName, RksId, DateInsert, ClientName, Phone as 'Tel', Adress, PriceName, TotalSumm, CommentsIM, CommentsKs, DLVPice, Overheads, GoodsPrice, Bonus, StatusName, StatusId, DateChg, TID1, TID2, TID3, TID4, TID5 FROM `RKS` WHERE `DateDelivery`>'" + date1 + "' AND `DateDelivery`<'" + date2 + "'";


            if (RegionId!="0")
                queryString +=" AND `RegionId`='" + RegionId + "'";

            if (status != "")
                queryString += " AND `StatusName`='" + status + "' ";

            if (filter != "")
            {
                filter = "%" + filter + "%";
                queryString += " AND (`CommentsIM` LIKE '" + filter + "'" +
                               " OR `CommentsKs` LIKE '" + filter + "' " +
                               " OR `PriceName` LIKE '" + filter + "' " +
                               " OR `UserName` LIKE '" + filter + "' " +
                               " OR `Adress` LIKE '" + filter + "' " +
                               " OR `WorkId` LIKE '" + filter + "' " +
                               " OR `ClientName` LIKE '" + filter + "')";
            }

            queryString += "ORDER by StatusId DESC, DateInsert";

            richTextBox1.AppendText(queryString + "\r\n");

            DataTable dt= Get_DataTable(queryString);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i][0] = i+1;
            }

            return dt;
        }

        public DataTable Get_Arrived(string UserName, string date1, string date2, string RegionId, string status)
        {
            //GoodsPrice=СЕБЕСТОИМОСТЬ
            //DLVPice=СТОИМОСТЬ ДОСТАВКИ
            //Bonus=ЗАРПЛАТА ОПЕРАТОРА ВЫСТАВЛЯЕТСЯ ПОСЛЕ УСТАНОВКИ СТАТУС ДОСТАВДЕН

            string queryString = @"SELECT WorkId, UserName, DateInsert, ClientName, Adress, PriceName, TotalSumm, CommentsIM, CommentsKs, StatusName FROM `RKS` WHERE `DateDelivery`>'" + date1 + "' AND `DateDelivery`<'" + date2 + "' AND `RegionId`='" + RegionId + "'";
            queryString += " AND `StatusName`='Прибыл' AND UserName='" + UserName + "' ";

           // MessageBox.Show(queryString);

           // richTextBox1.AppendText(queryString + "\r\n");
            return Get_DataTable(queryString);
        }



        public DataTable Get_Vozvrat(string UserName, string date1, string date2, string RegionId, string status)
        {
            //GoodsPrice=СЕБЕСТОИМОСТЬ
            //DLVPice=СТОИМОСТЬ ДОСТАВКИ
            //Bonus=ЗАРПЛАТА ОПЕРАТОРА ВЫСТАВЛЯЕТСЯ ПОСЛЕ УСТАНОВКИ СТАТУС ДОСТАВДЕН

            string queryString = @"SELECT WorkId, UserName, DateInsert, ClientName, Adress, PriceName, TotalSumm, CommentsIM, CommentsKs, StatusName FROM `RKS` WHERE `DateDelivery`>'" + date1 + "' AND `DateDelivery`<'" + date2 + "' AND `RegionId`='" + RegionId + "'";
            queryString += " AND `StatusName`='Возврат' AND UserName='" + UserName + "' ";

            // MessageBox.Show(queryString);

            // richTextBox1.AppendText(queryString + "\r\n");
            return Get_DataTable(queryString);
        }

        public DataTable SetStatus(string WorkId, string StatusId, string StatusName)
        {
            string q = @"UPDATE `RKS` SET `StatusName`='" + StatusName + "', `StatusId`='" + StatusId + "' WHERE `WorkId`='" + WorkId + "' ";
            //MessageBox.Show(q);
            return Get_DataTable(q);
        }

        public string GetCountOrdersApproved(string username, string date)
        {
            string q = @"SELECT COUNT(*) from RKS WHERE `UserName`='" + username + "' AND `DateInsert` > '" + date + " 00:00:00' AND `DateInsert` < '" + date + " 23:59:00' AND (`StatusName`='Одобрен' OR `StatusName`='Отправлен' OR `StatusName`='Отказ MGR' OR `StatusName`='Прибыл'  OR `StatusName`='Выкуплен'  OR `StatusName`='Доставлен')";
            DataTable d = new DataTable();
            d = Get_DataTable(q);
            DataRow row = d.Rows[0];
            return row["COUNT(*)"].ToString();
        }

        public string GetCountOrdersDelivered(string username, string date)
        {
            string q = @"SELECT COUNT(*) from RKS WHERE `UserName`='" + username + "' AND `DateInsert` > '" + date + " 00:00:00' AND `DateInsert` < '" + date + " 23:59:00' AND `StatusName`='Доставлен'";
            DataTable d = new DataTable();
            d = Get_DataTable(q);
            DataRow row = d.Rows[0];
            return row["COUNT(*)"].ToString();
        }
        
        public DataTable Get_Salary()
        {
            string queryString = @"SELECT Id, RegionId, Date, Sum FROM `Salary` ORDER by id DESC";
            return Get_DataTable(queryString);
        }


        public void AddSalary(string regionid, string sum)
        {
            string now = DateTime.Now.ToString("yyyy-MM-dd");

            string sql = @"INSERT INTO `Salary` VALUES (NULL," + regionid + ",'" + now + "'," + sum + ")";
            SqlQuery(sql, "Бонус добавлен!");
        }


        public DataTable Get_UserHistory(string lim)
        {
            string queryString = @"SELECT `UserHistory`.`Id`, `Users`.`Name`, `UserHistory`.`act`, `UserHistory`.`text`, `UserHistory`.`Date`, `UserHistory`.`ip` FROM `UserHistory` 
                                 INNER JOIN `Users` ON `Users`.`Id`= `UserHistory`.`UserId` 
                                 ORDER by Date DESC LIMIT 0," + lim;

            return Get_DataTable(queryString);
        }
        
        public string GetSaldo(string id)
        {
            string q = @"SELECT SUM(`Debet`)-SUM(`Сredit`) as sm FROM `OurСosts` WHERE `Ovner` = " + id;
            DataTable d = new DataTable();
            d = Get_DataTable(q);
            DataRow row = d.Rows[0];
            return row["sm"].ToString();
        }
        
        public DataTable GetCosts(string id)
        {
            string q = @"SELECT `OurСosts`.`Id`, `User`.`Name`, `OurСosts`.`Insert`, `OurСosts`.`Debet`, `OurСosts`.`Сredit`, `OurСosts`.`Comment` FROM `OurСosts`
				LEFT JOIN `User` ON `OurСosts`.`Ovner` = `User`.`Id`
				WHERE `OurСosts`.`Ovner` = " + id + " ORDER by `OurСosts`.`Insert` DESC";

            return Get_DataTable(q);
        }


        public void AddCosts(string id,string debet,string credit,string comment)
        {
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

            if (debet == "")
                debet = "0";

            if (credit == "")
                credit = "0";

            string sql = @"INSERT INTO `OurСosts` VALUES (NULL," + id + ",'" + now + "'," + debet + "," + credit + ",1,'" + comment + "')";
            SqlQuery(sql, "Расход добавлен!");
        }
        
        public DataTable FindOrder(string id, string queue, string Status, string phone, string fio,string Adress)
        {
            string q = @"SELECT WorkId, UserName, RksId, StatusName, Phone, ClientName, Adress    
                          FROM `RKS`
                          INNER JOIN `Users` ON `RKS`.`RksId` = `Users`.`id` 
                          WHERE 1=1";

            if (Status != "")
            {
                q += " AND `StatusName` = '" + Status + "'";
            }

            if (id!="")
            {
                q += " AND `WorkId` LIKE '" + id + "'";
            }

            if (phone != "")
            {
                phone = "%" + phone + "%";
                q += " AND `Phone` LIKE '" + phone + "'";
            }

            if (fio != "")
            {
                fio = "%" + fio + "%";
                q += " AND `ClientName` LIKE '" + fio + "'";
            }

            if (Adress != "")
            {
                Adress = "%" + Adress + "%";
                q += " AND `Adress` LIKE '" + Adress + "'";
            }
            
            MessageBox.Show(q);
            return Get_DataTable(q);
        }


        public DataTable GetDostavka(string RksId, string username, string status, int pribil_plus)
        {
            if (status == "Прибыл")
                status = "19";
            if (status == "Выкуплен")
                status = "20";

            //`RKS`.`id` AS ksid, //`UserName`, `Region`.`Name` AS rin,  `Region`.`DLVPrice`, `RKS`.`Check`, `DateComplete`, , `RKS`.`DLVPice` AS RDPrice `StatusName`, `StatusId`
            string q = @"SELECT  `WorkId`, `DateInsert` as `Дата оформ.` ,
                                 `DateChg` as `Дата статуса`, 
                                 `ClientName` as `ФИО заказчика`, 
                                 `Phone` as `Телефон`, 
                                 `Phone` as `Tel`, 
                                 `Adress` as `Адрес`, 
                                  SUBSTRING(`Adress`, 1, 6) as `index`,
                                 `PriceName` as `Товар`,
                                 `TotalSumm` as `Сумма` , 
                                 `CommentsIM` as `Комментарий ИМ`, 
                                 `CommentsKS` as `Комментарий КС`, 
                                 `CommentsOD` as `Комментарий ОД`,
                                 `UserName` as `Опер`,
                                 `RKS`.`id` as `ksid`
                                  FROM `RKS` ";
                        
            q += " LEFT JOIN `Region` ON `RKS`.`RegionId` = `Region`.`Id`";
            q += " WHERE `StatusId` = " + status;
            q += " AND `UserName`='" + username + "'";
            //q += " AND `Region`.`RksId` = '" + RksId + "'";
            q += " AND NOT EXISTS (SELECT * FROM `Notify` WHERE `RKS`.`id` = `Notify`.`OrderId`) ";
            q += " ORDER BY `RKS`.`DateChg` DESC";

            //pribil_plus = 0;
            //  MessageBox.Show(q);

            DataTable dt;

            if (pribil_plus > 0)
            {
                dt=Get_DataTable(q);

                if (dt.Rows.Count > 0)
                {
                    DataRow[] foundRows;
                    if (pribil_plus == 1) // прибыл
                    {
                        foundRows = dt.Select("index < 630000");//ostatok > 0
                        if (foundRows.Count() > 0)
                            dt = foundRows.CopyToDataTable();
                        else
                            return null;
                    }
                    if (pribil_plus == 2) // прибыл+
                    {
                        foundRows = dt.Select("index > 630000");//ostatok > 0
                        if (foundRows.Count() > 0)
                           dt = foundRows.CopyToDataTable();
                        else
                            return null;
                    }

                }
            }
            else
            {
                dt = Get_DataTable(q);
            }

            //dt.Columns[]
            return dt;
        }


        public DataTable GetDostavkaOtpravlen(string RksId, string username, string status)
        {
            if (status == "Отправлен")
                status = "18";
            if (status == "Прибыл")
                status = "19";
            if (status == "Выкуплен")
                status = "20";

            //`RKS`.`id` AS ksid, //`UserName`, `Region`.`Name` AS rin,  `Region`.`DLVPrice`, `RKS`.`Check`, `DateComplete`, , `RKS`.`DLVPice` AS RDPrice `StatusName`, `StatusId`
            string q = @"SELECT  `WorkId`, `DateInsert` as `Дата оформ.` ,
                                 `DateChg` as `Дата статуса`, 
                                 `ClientName` as `ФИО заказчика`, 
                                 `Phone` as `Телефон`, 
                                 `Adress` as `Адрес`, 
                                  SUBSTRING(`Adress`, 1, 6) as `index`,
                                 `PriceName` as `Товар`,
                                 `TotalSumm` as `Сумма` , 
                                 `CommentsIM` as `Комментарий ИМ`, 
                                 `CommentsKS` as `Комментарий КС`, 
                                 `CommentsOD` as `Комментарий ОД`,
                                 `UserName` as `Опер`,
                                 `RKS`.`id` as `ksid`
                                  FROM `RKS` ";

            q += " LEFT JOIN `Region` ON `RKS`.`RegionId` = `Region`.`Id`";
            q += " WHERE `StatusId` = " + status;
            q += " AND `UserName`='" + username + "'";
            //q += " AND `Region`.`RksId` = '" + RksId + "'";
            q += " AND NOT EXISTS (SELECT * FROM `Notify` WHERE `RKS`.`id` = `Notify`.`OrderId`) ";
            q += " ORDER BY `RKS`.`DateChg` DESC";

            //pribil_plus = 0;
            //  MessageBox.Show(q);

            /*if (pribil_plus > 0)
            {
                DataTable dt = Get_DataTable(q);

                /*if (dt.Rows.Count > 0)
                {
                    DataRow[] foundRows;
                    if (pribil_plus == 1) // прибыл
                    {
                        foundRows = dt.Select("index < 630000");//ostatok > 0
                        if (foundRows.Count() > 0)
                            dt = foundRows.CopyToDataTable();
                        else
                            return null;
                    }
                    if (pribil_plus == 2) // прибыл+
                    {
                        foundRows = dt.Select("index > 630000");//ostatok > 0
                        if (foundRows.Count() > 0)
                            dt = foundRows.CopyToDataTable();
                        else
                            return null;
                    }

                }
                return dt;
            }
            else
            {
                DataTable dt = Get_DataTable(q);
                return dt;
            }*/

            DataTable dt = Get_DataTable(q);
            return dt;
        }





        public DataTable GetDostavkaUsers(string id)
        {
            string q = @"SELECT username FROM `Dostavka` WHERE `dostid`=" + id;
            return Get_DataTable(q);
        }

        public void InsertUser(string User, string Pass, string UserType, string Stavka)
        {
            string sql = @"INSERT INTO `Users` VALUES ('0','" + User + "','" + UserType + "','" + GetMD5ofMD5(Pass) + "','" + Stavka + "',0,0,0,0,0,0,0,0,0)";
            SqlQuery(sql, "Пользователь добавлен!");
        }


        public DataTable GetDostavkaGroupId(string user)
        {
            string q = @"SELECT GroupId from `User` WHERE login = '" + user + "'";
            return Get_DataTable(q);
        }

        public DataTable GetNotifyById(string WorkId)
        {
            string q = @"SELECT `Notify`.`Notify` as `Заметка` FROM `Notify`  ";
            if (WorkId != "")
            {
                q += " WHERE WorkId='" + WorkId + "'";
            }

            // q += " LEFT JOIN `RKS` ON `Notify`.`OrderId` = `RKS`.`Id`"; 
            //  q += " ORDER BY `Notify`.`Time` DESC";

            MessageBox.Show(q);
            return Get_DataTable(q);
        }

        public DataTable GetNotify(string username) //, `RKS`.`id` as `ksid` 
        {
            string q = @"SELECT `RKS`.`DateInsert` as `Дата оформ`,
                                `Notify`.`Time` as `Дата заметки`,
                                `RKS`.`ClientName` as `ФИО заказчика`,
                                `RKS`.`Phone` as `Телефон`,
                                `RKS`.`Adress` as `Адрес`,
                                `RKS`.`PriceName` as `Товар`,
                                `RKS`.`TotalSumm` as `Сумма` ,
                                `RKS`.`CommentsIM` as `Комментарий ИМ`,
                                `RKS`.`CommentsKs` as `Комментарий КС`,
                                `RKS`.`CommentsOD` as `Комментарий ОД`,
                                `Notify`.`Notify` as `Заметка`,
                                `RKS`.`UserName` as `Опер`,
                                `Notify`.`OrderId` as ksid
                                
                        FROM `Notify`  
                        LEFT JOIN `RKS` ON `Notify`.`OrderId` = `RKS`.`Id`";

            if (username != "")
                q += "WHERE `RKS`.`UserName`='" + username + "'";

            q += "ORDER BY `Notify`.`Time` DESC";

            return Get_DataTable(q);
        }
        
        public DataTable GetRKSHistory(string ksid)
        {
            string q = "SELECT Date, text, act, UserId FROM `UserHistory` WHERE `id`=" + ksid + " ORDER by Date DESC";
            return Get_DataTable(q);
        }

        public void InsertRKSHistory(string ksid,string text)
        {
            string q = "INSERT into UserHistory VALUES(" + ksid + ",0,0,'" + text + "', NULL, 0)";
            SqlQuery(q,"");
        }

        public DataTable GetDataTableProfit()
        {
            string q= @"SELECT     
            SUM( IF(`RegionId` = 11 AND `StatusName`='Одобрен',1,0) ) `Одобрен`,
            SUM( IF(`RegionId` = 11 AND `StatusName`='Отправлен',1,0) ) `Отправлен`, 
            SUM( IF(`RegionId` = 11 AND `StatusName`='Прибыл',1,0) ) `Прибыл`,      (SELECT SUM(TotalSumm) FROM RKS WHERE `RegionId` = 11 AND `StatusName`='Прибыл') `Прибыл2`,
            SUM( IF(`RegionId` = 11 AND `StatusName`='Выкуплен',1,0) ) `Выкуплен`,  (SELECT SUM(TotalSumm) FROM RKS WHERE `RegionId` = 11 AND `StatusName`='Выкуплен') `Выкуплен2`,           
            SUM( IF(`RegionId` = 11 AND `StatusName`='Доставлен',1,0) ) `Доставлен`,(SELECT SUM(TotalSumm) FROM RKS WHERE `RegionId` = 11 AND `StatusName`='Доставлен') `Доставлен2`,                
            SUM( IF(`RegionId` = 11 AND `StatusName`='Возврат',1,0) ) `Возврат`    
            from `RKS` as `r` ";


        /*    string q = @"SELECT 'Одобрен', COUNT(*),0 from `RKS`  WHERE `RegionId` = 11 AND StatusName = 'Одобрен'
UNION
SELECT 'Отправлен',COUNT(*),SUM(`Check`) from `RKS`  WHERE `RegionId` = 11 AND StatusName = 'Отправлен'
UNION
SELECT 'Прибыл', COUNT(*),SUM(`Check`) from `RKS`  WHERE `RegionId` = 11 AND StatusName = 'Прибыл'
UNION
SELECT 'Выкуплен', COUNT(*),SUM(`Check`) from `RKS`  WHERE `RegionId` = 11 AND StatusName = 'Выкуплен'
UNION
SELECT 'Доставлен', COUNT(*),SUM(`Check`) from `RKS`  WHERE `RegionId` = 11 AND StatusName = 'Доставлен'
UNION
SELECT 'Возврат', COUNT(*),SUM(`Check`) from `RKS`  WHERE `RegionId` = 11 AND StatusName = 'Возврат'
UNION
SELECT 'Приход', '', SUM(`Check`) from `RKS`  WHERE `RegionId` = 11 AND StatusName = 'Доставлен'
UNION
SELECT 'Расход', '', SUM(`DLVPice`+`GoodsPrice`+`Overheads`+`Bonus`+`ReturnPrice`) from `RKS`  WHERE `RegionId` = 11
UNION
SELECT 'Расход2', '', SUM(`Costs`) FROM `DayСosts` WHERE `RegionId` = 11";*/


            return Get_DataTable(q);
        }


        string GetStata(string rid, string status)
        {
            string q = "SELECT COUNT(*),SUM(`Check`) from `RKS`  WHERE `RegionId` = " + rid + " AND StatusName = '" + status + "'";
            DataRowCollection r = SqlQueryWithResultRow(q);
            return (status + " " + r[0][0].ToString() + " / " + r[0][1].ToString());
        }

        string GetPRP(string rid, string date1, string date2)
        {
            string text = "";
            string q1 = @"SELECT SUM(`Check`) from `RKS`  WHERE `RegionId` = " + rid + " AND StatusName = 'Доставлен'";
            string q2 = @"SELECT SUM(`DLVPice`+`GoodsPrice`+`Overheads`+`Bonus`) from `RKS`  WHERE `RegionId` = " + rid; //+`ReturnPrice`
            string q3 = @"SELECT SUM(`Costs`) FROM `DayСosts` WHERE `RegionId` = " + rid;

            if (date1 != "")
            {
                q1 += " AND DateDelivery>'" + date1 + "' AND DateDelivery<'" + date2 + "'";
                q2 += " AND DateDelivery>'" + date1 + "' AND DateDelivery<'" + date2 + "'";
                q3 += " AND Date>'" + date1 + "' AND Date<'" + date2 + "'";
            }

            string s1 = SqlQueryWithResult(q1);
            string s2 = SqlQueryWithResult(q2);
            string s3 = SqlQueryWithResult(q3);

            if (s1 == "")
                s1 = "0";
            if (s2 == "")
                s2 = "0";
            if (s3 == "")
                s3 = "0";

            int rashod = 0;

            if (date1 == "")
            {
                rashod = Convert.ToInt32(s2) + Convert.ToInt32(s3);
            }
            else
            {
                rashod = Convert.ToInt32(s2);
            }

            int pribil = Convert.ToInt32(s1) - rashod;

            text += "Приход: " + s1 + Environment.NewLine;
            text += "Расход: " + rashod;

            if (date1 != "")
                text += " (Расход2: " + Convert.ToInt32(s3) + ")";
            text += Environment.NewLine;
            
            text += "Прибыль: " + pribil;
            return text;
        }

        public string GetFullStatTable(string date1, string date2, string rid)
        {
            string text = "";
            text += GetPRP(rid, date1, date2);
            text += Environment.NewLine;
            text += GetPRP(rid, "", "");
            text += Environment.NewLine;
            text += "-------------";
            text += Environment.NewLine;
            text += GetStata(rid, "Одобрен") + Environment.NewLine;
            text += GetStata(rid, "Отправлен") + Environment.NewLine;
            text += GetStata(rid, "Прибыл") + Environment.NewLine;
            text += GetStata(rid, "Выкуплен") + Environment.NewLine;
            text += GetStata(rid, "Доставлен") + Environment.NewLine;
            text += GetStata(rid, "Возврат");
            return text;
        }


        public string GetFullStatTable1(string date1, string date2, string rid)
        {
            string text = "";
            text += GetPRP(rid, "", "");
            text += Environment.NewLine;
            text += GetPRP(rid, date1, date2);
            return text;
        }

        public string GetFullStatTable2(string date1, string date2, string rid)
        {
            string text = "";
            text += GetStata(rid, "Одобрен") + Environment.NewLine;
            text += GetStata(rid, "Отправлен") + Environment.NewLine;
            text += GetStata(rid, "Прибыл") + Environment.NewLine;
            text += GetStata(rid, "Выкуплен") + Environment.NewLine;
            text += GetStata(rid, "Доставлен") + Environment.NewLine;
            text += GetStata(rid, "Возврат");
            return text;
        }

        public DataTable GetPovtory(string phone, string fio)
        {
            try
            {
                string temp_phone7 = "7" + phone.Substring(1, phone.Length - 1);
                string temp_phone8 = "8" + phone.Substring(1, phone.Length - 1);

                string q = "SELECT WorkId, ClientName, Phone, StatusName from RKS WHERE Phone='" + temp_phone7 + "' OR  Phone='" + temp_phone8 + "' OR ClientName='" + fio + "'";

                /*@"SELECT `Work`.`Id`, `Clients`.`Fio`, `Clients`.`Tel`, `Status`.`Name` from `Work` 
                       LEFT JOIN `Clients` on `Work`.`ClientId`=`Clients`.`Id`                       
                       LEFT JOIN `Status` on `Work`.`StatusId`=`Status`.`Id`
                       WHERE `Clients`.`Fio`='" + fio + "' AND `Work`.`Id` != '" + workid + "' "; // LIMIT 1*/
                return Get_DataTable(q);
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        private static string GET(string Url, string Data)
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create(Url + "?" + Data);
            System.Net.WebResponse resp = req.GetResponse();
            System.IO.Stream stream = resp.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(stream);
            string Out = sr.ReadToEnd();
            sr.Close();
            return Out;
        }


        public string api_id1 = "SOy8X0ag-CELCO7U8-eINGkGm9-DGdTdR7V"; // старый до 05.04
        public string api_id2 = "kS0gDyBZ-eXBGl9AG-YlAx9SYc-yhaGf1gr"; // новый


        public void SendToLiveinform(string api_id, string workid, string phone, string fio, string tracking)
        {
            string orderid = SqlQueryWithResult("SELECT Id from RKS WHERE WorkId = " + workid);
            string Answer = GET("http://www.liveinform.ru/api/add/", "api_id=" + api_id + "&phone=" + phone + "&tracking=" + tracking + "&type=1&firstname=" + fio + "&order_id=" + orderid);
            //MessageBox.Show("ответ liveinform: " + Answer);
        }

        public string GetFromLiveinform(string workid, string phone, string fio, string tracking)
        {
            string api_id = api_id1;

            if (phone.Length == 10)
            {
                phone = "7" + phone;
            }            

            string orderid = SqlQueryWithResult("SELECT Id from RKS WHERE WorkId = " + workid);

            start:

            string Answer = GET("http://www.liveinform.ru/api/getinfo/", "api_id=" + api_id + "&phone=" + phone + "&tracking=" + tracking);
            //MessageBox.Show(Answer);

            if (Answer == "201") // неверно введен телефон
            {
                //SendToLiveinform(workid, phone, fio, tracking);
                //goto start;
                return workid + " неверный телефон" + api_id;
            }

            if (Answer == "208") // заказ не найден
            {
                if (api_id == api_id1)
                {
                    api_id = api_id2;
                    goto start;
                }
                else
                {
                    SendToLiveinform(api_id, workid, phone, fio, tracking);
                    return workid + " не было в liveinform, добавили" + api_id;
                }
            }

            if (Answer == "210") // заказ не найден
            {
                    return workid + " Информация по треку пока не поступало" + api_id;
            }

            if (Answer.IndexOf("date")>0)
            {
                int n = Answer.IndexOf("date");
                string dt_str = Answer.Substring(n+7,16);

                //DateTime dt = Convert.ToDateTime(dt_str); // не работает на сервере
                string day = dt_str.Substring(0, 2);
                string month = dt_str.Substring(3, 2);
                string year = dt_str.Substring(6, 4);
                string time = dt_str.Substring(11, 5);

                string datechg = year + "-" + month + "-" + day + " " + time;  ///dt.ToString("yyyy-MM-dd HH:mm");
                string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

                string query = "UPDATE RKS Set DateChg='" + datechg + "' WHERE WorkId=" + workid;
                SqlQuery(query, "");
            }

            if (Answer.IndexOf("\"status\":\"0\"") > 0)
            {
                return workid + " В пути" + api_id;
            }
            
            //63008906245463
            if (Answer.IndexOf("\"status\":\"1\"") > 0)
            {
                string query = "UPDATE RKS Set StatusName='Прибыл', StatusId=19 WHERE WorkId=" + workid;
                SqlQuery(query, ""); //workid + "Выкуплен"
                return workid + " Прибыл" + api_id;
            }
            
            if (Answer.IndexOf("\"status\":\"2\"")>0)
            {
                string query = "UPDATE RKS Set StatusName='Выкуплен', StatusId=20 WHERE WorkId=" + workid;
                SqlQuery(query, ""); //workid + "Выкуплен"
                return workid + " Выкуплен" + api_id;
            }

            if (Answer.IndexOf("\"status\":\"3\"") > 0)
            {
                string query = "UPDATE RKS Set StatusName='Возврат Почта', StatusId=21 WHERE WorkId=" + workid;
                SqlQuery(query, ""); //workid + "Выкуплен"
                return workid + " Возврат Почта" + api_id;
            }

            return "";
            //return Answer;
            //MessageBox.Show("ответ liveinform: " + Answer);
        }
    }
}
