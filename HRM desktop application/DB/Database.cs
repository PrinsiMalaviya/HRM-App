using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HRM_desktop_application.DB
{
    public class Database
    {
        

        public static string dbName = "employee";
        public static string server = "localhost";
        public static string userId = "admin";
        public static string password = "password";

        public static string baseConnStr = $"Server={server};Uid={userId};Pwd={password};";

        public static string connStr = $"Server={server};Database={dbName};Uid={userId};Pwd={password};";

        // Unified method for SELECT, INSERT, UPDATE
        public static object ExecuteQuery(string query, CommandType cmdType = CommandType.Text)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.CommandType = cmdType;
                    conn.Open();

                    // Detect query type
                    if (query.TrimStart().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
                    {
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            return dt; // Return DataTable for SELECT
                        }
                    }
                    else
                    {
                        int affectedRows = cmd.ExecuteNonQuery(); // For INSERT/UPDATE/DELETE
                        return affectedRows; // Return number of affected rows
                    }
                }
            }
        }


        // Initialize the database and tables if they don't exist
        public static void EnsureDatabaseAndTablesExist()
        {
            // 1. Create the database if not exists
            using (MySqlConnection conn = new MySqlConnection(baseConnStr))
            {
                conn.Open();
                string createDbQuery = $"CREATE DATABASE IF NOT EXISTS {dbName} CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci;";
                using (MySqlCommand cmd = new MySqlCommand(createDbQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            // 2. Create tables inside the keyper database

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                // Define required table structures
                Dictionary<string, string[]> tableColumns = new Dictionary<string, string[]>
                {
                    {
                        "employee_data", new[]
                        {
                            "employee_id INT",
                            "employee_name VARCHAR(255)",
                            "employee_gender VARCHAR(10)",
                            "employee_phoneNo VARCHAR(20)",
                            "employee_position VARCHAR(100)",
                            "employee_status VARCHAR(50)",
                            "salary VARCHAR(255)"
                        }
                    },
                    {
                        "login", new[]
                        {
                            "id INT",
                            "username VARCHAR(255)",
                            "password VARCHAR(255)"
                        }
                    },
                    {
                        "salary_data", new[]
                        {
                            "salary_id INT",
                            "employee_id INT",
                            "employee_name VARCHAR(100)",
                            "employee_position VARCHAR(50)",
                            "basic_salary DECIMAL(10,2)",
                            "bonus DECIMAL(10,2)",
                            "deductions DECIMAL(10,2)",
                            "net_salary DECIMAL(10,2)",
                            "salary_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP"
                        }
                    }
                };


                // 3. Loop through tables and create + ensure columns
                foreach (var table in tableColumns)
                {
                    string tableName = table.Key;
                    string[] columns = table.Value;

                    // Create table with ID only (minimal required)
                    string createTableQuery = $"CREATE TABLE IF NOT EXISTS {tableName} (id INT PRIMARY KEY AUTO_INCREMENT);";
                    using (MySqlCommand createTableCmd = new MySqlCommand(createTableQuery, conn))
                    {
                        createTableCmd.ExecuteNonQuery();
                    }

                    // Check & Add each column
                    foreach (string columnDef in columns)
                    {
                        string[] parts = columnDef.Split(' ');
                        string columnName = parts[0];

                        string checkColumn = @"
                            SELECT COUNT(*) 
                            FROM INFORMATION_SCHEMA.COLUMNS 
                            WHERE TABLE_SCHEMA = @dbName 
                                AND TABLE_NAME = @tableName 
                                AND COLUMN_NAME = @columnName;";

                        using (MySqlCommand checkCmd = new MySqlCommand(checkColumn, conn))
                        {
                            checkCmd.Parameters.AddWithValue("@dbName", dbName);
                            checkCmd.Parameters.AddWithValue("@tableName", tableName);
                            checkCmd.Parameters.AddWithValue("@columnName", columnName);

                            long exists = (long)checkCmd.ExecuteScalar();
                            if (exists == 0)
                            {
                                string alterTable = $"ALTER TABLE {tableName} ADD COLUMN {columnDef};";
                                using (MySqlCommand alterCmd = new MySqlCommand(alterTable, conn))
                                {
                                    alterCmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }

                // 4. Insert default login if not exists
                string checkLoginExists = "SELECT COUNT(*) FROM login;";
                using (MySqlCommand cmd = new MySqlCommand(checkLoginExists, conn))
                {
                    long count = (long)cmd.ExecuteScalar();
                    if (count == 0)
                    {
                        string insertDefaultLogin = "INSERT INTO login (username, password) VALUES ('admin', 'password');";
                        using (MySqlCommand insertCmd = new MySqlCommand(insertDefaultLogin, conn))
                        {
                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
    }
}
