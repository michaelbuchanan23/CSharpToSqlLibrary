using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLibrary {
	public class VendorsController {

		SqlConnection conn = null;
		SqlCommand cmd = new SqlCommand();

		private void SetupCommand(SqlConnection conn, string sql) {
			cmd.Connection = conn;
			cmd.CommandText = sql;
			cmd.Parameters.Clear();
		}

		//method to return all of the vendors in the list
		public IEnumerable<Vendor> List() {

			//getting information from the Vendor table
			string sql = "select * from Vendor";
			SetupCommand(conn, sql); //this method replaces the above 3 lines of code
			SqlDataReader reader = cmd.ExecuteReader();
			List<Vendor> vendors  = new List<Vendor>();
			//navigating through the data row by row using the reader
			while (reader.Read()) { 
				Vendor vendor = new Vendor(reader);
				vendors.Add(vendor);
			}


			//close data reader so reader can be used elsewhere
			reader.Close();

			return vendors;

		}

		//get vendor with value equal to id
		public Vendor Get(int id) {

			string sql = "select * from Vendor where Id = @id;";
			SetupCommand(conn, sql); //this method replaces the above 3 lines of code
			cmd.Parameters.Add(new SqlParameter("@id", id));
			SqlDataReader reader = cmd.ExecuteReader();
			if (reader.HasRows == false) {
				reader.Close();
				return null;
			}
			reader.Read();
			Vendor vendor = new Vendor(reader);

			//close data reader so reader can be used elsewhere
			reader.Close();

			return vendor;
		}

		//create a new vendor
		public bool Create(Vendor vendor) {
			string sql = "INSERT into Vendor " +
				"(Code, Name, Address, City, State, Zip, Phone, Email, IsPreApproved, Active) " +
				"VALUES " +
				"(@Code, @Name, @Address, @City, @State, @Zip, @Phone, @Email, @IsPreApproved, @Active);";

			//create connection and pass sql query
			SetupCommand(conn, sql);

			//set up the parameters to add the new user --- this will later be changed to a method
			cmd.Parameters.Add(new SqlParameter("@Code", vendor.Code));
			cmd.Parameters.Add(new SqlParameter("@Name", vendor.Name));
			cmd.Parameters.Add(new SqlParameter("@Address", vendor.Address));
			cmd.Parameters.Add(new SqlParameter("@City", vendor.City));
			cmd.Parameters.Add(new SqlParameter("@State", vendor.State));
			cmd.Parameters.Add(new SqlParameter("@Zip", vendor.Zip));
			cmd.Parameters.Add(new SqlParameter("@Phone", vendor.Phone));
			cmd.Parameters.Add(new SqlParameter("@Email", vendor.Email));
			cmd.Parameters.Add(new SqlParameter("@IsPreApproved", vendor.IsPreApproved));
			cmd.Parameters.Add(new SqlParameter("@Active", vendor.Active));

			//gives number of rows affected
			int recsAffected = cmd.ExecuteNonQuery();
			//this expression returns true if the number is 1 and false if the recsffected is 0
			return (recsAffected == 1);
		}


		public bool Change(Vendor vendor) {
			string sql = "UPDATE Vendor SET " +
				" Code = @Code, " +
				" Name = @Name, " +
				" Address = @Address, " +
				" City = @City, " +
				" State = @State, " +
				" Zip = @Zip, " +
				" Phone = @Phone, " +
				"Email = @Email, " +
				"IsPreApproved = @IsPreApproved, " +
				" Active = @Active " +
				" WHERE Id = @Id;";

			//create connection and pass sql query
			SetupCommand(conn, sql);

			//set up the parameters to add the new user --- this will later be changed to a method
			cmd.Parameters.Add(new SqlParameter("@Id", vendor.Id));
			cmd.Parameters.Add(new SqlParameter("@Code", vendor.Code));
			cmd.Parameters.Add(new SqlParameter("@Name", vendor.Name));
			cmd.Parameters.Add(new SqlParameter("@Address", vendor.Address));
			cmd.Parameters.Add(new SqlParameter("@City", vendor.City));
			cmd.Parameters.Add(new SqlParameter("@State", vendor.State));
			cmd.Parameters.Add(new SqlParameter("@Zip", vendor.Zip));
			cmd.Parameters.Add(new SqlParameter("@Phone", vendor.Phone));
			cmd.Parameters.Add(new SqlParameter("@Email", vendor.Email));
			cmd.Parameters.Add(new SqlParameter("@IsPreApproved", vendor.IsPreApproved));
			cmd.Parameters.Add(new SqlParameter("@Active", vendor.Active));
			
			//gives number of rows affected
			int recsAffected = cmd.ExecuteNonQuery();
			//this expression returns true if the number is 1 and false if the recsffected is 0
			return (recsAffected == 1);
		}

		public bool Remove(Vendor vendor) {
			string sql = "DELETE from Vendor where Id = @id;";

			//create connection and pass sql query
			SetupCommand(conn, sql);

			//set up the parameters to add the new vendor --- this will later be changed to a method
			cmd.Parameters.Add(new SqlParameter("@id", vendor.Id));

			//gives number of rows affected
			int recsAffected = cmd.ExecuteNonQuery();
			//this expression returns true if the number is 1 and false if the recsAffected is 0
			return (recsAffected == 1);
		}


		//private method to open the SQL connection
		private SqlConnection CreateAndOpenConnection(string server, string database) {
			string connStr = $"server = {server}; database = {database}; Trusted_connection = true;";
			SqlConnection conn = new SqlConnection(connStr);
			conn.Open();
			if (conn.State != System.Data.ConnectionState.Open) {
				throw new ApplicationException("SQL connection did not open");
			}
			return conn;
		}



		//closes the SQL connection
		public void CloseConnection() {
			if (conn != null && conn.State == System.Data.ConnectionState.Open) {
				conn.Close();
			}
		}

		//constructor passing in the server and database information
		public VendorsController(string server, string database) {
			conn = CreateAndOpenConnection(server, database);
		}

		public VendorsController() { }
	}
}
