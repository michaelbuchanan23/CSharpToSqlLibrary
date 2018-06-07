using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//use generic types and create a base class that the other libraries can inherit from

namespace SqlLibrary {
	public class UsersController { //default access is internal which means it works within the project/namespace -- 
								   //--we need to make it public so that the other project (CSharpToSqlLibrary) can access it

		SqlConnection conn = null;
		SqlCommand cmd = new SqlCommand();


		//method to add conn, sql, and clear parameters so we don't have to reuse the code in both List and Get below
		private void SetupCommand(SqlConnection conn, string sql) {
			cmd.Connection = conn;
			cmd.CommandText = sql;
			cmd.Parameters.Clear();
		}

		//method to list all the users in the database
		public IEnumerable<User> List() {

			//getting information from the [User] table
			string sql = "select * from [User]";
			//cmd.Connection = conn; //commented out these 3 lines after we added the SetupCommand() method
			//cmd.CommandText = sql;
			//cmd.Parameters.Clear();
			SetupCommand(conn, sql); //this method replaces the above 3 lines of code
			SqlDataReader reader = cmd.ExecuteReader();
			List<User> users = new List<User>();
			//navigating through the data row by row using the reader
			while (reader.Read()) { //the Read() method returns true if there is more to read and false if it there are no more rows which would then exit the while loop
				//users.Add(new User(reader)); //this is how Greg said he would have done it professionally instead of the 2 steps below
				User user = new User(reader); //calls the constructor method for User.cs to create the user with the info from the table
				users.Add(user);
			}

			//close data reader so reader can be used elsewhere
			reader.Close();

			return users;

		}

		public User Get(int id) {

			string sql = "select * from [User] where Id = @id;";
			//cmd.Connection = conn;  //commented out these 3 lines after we added the SetupCommand() method
			//cmd.CommandText = sql;
			//cmd.Parameters.Clear();
			SetupCommand(conn, sql); //this method replaces the above 3 lines of code
			cmd.Parameters.Add(new SqlParameter("@id", id));
			SqlDataReader reader = cmd.ExecuteReader();
			if (reader.HasRows == false) {
				reader.Close();
				return null;
			}
			reader.Read();
			User user = new User(reader);
			
			//close data reader so reader can be used elsewhere
			reader.Close();

			return user;
		}

		public bool Create(User user) {
			string sql = "INSERT into [User] " +
				"(Username, Password, Firstname, Lastname, Phone, Email, IsReviewer, IsAdmin, Active) " +
				"VALUES " +
				"(@Username, @Password, @Firstname, @Lastname, @Phone, @Email, @IsReviewer, @IsAdmin, @Active);";
			
			//create connection and pass sql query
			SetupCommand(conn, sql);
			
			//set up the parameters to add the new user --- this will later be changed to a method
			cmd.Parameters.Add(new SqlParameter("@Username", user.Username));
			cmd.Parameters.Add(new SqlParameter("@Password", user.Password));
			cmd.Parameters.Add(new SqlParameter("@Firstname", user.Firstname));
			cmd.Parameters.Add(new SqlParameter("@Lastname", user.Lastname));
			cmd.Parameters.Add(new SqlParameter("@Phone", user.Phone));
			cmd.Parameters.Add(new SqlParameter("@Email", user.Email));
			cmd.Parameters.Add(new SqlParameter("@IsReviewer", user.IsReviewer));
			cmd.Parameters.Add(new SqlParameter("@IsAdmin", user.IsAdmin));
			cmd.Parameters.Add(new SqlParameter("@Active", user.Active));

			//gives number of rows affected
			int recsAffected = cmd.ExecuteNonQuery();
			//this expression returns true if the number is 1 and false if the recsffected is 0
			return (recsAffected == 1);
		}

		public bool Change(User user) {
			string sql = "UPDATE [User] SET " +
				" Username = @Username, " +
				" Password = @Password, " +
				" Firstname = @Firstname, " +
				" Lastname = @Lastname, " +
				" Phone = @Phone, " +
				" Email = @Email, " +
				" IsReviewer = @IsReviewer, " +
				"IsAdmin = @IsAdmin, " +
				"Active = @Active " +
				" WHERE Id = @id;";

			//create connection and pass sql query
			SetupCommand(conn, sql);

			//set up the parameters to add the new user --- this will later be changed to a method
			cmd.Parameters.Add(new SqlParameter("@Id", user.Id));
			cmd.Parameters.Add(new SqlParameter("@Username", user.Username));
			cmd.Parameters.Add(new SqlParameter("@Password", user.Password));
			cmd.Parameters.Add(new SqlParameter("@Firstname", user.Firstname));
			cmd.Parameters.Add(new SqlParameter("@Lastname", user.Lastname));
			cmd.Parameters.Add(new SqlParameter("@Phone", user.Phone));
			cmd.Parameters.Add(new SqlParameter("@Email", user.Email));
			cmd.Parameters.Add(new SqlParameter("@IsReviewer", user.IsReviewer));
			cmd.Parameters.Add(new SqlParameter("@IsAdmin", user.IsAdmin));
			cmd.Parameters.Add(new SqlParameter("@Active", user.Active));

			//gives number of rows affected
			int recsAffected = cmd.ExecuteNonQuery();
			//this expression returns true if the number is 1 and false if the recsffected is 0
			return (recsAffected == 1);
		}

		public bool Remove(User user) {
				string sql = "DELETE from [User] where Id = @id;";

				//create connection and pass sql query
				SetupCommand(conn, sql);

				//set up the parameters to add the new user --- this will later be changed to a method
				cmd.Parameters.Add(new SqlParameter("@id", user.Id));

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

		//controller with server and database information
		public UsersController(string server, string database) {
			conn = CreateAndOpenConnection(server, database);
		}

		//default constructor
		public UsersController() {

		}
	}
}
