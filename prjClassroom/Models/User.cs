using System;
namespace prjClassroom.Models
{
	public class User
	{
		public int IdUSer { get; set; }
		public string Username  { get; set; }
		public string Password { get; set; }
		public string Role { get; set; }

		public static  List<User> Db()
		{
			var listUsers = new List<User>
			{
				new User
				{
					IdUSer = 1,
					Username = "jeff",
					Password = "1234",
					Role= "Admin"
				},
                new User
                {
                    IdUSer = 2,
                    Username = "mary",
                    Password = "1234",
                    Role= "employee"
                },
                new User
                {
                    IdUSer = 3,
                    Username = "charls",
                    Password = "1234",
                    Role= "employee"
                },
                new User
                {
                    IdUSer = 4,
                    Username = "diana",
                    Password = "1234",
                    Role= "employee"
                }
            };
			return listUsers;
		}
	}
}

