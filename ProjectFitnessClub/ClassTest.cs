using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessClub;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace ProjectFitnessClub

{
    public class ClassTest
    {
        ConnectionClass cs = new ConnectionClass();
        public string GetRole(string login)
        {
            try
            {
                using (cs.getConnection())
                {
                    using (SqlCommand command = new SqlCommand("GetRoleNameByLogin", cs.getConnection()))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Добавляем входной параметр
                        command.Parameters.AddWithValue("@Login", login);

                        // Добавляем выходной параметр
                        SqlParameter outputRoleName = new SqlParameter("@RoleName", SqlDbType.NVarChar, 50)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputRoleName);

                        cs.getConnection().Open();
                        command.ExecuteNonQuery();
                        login = outputRoleName.Value.ToString();
                        cs.getConnection().Close();
                        if (string.IsNullOrEmpty(login))
                        {
                            login = "юзер";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при получении роли: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return login;
        }

    }
}


