using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMySQL_Code_Ferst
{
    class Program
    {
        static void Main(string[] args)
        {
            bool flag = true;

            while (flag)
            {
                Console.Clear();

                Console.WriteLine("Добавить группу - введите \"add_grups\"");
                Console.WriteLine("Если вы хотите  получить список групп  - введите \"all_grups\"");
                Console.WriteLine("Если вы хотите  Добавить  студента - введите \"add_student\"");
                Console.WriteLine("Если вы хотите  получить список студентов - введите \"all_student\"");
                Console.WriteLine("Если вы хотите  удалить студента - введите \"remove_student\"");
                Console.WriteLine("Для выхода  - введите \"*\"");
                

                switch (Console.ReadLine())
                {
                    case "add_grups":
                        Console.WriteLine("Введите название группы");
                        AddGrups(Console.ReadLine());
                        Console.WriteLine("Группа добавлена - нажмите на  Enter что бы продолжить");
                        break;

                    case "all_grups":
                        AllGrups().ForEach((x) => Console.WriteLine(x.Name));
                        break;

                    case "add_student":
                        Console.WriteLine("Введите имя студента");
                        string name = Console.ReadLine();
                        Console.WriteLine("Введите группу");
                        string grup = Console.ReadLine();

                        try { AddStudent(name, grup); Console.WriteLine("Студент добавлен - нажмите на  Enter что бы продолжить"); }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                        break;

                    case "all_student":
                        AllStudent().ForEach(x => Console.WriteLine(x));
                        break;

                    case "remove_student":
                        Console.WriteLine("Удаление студента \n Введите имя студента");
                        try
                        {
                            RemoveStudent(Console.ReadLine());
                            Console.WriteLine("Студент  удален");
                        }
                        catch(Exception ex) { Console.WriteLine(ex.Message); }
                        break;


                    case "*":
                        flag = false;return;

                    default:
                        Console.WriteLine("Команда  не  распознана!");
                        break;
                }
                Console.ReadLine();
            }
        }

        private static void RemoveStudent(string name)
        {
            try
            {
                Model.UserContext context = new Model.UserContext();
                context.Users.Remove(context.Users.Single(x => x.Name == name));
                context.SaveChanges();
            }
            catch
            {
                throw new Exception("Ошибка удаления");
            }
        }

        public static void AddGrups(string name)
        {
            Model.UserContext context = new Model.UserContext();
            context.Grups.Add(new Model.Grups() { Name = name });
            context.SaveChanges();
        }

        public static List<Model.Grups> AllGrups()
        {
            Model.UserContext context = new Model.UserContext();
            return context.Grups.OrderBy(x => x.Name).ToList();
        }

        public static void  AddStudent(string name, string grups)
        {
            Model.UserContext context = new Model.UserContext();
            try
            {
                Model.Grups gr = context.Grups.SingleOrDefault(x => x.Name == grups);
                if (gr == null) { throw new Exception("Группа не найдена"); }
               Model.User user = new Model.User { Name = name, GrupsId = gr.GrupsId };

                context.Users.Add(user);
                context.SaveChanges();
            }
            catch
            {
                throw new Exception("Группа не найдена");
            }
        }

        public static List< Model.User> AllStudent ()
        {
            Model.UserContext context = new Model.UserContext();
            return context.Users.OrderBy(x => x.Grups.Name).ThenBy(x => x.Name).ToList();
        }
    }
}
