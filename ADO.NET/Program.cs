using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET
{
    class Program
    {
        static void Main(string[] args)
        {
            GetUomWithDataAdapter();
            Console.ReadLine();
        }

        private static void GetUomWithDataAdapter()
        {
            DataSet uomData = new DataSet("UomData");
            DataTable uomTable = new DataTable("UnitMeasure");
            uomData.Tables.Add(uomTable);

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorks"].ConnectionString))
            {
                var query = "select * from production.unitmeasure where unitmeasurecode like @mParam";
                using (var sqlCommand = new SqlCommand(query, conn))
                {
                    sqlCommand.Parameters.AddWithValue("@mParam", $"m%");
                    using (var adapter = new SqlDataAdapter(sqlCommand))
                    {
                        adapter.Fill(uomData, "UnitMeasure");
                    }
                }
            }

            Console.WriteLine($"Total Records: {uomTable.Rows.Count}");
        }
    }
}
