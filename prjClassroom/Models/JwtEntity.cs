﻿using System;
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
	}
}

