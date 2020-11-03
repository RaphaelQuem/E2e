using Infra;
using Microsoft.Extensions.Configuration;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Repository
{
    public class BaseRepository<T> : IBaseRepository<T>
    {
        const string INSERT_LAYOUT = "INSERT INTO {0} ({1}) VALUES ({2})";
        const string UPDATE_LAYOUT = "UPDATE {0} SET {1}";
        const string SELECT_LAYOUT = "SELECT {0} FROM \"{1}\"";
        const string WHERE_LAYOUT = " WHERE {0}";
        const string DELETE_LAYOUT = "DELETE FROM \"{0}\"";
        public List<T> Select()
        {
            using (IDbConnection conn = Factory.GetOpenConnection())
            {
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = CreateSelect(typeof(T));

                    return cmd.ExecuteReader().ToModel<T>();
                };
            }
        }
        public T Select(int id)
        {

            using (IDbConnection conn = Factory.GetOpenConnection())
            {
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = CreateSelect(typeof(T),true);

                    var pk = GetPK(typeof(T));

                    var param = cmd.CreateParameter();
                    param.ParameterName = pk.Name;
                    param.Value = id;
                    cmd.Parameters.Add(param);

                    return cmd.ExecuteReader().ToModel<T>().FirstOrDefault();
                };
            }
        }
        public bool Insert(T model)
        {
            int rows = 0;
            using (IDbConnection conn = Factory.GetOpenConnection())
            {
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = CreateInsert(model);

                    foreach (var prop in model.GetType().GetProperties().Where(p => !p.CustomAttributes.Any(ca => ca.AttributeType.Name == "PK")))
                    {
                        var param = cmd.CreateParameter();
                        param.ParameterName = prop.Name;
                        param.Value = prop.GetValue(model) ?? DBNull.Value;
                        cmd.Parameters.Add(param);
                    }
                    rows = cmd.ExecuteNonQuery();
                };
            }

            return rows > 0;
        }
        public bool Update(T model)
        {
            int rows = 0;
            using (IDbConnection conn = Factory.GetOpenConnection())
            {
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = CreateUpdate(model);

                    foreach (var prop in model.GetType().GetProperties())
                    {
                        var param = cmd.CreateParameter();
                        param.ParameterName = prop.Name;
                        param.Value = prop.GetValue(model) ?? DBNull.Value;
                        cmd.Parameters.Add(param);
                    }

                    rows = cmd.ExecuteNonQuery();
                };
            }

            return rows > 0;
        }
        public bool Delete(int id)
        {
            int rows = 0;
            using (IDbConnection conn = Factory.GetOpenConnection())
            {
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = CreateDelete(typeof(T));
                    var pk = GetPK(typeof(T));

                    var param = cmd.CreateParameter();
                    param.ParameterName = pk.Name;
                    param.Value = id;
                    cmd.Parameters.Add(param);


                    rows = cmd.ExecuteNonQuery();
                };
            }

            return rows > 0;
        }

        private string CreateSelect(Type obj,bool filterPk=false)
        {


            string query = SELECT_LAYOUT;

            string tableName = obj.Name;
            List<string> fieldNames = new List<string>();
            foreach (PropertyInfo info in obj.GetProperties())
            {
                fieldNames.Add(info.Name);
            }

            query = string.Format(query, string.Join(", ", fieldNames), tableName);

            if (filterPk)
            {
                var pk = GetPK(typeof(T));
                query += string.Format(WHERE_LAYOUT, GetPValueLayout(pk.Name));
            }

            return query;
        }
        private string CreateDelete(Type obj)
        {
            string query = DELETE_LAYOUT;

            string tableName = obj.Name;

            var pk = GetPK(obj);


            query = string.Format(query, tableName);
            query += string.Format(WHERE_LAYOUT, string.Concat(pk.Name, "=@", pk.Name));

            return query;
        }
        private string CreateInsert(T obj)
        {
            string query = INSERT_LAYOUT;

            string tableName = obj.GetType().Name;
            List<string> fieldNames = new List<string>();
            List<string> fieldValues = new List<string>();

            foreach (PropertyInfo info in obj.GetType().GetProperties())
            {
                if (!info.CustomAttributes.Any(ca => ca.AttributeType.Name == "PK"))
                {
                    fieldNames.Add(info.Name);
                    fieldValues.Add($"@{info.Name}");
                }
            }

            query = string.Format(query, tableName, string.Join(", ", fieldNames), string.Join(", ", fieldValues));

            return query;
        }
        private string CreateUpdate(T obj)
        {
            string query = UPDATE_LAYOUT;

            string tableName = obj.GetType().Name;
            List<string> sets = new List<string>();
            List<string> fieldValues = new List<string>();

            foreach (PropertyInfo info in obj.GetType().GetProperties())
            {
                if (!info.CustomAttributes.Any(ca => ca.AttributeType.Name == "PK"))
                {
                    sets.Add(GetPValueLayout(info.Name));
                }
            }

            var pk = GetPK(typeof(T));

            string where = string.Format(WHERE_LAYOUT,GetPValueLayout(pk.Name));

            query = string.Format(query, tableName, string.Join(", ", sets));
            query += where;

            return query;
        }
        private PropertyInfo GetPK(Type obj)
        {
            return obj.GetProperties().Where(p => p.CustomAttributes.Any(ca => ca.AttributeType.Name == "PK")).First();
        }
        private string GetPValueLayout(string name)
        {
            return string.Concat(name, "=@", name);
        }
    }
}
