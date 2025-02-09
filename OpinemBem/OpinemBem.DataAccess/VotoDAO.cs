﻿using OpinemBem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace OpinemBem.DataAccess
{
    public class VotoDAO
    {
        public void Inserir(Voto obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = "INSERT INTO voto (id_usuario, id_projeto, data_voto, valor) VALUES ( @id_usuario, @id_projeto, @data_voto, @valor);";
                {
                    using (SqlCommand cmd = new SqlCommand(strSQL))
                    {
                        cmd.Connection = conn;

                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = obj.Usuario.Id;
                        cmd.Parameters.Add("@id_projeto", SqlDbType.Int).Value = obj.ProjetoDeLei.Id;
                        cmd.Parameters.Add("@data_voto", SqlDbType.DateTime).Value = obj.DataVoto;
                        cmd.Parameters.Add("@valor", SqlDbType.VarChar).Value = obj.Valor;

                        foreach (SqlParameter parameter in cmd.Parameters)
                        {
                            if (parameter.Value == null)
                            {
                                parameter.Value = DBNull.Value;
                            }
                        }

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
        }

        public void Atualizar(Voto obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = "UPDATE voto set voto = @voto where id_projeto = @id_projeto and id_usuario = @id_usuario";
                {
                    using (SqlCommand cmd = new SqlCommand(strSQL))
                    {
                        cmd.Connection = conn;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = obj.Usuario.Id;
                        cmd.Parameters.Add("@id_projeto", SqlDbType.Int).Value = obj.ProjetoDeLei.Id;
                        cmd.Parameters.Add("@data_voto", SqlDbType.DateTime).Value = obj.DataVoto;
                        cmd.Parameters.Add("@valor", SqlDbType.VarChar).Value = obj.Valor;

                        foreach (SqlParameter parameter in cmd.Parameters)
                        {
                            if (parameter.Value == null)
                            {
                                parameter.Value = DBNull.Value;
                            }
                        }

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
        }

        public Voto BuscarPorId(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = "SELECT * FROM voto where id_projeto = @id_projeto;";
                {
                    using (SqlCommand cmd = new SqlCommand(strSQL))
                    {
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.Parameters.Add("@id_projeto", SqlDbType.Int).Value = id;
                        cmd.CommandText = strSQL;

                        var dataReader = cmd.ExecuteReader();
                        var dt = new DataTable();
                        dt.Load(dataReader);

                        conn.Close();

                        if (!(dt != null && dt.Rows.Count > 0))
                            return null;

                        var row = dt.Rows[0];
                        var voto = new Voto()
                        {
                            Id = Convert.ToInt32(row["id_voto"]),
                            Usuario = new Usuario() { Id = Convert.ToInt32(row["id_usuario"]) },
                            ProjetoDeLei = new ProjetoDeLei() { Id = Convert.ToInt32(row["id_projeto"]) },
                            DataVoto = Convert.ToDateTime(row["data_voto"]),
                            Valor = row["valor"].ToString()
                        };
                        return voto;
                    }
                }
            }
        }

        public List<Voto> BuscarTodos()
        {
            var lst = new List<Voto>();
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
                {
                    string strSQL = @"SELECT * FROM projeto_de_lei;";

                    using (SqlCommand cmd = new SqlCommand(strSQL))
                    {
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.CommandText = strSQL;

                        var dataReader = cmd.ExecuteReader();
                        var dt = new DataTable();
                        dt.Load(dataReader);

                        conn.Close();

                        foreach (DataRow row in dt.Rows)
                        {
                            var voto = new Voto()
                            {
                                Id = Convert.ToInt32(row["id_voto"]),
                                Usuario = new Usuario() { Id = Convert.ToInt32(row["id_usuario"]) },
                                ProjetoDeLei = new ProjetoDeLei() { Id = Convert.ToInt32(row["id_projeto"]) },
                                DataVoto = Convert.ToDateTime(row["data_voto"]),
                                Valor = row["valor"].ToString()
                            };
                            lst.Add(voto);
                        }
                    }
                }
                return lst;
            }
        }

        public Voto BuscarVoto(int projeto, int usuario)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = "SELECT top 1 * FROM voto where id_projeto = @id_projeto and id_usuario = @id_usuario;";
                {
                    using (SqlCommand cmd = new SqlCommand(strSQL))
                    {
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.Parameters.Add("@id_projeto", SqlDbType.Int).Value = projeto;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = usuario;
                        cmd.CommandText = strSQL;

                        var dataReader = cmd.ExecuteReader();
                        var dt = new DataTable();
                        dt.Load(dataReader);

                        conn.Close();

                        if (!(dt != null && dt.Rows.Count > 0))
                            return null;

                        var row = dt.Rows[0];
                        var voto = new Voto()
                        {
                            Id = Convert.ToInt32(row["id_voto"]),
                            Usuario = new Usuario() { Id = Convert.ToInt32(row["id_usuario"]) },
                            ProjetoDeLei = new ProjetoDeLei() { Id = Convert.ToInt32(row["id_projeto"]) },
                            DataVoto = Convert.ToDateTime(row["data_voto"]),
                            Valor = row["valor"].ToString()
                        };
                        return voto;
                    }
                }
            }
        }
    }
}
