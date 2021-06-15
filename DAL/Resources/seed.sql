USE [InteriorDesign]
GO

SET IDENTITY_INSERT [Styles] ON;

INSERT INTO [Styles]([Id],[Name],[ImagePath]) VALUES 
            (1, N'Американский стиль', '/Main;component/Resources/amer.jpg'),
            (2, N'Ампир', '/Main;component/Resources/ampir.jpg'),
            (3, N'Барокко', '/Main;component/Resources/barokko.jpg'),
            (4, N'Брутализм', '/Main;component/Resources/brutalism.jpg'),
            (5, N'Винтаж', '/Main;component/Resources/vinatge.jpg'),
            (6, N'Кантри', '/Main;component/Resources/kantri.jpg'),
            (7, N'Колониальный стиль', '/Main;component/Resources/colonial.jpg'),
            (8, N'Минимализм', '/Main;component/Resources/minimal.jpg'),
            (9, N'Модерн', '/Main;component/Resources/modern.jpg'),
            (10, N'Русский стиль', '/Main;component/Resources/russian.jpg');

            
SET IDENTITY_INSERT [Styles] OFF;
SET IDENTITY_INSERT [Services] ON;


INSERT INTO [Services] ([Id], [Name], [Cost], [CostUnitName], [Description], [NeedDetails]) VALUES
                                                    (1, N'Консультация', 2500, NULL, 
                                                    N'Личная встреча с дизайнером у нас в студии в режиме консультации по материалам, мебели, стилю и другим интересующим вопросам', 0),
                                                    (2, N'Выезд дизайнера', 4400, NULL, 
                                                    N'Выезд дизайнера на объект;Обмер объекта и составление обмерного плана;Разработка трех вариантов чертежей с планировочным решением расстановки мебели и оборудования', 0),
                                                    (3, N'Ремонтно-отделочные работы', 6000, N'кв. м', 
                                                    N'Отделочные работы; Перепланировка и функциональное зонирование помещений; Электромонтажные работы; Монтаж напольного покрытия; Укладка керамической плитки; Сантехнические работы; Покраска потолка; Устройство натяжных потолков; Монтаж фигурного потолка;', 1);

SET IDENTITY_INSERT [Services] OFF;


INSERT INTO [Employees] VALUES 
            (N'Сергей', N'Монтажник', 1, CAST('02/03/2015' AS DATETIME), 32457, 0, '/Main;component/Resources/male_01.png'),
            (N'Алексей', N'Проектировщик', 1, CAST('03/15/2012' AS DATETIME), 34556, 0, '/Main;component/Resources/male_02.jpg'),
            (N'Алина', N'Менеджер', 1, CAST('11/12/2016' AS DATETIME), 29000, 0, '/Main;component/Resources/female_01.jpg'),
            (N'Анна', N'Дизайнер', 1, CAST('12/25/2014' AS DATETIME), 29000, 0, '/Main;component/Resources/female_02.jpeg'),
            (N'Семен', N'Админ', 0, CAST('03/05/2016' AS DATETIME), 32000, 0, NULL);







