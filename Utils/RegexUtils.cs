using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NetCoreApi.Utils
{
    public class RegexUtils
    {

		/// <summary>
		/// Valida un correo electronico
		/// </summary>
		/// <param name="mail"></param>
		/// <returns></returns>
		public static bool IsValidEmail(string mail) {
			bool centinela = false;
			string pattern = null;
			pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
			centinela = Regex.IsMatch(mail, pattern);
			return centinela;
		}
}
}
