using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lion.ExAPI.Constellation.Model.DataModel;
using Lion.ExAPI.Constellation.Model.ViewModel;
using System.Data.SqlClient;
using Dapper;

namespace Lion.ExAPI.Constellation.Model.Repository
{
    public class ConstellationRepository
    {
        private string conStr = @"Data Source=ITTC-04406-9001\SQLEXPRESS;Initial Catalog=test;Integrated Security=True";

        /// <summary>
        /// 取得
        /// </summary>
        /// <returns></returns>
        public List<ConstellationData> GetConstellation()
        {
            using (var cn = new SqlConnection(conStr))
            {
                var cmdStr = "SELECT * FROM Constellation";
                var result = cn.Query<ConstellationData>(cmdStr);
                List<ConstellationData> list = new List<ConstellationData>();

                foreach (var item in result)
                {
                    list.Add(
                        new ConstellationData()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Constellation = item.Constellation
                        });
                }
                return list;
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="constellation"></param>
        public void PostConstellation(string name, string constellation)
        {
            using (var cn = new SqlConnection(conStr))
            {
                var cmdStr = "INSERT INTO Constellation(Name, Constellation) VALUES (@name, @Constellation);";
                var p = new DynamicParameters();
                p.Add("@name", name);
                p.Add("@constellation", constellation);
                cn.Query<ConstellationData>(cmdStr, p);
            }
        }

        public void PutConstellation(string name, string constellation)
        {
            using (var cn = new SqlConnection(conStr))
            {
                var cmdStr = "UPDATE Constellation SET Constellation = @constellation WHERE Name = @name;";
                var p = new DynamicParameters();
                p.Add("@name", name);
                p.Add("@constellation", constellation);
                cn.Query<ConstellationModel>(cmdStr, p);
            }
        }

        public void DeleteConstellation(string name)
        {
            using (var cn = new SqlConnection(conStr))
            {
                var cmdStr = "DELETE FROM Constellation WHERE Name = @name;";
                var p = new DynamicParameters();
                p.Add("@name", name);

                cn.Query<ConstellationData>(cmdStr, p);
            }
        }
    }
}