using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServerCompact;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks
{
	class MyDbConfiguration : DbConfiguration
    {
		//public MyDbConfiguration()
		//{
		//	SetProviderServices(SqlCeProviderServices.ProviderInvariantName, SqlCeProviderServices.Instance);
		//	string path = @"D:\Files\Projects\Programming\Tasks\Tasks\bin\JOBS.mdf";
		//	var connectionString = string.Format(@"Data Source={0}", path);
		//	SetDefaultConnectionFactory(new SqlCeConnectionFactory(SqlCeProviderServices.ProviderInvariantName, "", connectionString));
		//}
	}
}
