using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MySql.Data.EntityFramework;
using EventiApp.Utils;

namespace EventiApp.Models
{
    [DbConfigurationType(typeof(MyCustomEFConfiguration))]
    public class DataContext : DbContext
    {
        public DataContext()
                : base("DefaultConnection") //Esta 'Conexión predeterminada' debe ser igual al nombre de la cadena de conexión en Web.config.
        {
            this.Configuration.ValidateOnSaveEnabled = false;
        }

        public DbSet<Document> Documents { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Invitation> Invitations { get; set; }

        public DbSet<Menu> Menus { get; set; }


        public DbSet<MenuSelection> MenuSelections { get; set; }
    }
}