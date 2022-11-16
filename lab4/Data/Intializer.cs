using Laba4.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Laba4.Data
{
    public static class Intializer
    {
        public static void Intialize(Context context)
        {
            context.Database.EnsureCreated();
            if (context.Departments.Any())
            {
                return;
            }

            for (int i = 1; i <= 20; i++)
            {
                string name = "Faculty" + new Random().Next(20).ToString();
                context.Faculties.Add(new Faculty { Name = name });
            }
            context.SaveChanges();

            for (int i = 1; i <= 100; i++)
            {
                string name = "Department" + new Random().Next(100).ToString();
                bool isReleasing = new Random().Next(1) == 0;
                int id = new Random().Next(context.Faculties.Count());
                Guid facultyId = context.Faculties.ToList()[id].Id;
                context.Departments.Add(new Department { Name = name, IsReleasing = isReleasing, FacultyId = facultyId });
            }
            context.SaveChanges();

            for (int i = 1; i <= 100; i++)
            {
                string name = "Specialization" + new Random().Next(100).ToString();
                int id = new Random().Next(context.Departments.Count());
                Guid departmentId = context.Departments.ToList()[id].Id;
                context.Specializations.Add(new Specialization { Name = name, DepartmentId = departmentId });
            }
            context.SaveChanges();

            for (int i = 1; i <= 100; i++)
            {
                string name = "Discipline" + new Random().Next(100).ToString();
                int hours = 20 + new Random().Next(15);
                string reportType = "Type" + new Random().Next(10).ToString();
                int id = new Random().Next(context.Specializations.Count());
                Guid specializationId = context.Specializations.ToList()[id].Id;
                context.Disciplines.Add(new Discipline { Name = name, Hours = hours, ReportType = reportType, SpecializationId = specializationId });
            }
            context.SaveChanges();

            string[] names = {"Максим", "Никита", "Александр", "Денис", "Игорь"};
            string[] lastnames = { "Ялченко", "Шух", "Поминдевв", "Молойчик", "Драпеза"};
            for (int i = 1; i <= 100; i++)
            {
                int id = new Random().Next(names.Length);
                string name = names[id];
                id = new Random().Next(lastnames.Length);
                string lastname = lastnames[id];
                int age = 20 + new Random().Next(40);
                context.Teachers.Add(new Teacher { Name = name, Lastname = lastname, Age = age });
            }
            context.SaveChanges();

            for (int i = 1; i <= 200; i++)
            {
                int id = new Random().Next(context.Disciplines.Count());
                Guid disciplineId = context.Disciplines.ToList()[id].Id;
                id = new Random().Next(context.Teachers.Count());
                Guid teacherId = context.Teachers.ToList()[id].Id;
                context.DisciplineLists.Add(new DisciplineList { DisciplineId = disciplineId, TeacherId = teacherId });
            }
            context.SaveChanges();
        }
    }
}
