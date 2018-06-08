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
			VendorsController VendorCtrl =new VendorsController(@"STUDENT03\SQLEXPRESS", "prssql");

			//VENDOR CODE STARTS HERE
			//returns the list of all vendors in the Vendor table in prssql database
			Console.WriteLine("List of all vendors:");
			IEnumerable<Vendor> vendors = VendorCtrl.List();
			foreach (Vendor vendor1 in vendors) {
				Console.WriteLine($"{vendor1.Code} {vendor1.Name}");
			}

			//get the vendor with id number in "VendorCtrl.Get(#)
			Console.WriteLine("-----------------------------------------------");

			Vendor vendor = VendorCtrl.Get(3);
			if (vendor == null) {
				Console.WriteLine("Vendor not found");
			} else {
				Console.WriteLine($"Id {vendor.Id} is {vendor.Code} {vendor.Name}");
				Console.WriteLine("-----------------------------------------------");
			}

			//Begin adding a vendor code here
			Console.WriteLine("-----------------------------------------------");

			//Vendor newVendor = new Vendor("NOVN", "Novan", "4105 Hopson Road", "Morrisville", "NC", "27560", "919-485-8080", "novan@novan.com", true, true);

			//add newVendor to the database
			//bool vendorAddSuccess = VendorCtrl.Create(newVendor); //commented out so this won't keep adding to the DB
			//END adding a vendor code here

			//changing/updating the data in the vendor table
			vendor = VendorCtrl.Get(6);
			vendor.Email = "contact@novan.com";
			bool VendorChangeSuccess = VendorCtrl.Change(vendor);

			//delete a vendor
			//vendor = VendorCtrl.Get(6);
			//bool VendorDeleteSuccess = VendorCtrl.Remove(vendor);
			//VENDOR CODE ENDS HERE
			
			
			/////////////////////////////////////////////////////////////
			////////////////////////////////////////////////////////////
			
			
			//USER CODE BELOW HERE
			//returns the list of all users in the [User] table in prssql database
			Console.WriteLine("List of all users:");
			IEnumerable<User> users = UserCtrl.List();
			foreach (User user1 in users) {
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
			//user = UserCtrl.Get(13);
			//success = UserCtrl.Remove(user);


			//closes the active connection to SQL
			UserCtrl.CloseConnection();
		}
	}
}