using MySql.Data.EntityFramework;
using System.Data.Entity;


namespace TestMySQL_Code_Ferst.Model
{

    [DbConfigurationType (typeof(MySqlEFConfiguration))] // атрибут  подключения 
    class UserContext : DbContext
    {
        public UserContext () : base ("LocalMySql")
        {
        }

        public  DbSet<User> Users { get; set; }
        public  DbSet<Grups> Grups { get; set; }
    }

    public  class  User
    {
        public  int Id { get; set; }
        public  string  Name { get; set; }

        public  int  GrupsId { get; set; }
        public virtual  Grups Grups { get; set; }

        public override string ToString()
        {
            return string.Format("Имя: {0}, Группа: {1}", Name, Grups.Name);
        }
    }

    public class Grups
    {
        public int GrupsId { get; set; }
        public  string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
