using System;
using System.Data.Entity;
using System.Data.SqlClient;
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
        static string EmpAdminTableStatement { get; } =
            "INSERT INTO [EmployeeAdmins] VALUES((SELECT[Id] FROM(SELECT ROW_NUMBER() OVER(ORDER BY[Id] ASC) AS rownumber, " +
            "[Id] FROM[Employees]) AS foo WHERE rownumber = @row), @login, @pass, @code)";
        static string EmployeesTableStatement { get; } =
            "INSERT INTO [Employees] VALUES(@name, @spec, @isShow, @startWorking, @salary, @workStatus, @imagePath)";
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
                new SqlParameter("img", "/Main;component/Resources/vinatge.jpg")
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
        static SqlParameter[][] EmpAdminsTableParams { get; } = new SqlParameter[][]
        {
            new SqlParameter[]
            {
                new SqlParameter("row", 1),
                new SqlParameter("login", "admin"),
                new SqlParameter("pass", "admin"),
                new SqlParameter("code", new byte[]{ 1,2,3}),
            },
        };
        static SqlParameter[][] EmployeesTableParams { get; } = new SqlParameter[][]
        {
            new SqlParameter[]
            {
                new SqlParameter("name", "Сергей"),
                new SqlParameter("spec", "Монтажник"),
                new SqlParameter("isShow", true),
                new SqlParameter("startWorking", new DateTime(2015, 3, 2)),
                new SqlParameter("salary", 32457),
                new SqlParameter("workStatus", DAL.Models.WorkingStatus.Working),
                new SqlParameter("imagePath", "/Main;component/Resources/male_01.png"),
            },
            new SqlParameter[]
            {
                new SqlParameter("name", "Алексей"),
                new SqlParameter("spec", "Проектировщик"),
                new SqlParameter("isShow", true),
                new SqlParameter("startWorking", new DateTime(2012, 3, 2)),
                new SqlParameter("salary", 32457),
                new SqlParameter("workStatus", DAL.Models.WorkingStatus.Working),
                new SqlParameter("imagePath", "/Main;component/Resources/male_02.jpg"),
            },
            new SqlParameter[]
            {
                new SqlParameter("name", "Алина"),
                new SqlParameter("spec", "Менеджер"),
                new SqlParameter("isShow", true),
                new SqlParameter("startWorking", new DateTime(2011, 3, 2)),
                new SqlParameter("salary", 32457),
                new SqlParameter("workStatus", DAL.Models.WorkingStatus.Working),
                new SqlParameter("imagePath", "/Main;component/Resources/female_01.jpg"),
            },
            new SqlParameter[]
            {
                new SqlParameter("name", "Анна"),
                new SqlParameter("spec", "Дизайнер"),
                new SqlParameter("isShow", true),
                new SqlParameter("startWorking", new DateTime(2014, 3, 2)),
                new SqlParameter("salary", 32457),
                new SqlParameter("workStatus", DAL.Models.WorkingStatus.Working),
                new SqlParameter("imagePath", "/Main;component/Resources/female_02.jpeg"),
            },
            new SqlParameter[]
            {
                new SqlParameter("name", "Семен"),
                new SqlParameter("spec", "Админ"),
                new SqlParameter("isShow", false),
                new SqlParameter("startWorking", new DateTime(2013, 3, 2)),
                new SqlParameter("salary", 32457),
                new SqlParameter("workStatus", DAL.Models.WorkingStatus.Working),
                new SqlParameter("imagePath", DBNull.Value),
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
        public static async Task ExecuteAll()
        {
            await ExecuteStyles();
            await ExecuteServices();
            await ExecuteEmployee();
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
        public static async Task ExecuteEmployee()
        {
            using (AllDbContext context = new AllDbContext())
            {
                await context.Employees.LoadAsync();
                await context.EmployeeAdmins.LoadAsync();

                context.EmployeeAdmins.RemoveRange(context.EmployeeAdmins);
                context.Employees.RemoveRange(context.Employees);

                await ExecuteStatement(context, EmployeesTableStatement, EmployeesTableParams);
                await ExecuteStatement(context, EmpAdminTableStatement, EmpAdminsTableParams);
            }
        }
    }
}
