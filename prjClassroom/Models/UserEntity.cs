using System;
namespace prjClassroom.Models
{
    
    public class UserEntity
	{
       
        public string IdUSer { get; set; }
		public string Username  { get; set; }
		public string Password { get; set; }
		public string Role { get; set; }

		public static List<UserEntity> Db()
		{
			var listUsers = new List<UserEntity>
			{
				new UserEntity
                {
					IdUSer = "1",
					Username = "jeff",
					Password = "1234",
					Role= "Admin"
				},
                new UserEntity
                {
                    IdUSer = "2",
                    Username = "mary",
                    Password = "1234",
                    Role= "employee"
                },
                new UserEntity
                {
                    IdUSer = "3",
                    Username = "charls",
                    Password = "1234",
                    Role= "employee"
                },
                new UserEntity
                {
                    IdUSer = "4",
                    Username = "diana",
                    Password = "1234",
                    Role= "employee"
                }
            };
			return listUsers;
		}
	}
}

