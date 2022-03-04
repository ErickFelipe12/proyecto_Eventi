using EventiApp.Models;
using EventiApp.Models.Enums;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;


namespace EventiApp.Utils
{
    public class UserHelper : IDisposable
    {
        private static Random random = new Random();
        private static ApplicationDbContext userContext = new ApplicationDbContext();
        private static DataContext db = new DataContext();
        //eliminar usuario de .net
        public static bool DeleteUser(string userName, string rolName)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            var userAsp = userManager.FindByEmail(userName);
            if (userAsp == null)
            {
                return false;
            }
            var respuesta = userManager.RemoveFromRole(userAsp.Id, rolName);
            return respuesta.Succeeded;
        }

        //Actualizar Correo del usuario
        public static bool UpdateUserName(string userName, string NewuserName)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            var userAsp = userManager.FindByEmail(userName);
            if (userAsp == null)
            {
                return false;
            }

            userAsp.UserName = NewuserName;
            userAsp.Email = NewuserName;
            var respuesta = userManager.Update(userAsp);
            return respuesta.Succeeded;

        }
        //asigamos roles si no existe lo crea
        public static void CheckRole(string roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(userContext));

            // Verifique si existe el rol, si no, créelo
            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new IdentityRole(roleName));
            }
        }
        //CREAMOS EL SUPER USUARIO
        public static void CheckSuperUser()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            var email = WebConfigurationManager.AppSettings["AdminUser"];
            var password = WebConfigurationManager.AppSettings["AdminPassWord"];
            var userASP = userManager.FindByName(email);
            if (userASP == null)
            {
                var document = db.Documents.FirstOrDefault();
                if(document == null)
                {
                    document = new Document();
                    document.DocumentType = "Cedula de Ciudadania";
                    db.Documents.Add(document);
                    db.SaveChanges();
                }

                CreateUserASP(email, RolesEnum.Administrator, password);
                var user = new User()
                {
                    Active =true,
                    IdDocumentType = document.IdDocumentType,
                    Identification = "123",
                    Email = email,
                    LastName = "admin",
                    Name = "admin",
                    Phone = "00000"
                };
                db.Users.Add(user);
                db.SaveChanges();
                return;
            }

            userManager.AddToRole(userASP.Id, RolesEnum.Administrator);
        }

        public static void AgregarRolUsuario(string email, string roleName)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            var userASP = userManager.FindByName(email);
            if (userASP == null)
            {

                return;
            }
            userManager.AddToRole(userASP.Id, roleName);
        }
        public static void CreateUserASP(string email)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            var password = WebConfigurationManager.AppSettings["AdminPassWord"];

            var userASP = userManager.FindByEmail(email);
            if (userASP == null)
            {
                userASP = new ApplicationUser
                {
                    Email = email,
                    UserName = email,
                };
                userManager.Create(userASP, password);
            }
            
        }

        public static void CreateUserASP(string email, string roleName, string password)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));

            var userASP = new ApplicationUser
            {
                Email = email,
                UserName = email,
            };

            userManager.Create(userASP, password);
            userManager.AddToRole(userASP.Id, roleName);
        }

        public static string RandomPassword(int length = 6)
        {
            //string chars = WebConfigurationManager.AppSettings["CharsPassWord"]; 
            //return new string(Enumerable.Repeat(chars, length)
            //  .Select(s => s[random.Next(s.Length)]).ToArray());
            // return "123456";
            string chars = WebConfigurationManager.AppSettings["CharsPassWord"];
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

            public static async Task PasswordRecovery(string email)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            var userASP = userManager.FindByEmail(email);
            if (userASP == null)
            {
                return;
            }

            //var user = db.User.Where(tp => tp.UserName == email).FirstOrDefault();
            //if (user == null)
            //{
            //    return;
            //}

            //var random = new Random();
            //var newPassword = string.Format("{0}{1}{2:04}*",
            //    user.Nombre.Trim().ToUpper().Substring(0, 1),
            //    user.Apellido.Trim().ToLower(),
            //    random.Next(10000));

            //userManager.RemovePassword(userASP.Id);
            //userManager.AddPassword(userASP.Id, newPassword);

            //var subject = "Taxes Password Recovery";
            //var body = string.Format(@"
            //    <h1>Taxes Password Recovery</h1>
            //    <p>Yor new password is: <strong>{0}</strong></p>
            //    <p>Please change it for one, that you remember easyly",
            //    newPassword);

            //await EmailAyuda.SendMail(email, subject, body);
        }

        public void Dispose()
        {
            userContext.Dispose();
            db.Dispose();
        }
    }
}