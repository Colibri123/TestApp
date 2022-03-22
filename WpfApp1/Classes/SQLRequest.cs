﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Classes
{
    internal class SQLRequest
    {
        public DataSet Request(string selectSQL) // функция подключения к базе данных и обработка запросов
        {
            DataSet dataTable = new DataSet("dataBase");                // создаём таблицу в приложении
                                                                        // подключаемся к базе данных
            SqlConnection sqlConnection = new SqlConnection("server=DESKTOP-ONNJAH5;Trusted_Connection=Yes;DataBase=DataBD;");
            sqlConnection.Open();                                           // открываем базу данных
            SqlCommand sqlCommand = sqlConnection.CreateCommand();          // создаём команду
            sqlCommand.CommandText = selectSQL;                             // присваиваем команде текст
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand); // создаём обработчик
            sqlDataAdapter.Fill(dataTable);                                 // возращаем таблицу с результатом
            return dataTable;
        }
    }
}
