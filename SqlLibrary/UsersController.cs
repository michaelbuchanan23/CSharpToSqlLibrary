using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//use generic types and create a base class that the other libraries can inherit from

namespace SqlLibrary {
	public class UsersController { //default access is internal which means it works within the project/namespace -- 
								   //--we need to make it public so that the other project (CSharpToSqlLibrary) can access it

		public IEnumerable<User> List() {
			return new List<User>();
		}

		public User Get(int id) {
			return null;
		}

		public bool Create(User user) {
			return false;
		}

		public bool Change(User user) {
			return false;
		}

		public bool Remove(User user) {
			return false;
		}

		//constructor
		public UsersController() { }
	}
}
