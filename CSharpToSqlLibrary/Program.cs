using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlLibrary;

namespace CSharpToSqlLibrary {
	class Program {
		static void Main(string[] args) {

			//opens the connection to SQL via the UsersController constructor
			UsersController UserCtrl = new UsersController(@"STUDENT03\SQLEXPRESS", "prssql");

			//returns the list of all users in the [User] table in prssql database
			Console.WriteLine("List of all users:");
			IEnumerable<User> users = UserCtrl.List();
			foreach(User user1 in users) {
				Console.WriteLine($"{user1.Firstname} {user1.Lastname}");
			}


			//get the user with id number in "UserCtrl.Get(#)
			Console.WriteLine("-----------------------------------------------");

			User user = UserCtrl.Get(11);
			if (user == null) {
				Console.WriteLine("User not found");
			} else {
				Console.WriteLine($"Id {user.Id} is {user.Firstname} {user.Lastname}");
			}


			//intentionally putting in bad user id to see if it returns User not found
			Console.WriteLine("-----------------------------------------------");
			//get the user with id number in "UserCtrl.Get(#)
			User user2 = UserCtrl.Get(99);
			if (user2 == null) {
				Console.WriteLine("User not found");
			} else {
				Console.WriteLine($"Id {user2.Id} is {user2.Firstname} {user2.Lastname}");
			}


			//CREATE NEW USER//
			//User newUser = new User();
			//newUser.Username = "newuser3";
			//newUser.Password = "password";
			//newUser.Firstname = "New";
			//newUser.Lastname = "User";
			//newUser.Phone = "555-1234";
			//newUser.Email = "new@user.com";
			//newUser.IsReviewer = true;
			//newUser.IsAdmin = false;
			//newUser.Active = false;

			//add new user to the database//
			//bool success = UserCtrl.Create(newUser);
			//END CREATE NEW USER//

			//changing/updating the data in the user table
			user = UserCtrl.Get(3);
			user.Firstname= "user3";
			bool success = UserCtrl.Change(user);

			//delete a user
			user = UserCtrl.Get(13);
			success = UserCtrl.Remove(user);


			//closes the active connection to SQL
			UserCtrl.CloseConnection();
		}
	}
}