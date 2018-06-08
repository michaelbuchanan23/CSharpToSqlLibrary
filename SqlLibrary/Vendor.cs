using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLibrary {
	public class Vendor {

		public int Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public bool IsPreApproved;
		public bool Active { get; set; }
		//public List<Product> Products = new List<Product>();

		public Vendor (SqlDataReader reader) {
			Id = reader.GetInt32(reader.GetOrdinal("Id")); //get ordinal gives us the index number for the Id column in the table and stores it in id
			Code = reader.GetString(reader.GetOrdinal("Code"));
			Name = reader.GetString(reader.GetOrdinal("Name"));
			Address = reader.GetString(reader.GetOrdinal("Address"));
			City = reader.GetString(reader.GetOrdinal("City"));
			State = reader.GetString(reader.GetOrdinal("State"));
			Zip = reader.GetString(reader.GetOrdinal("Zip"));
			Phone = reader.GetString(reader.GetOrdinal("Phone"));
			Email = reader.GetString(reader.GetOrdinal("Email"));
			IsPreApproved = reader.GetBoolean(reader.GetOrdinal("IsPreApproved"));
			Active = reader.GetBoolean(reader.GetOrdinal("Active"));
		}

		public Vendor (string Code, string Name, string Address, string City, string State, string Zip, string Phone, string Email, bool IsPreApproved, bool Active) {
			this.Code = Code;
			this.Name = Name;
			this.Address = Address;
			this.City = City;
			this.State = State;
			this.Zip = Zip;
			this.Phone = Phone;
			this.Email = Email;
			this.IsPreApproved = IsPreApproved;
			this.Active = Active;
		}

		public Vendor() { }
	}
}
