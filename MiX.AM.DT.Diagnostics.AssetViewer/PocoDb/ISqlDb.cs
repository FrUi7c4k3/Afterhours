using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocoDb
{
	public interface ISqlDb
	{
		IEnumerable<T> ExecuteReaderSproc<T>(string sproc, params IDbDataParameter[] parameters) where T : new(); 
	}
}
