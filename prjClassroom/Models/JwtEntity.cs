using System;
using System.Security.Claims;

namespace prjClassroom.Models
{
	public class JwtEntity
	{
		public JwtEntity()
		{
		}
		public string Key { get; set; }
		public string Issuer { get; set; }
		public string Audience { get; set; }
		public string Subject { get; set; }

		public static dynamic TokenValidate(ClaimsIdentity identity )
		{
			try
			{
				if(identity.Claims.Count() == 0)
				{
                    return new
                    {
						success = false,
                        message = "Verify valid token",
                        result = "",
                    };
                }

				var userId = identity.Claims.FirstOrDefault(u => u.Type == "id").Value;

				UserEntity user = UserEntity.Db().FirstOrDefault(u => u.IdUSer == userId);
                return new
                {
                    success = true,
                    message = "Verify valid token",
                    result = user,
                };
            }
			catch (Exception ex)
			{
				return new
				{
                    success = false,
                    message = "Catch" + ex,
					result = "",
				};
			}
		}
	}
}

