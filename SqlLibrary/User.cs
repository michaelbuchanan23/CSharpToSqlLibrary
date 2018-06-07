using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLibrary {
	public class User { //public here allows us to access this class outside of namespace -- defaults to internal meaning it can only be used in this namespace

		public int Id { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public bool IsReviewer { get; set; }
		public bool IsAdmin { get; set; }
		public bool Active { get; set; }

		public User(SqlDataReader reader) {
			Id = reader.GetInt32(reader.GetOrdinal("Id")); //get ordinal gives us the index number for the Id column in the table and stores it in id
			Username = reader.GetString(reader.GetOrdinal("Username"));
			Password = reader.GetString(reader.GetOrdinal("Password"));
			Firstname = reader.GetString(reader.GetOrdinal("Firstname"));
			Lastname = reader.GetString(reader.GetOrdinal("Lastname"));
			Phone = reader.GetString(reader.GetOrdinal("Phone"));
			Email = reader.GetString(reader.GetOrdinal("Email"));
			IsReviewer = reader.GetBoolean(reader.GetOrdinal("IsReviewer"));
			IsAdmin = reader.GetBoolean(reader.GetOrdinal("IsAdmin"));
			Active = reader.GetBoolean(reader.GetOrdinal("Active"));
		}
		
		public User(string Username, string Password, string Firstname, string Lastname, string Phone, string Email, bool IsReviewer, bool IsAdmin, bool Active) {
			this.Username = Username;
			this.Password = Password;
			this.Firstname = Firstname;
			this.Lastname = Lastname;
			this.Phone = Phone;
			this.Email = Email;
			this.IsReviewer = IsReviewer;
			this.IsAdmin = IsAdmin;
			this.Active = Active;
		}

		public User() { }
	}
}
