using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace DbHelper
{
    static class SqlInstructions
    {
        static string StylesTableStatement { get; } =
            "INSERT INTO Styles VALUES (@name, @img)";



        static SqlParameter[][] StylesTableParams { get; } = new SqlParameter[][]
        {
            new SqlParameter[] 
            { 
                new SqlParameter("name", "Американский стиль"), 
                new SqlParameter("img", "/Main;component/Resources/amer.jpg") 
            },
            new SqlParameter[]
            {
                new SqlParameter("name", "Ампир"),
                new SqlParameter("img", "/Main;component/Resources/ampir.jpg")
            },
            new SqlParameter[]
            {
                new SqlParameter("name", "Барокко"),
                new SqlParameter("img", "/Main;component/Resources/barokko.jpg")
            },
            new SqlParameter[]
            {
                new SqlParameter("name", "Брутализм"),
                new SqlParameter("img", "/Main;component/Resources/brutalism.jpg")
            },
            new SqlParameter[]
            {
                new SqlParameter("name", "Винтаж"),
                new SqlParameter("img", "/Main;component/Resources/vintage.jpg")
            },
            new SqlParameter[]
            {
                new SqlParameter("name", "Индийский стиль"),
                new SqlParameter("img", "/Main;component/Resources/indian.jpg")
            },
            new SqlParameter[]
            {
                new SqlParameter("name", "Колониальный стиль"),
                new SqlParameter("img", "/Main;component/Resources/colonial.jpg")
            },
            new SqlParameter[]
            {
                new SqlParameter("name", "Минимализм"),
                new SqlParameter("img", "/Main;component/Resources/minimal.jpg")
            },
            new SqlParameter[]
            {
                new SqlParameter("name", "Модерн"),
                new SqlParameter("img", "/Main;component/Resources/modern.jpg")
            },
            new SqlParameter[]
            {
                new SqlParameter("name", "Русский стиль"),
                new SqlParameter("img", "/Main;component/Resources/russian.jpg")
            },
        };


        public static async Task ExecuteStatement(AllDbContext context, string text, SqlParameter[][] @params)
        {
            
            var db = context.Database;

            foreach (var param in @params)
            {
                await db.ExecuteSqlCommandAsync(text, param);
            }
            
        }

        public static async Task DefaultExecute()
        {
            using (AllDbContext context = new AllDbContext())
            {
                await context.Styles.LoadAsync();

                if (context.Styles == null || context.Styles.ToList().Count == 0)
                {
                    await ExecuteStatement(context, StylesTableStatement, StylesTableParams);
                }
            }
        }

    }
}
