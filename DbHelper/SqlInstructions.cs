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

        static string ServicesTableStatement { get; } =
            "INSERT INTO Services VALUES (@name, @cost, @costUnitName, @descr, @need)";

        static SqlParameter[][] ServicesTableParams { get; } = new SqlParameter[][]
        {
            new SqlParameter[]
            {
                new SqlParameter("name", "Консультация"),
                new SqlParameter("cost", 2500),
                new SqlParameter("costUnitName", DBNull.Value),
                new SqlParameter("descr", "Личная встреча с дизайнером у нас в студии в режиме консультации по материалам, мебели, стилю и другим интересующим вопросам"),
                new SqlParameter("need", false),
            },
            new SqlParameter[]
            {
                new SqlParameter("name", "Выезд дизайнера"),
                new SqlParameter("cost", 4400),
                new SqlParameter("costUnitName", DBNull.Value),
                new SqlParameter("descr", "Выезд дизайнера на объект;Обмер объекта и составление обмерного плана;Разработка трех вариантов чертежей с планировочным решением расстановки мебели и оборудования;"),
                new SqlParameter("need", false),
            },

            new SqlParameter[]
            {
                new SqlParameter("name", "Ремонтно-отделочные работы"),
                new SqlParameter("cost", 6000),
                new SqlParameter("costUnitName", "кв. м"),
                new SqlParameter("descr", "Отделочные работы;" +
                                    "Перепланировка и функциональное зонирование помещений;"+
                                    "Электромонтажные работы;"+
                                    "Монтаж напольного покрытия;"+
                                    "Укладка керамической плитки;"+
                                    "Сантехнические работы;"+
                                    "Покраска потолка;"+
                                    "Устройство натяжных потолков;"+
                                    "Монтаж фигурного потолка;"),
                new SqlParameter("need", true),
            },

        };

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

        public static async Task ExecuteStyles()
        {
            using (AllDbContext context = new AllDbContext())
            {
                await context.Styles.LoadAsync();

                context.Styles.RemoveRange(context.Styles);
                
                await ExecuteStatement(context, StylesTableStatement, StylesTableParams);                
            }
        }
        public static async Task ExecuteServices()
        {
            using (AllDbContext context = new AllDbContext())
            {
                await context.Services.LoadAsync();

                context.Services.RemoveRange(context.Services);

                await ExecuteStatement(context, ServicesTableStatement, ServicesTableParams);
            }
        }
    }
}
