using MVC_progettoVecchio.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace MVC_progettoVecchio.Controllers
{
    public class InfoPagamentoController : Controller
    {
        string query;
        // GET: InfoPagamento
        public ActionResult Index(string id)
        {

            // scrivi la stringa di connessione al database

            if (id == null)
            {
                query = "SELECT * FROM InfoPagamenti";
            }
            else if (id == "1")
            {
                query = "SELECT * FROM InfoPagamenti ORDER BY PeriodoDelPagamento DESC";
            }
            else if (id == "2")
            {
                query = "SELECT * FROM InfoPagamenti ORDER BY PeriodoDelPagamento ASC";
            }

            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            List<InfoPagamento> ListaPagamenti = new List<InfoPagamento>();

            try
            {

                conn.Open();


                SqlCommand cmd = new SqlCommand(query, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Se il reader ha delle righe, allora esiste un utente con quel nome e password

                    // Utente trovato

                    while (reader.Read())
                    {
                        int idPagamento = reader.GetInt32(0);
                        DateTime periodoDelPagamento = reader.GetDateTime(1);
                        string TipoDiPagamento = reader.GetString(2);
                        decimal Importo = reader.GetDecimal(3);
                        int Idlavoratore = reader.GetInt32(4);


                        InfoPagamento objPagamento = new InfoPagamento(idPagamento, Convert.ToDateTime(periodoDelPagamento), TipoDiPagamento, Convert.ToDecimal(Importo), Idlavoratore);
                        ListaPagamenti.Add(objPagamento);
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("Errore ");
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return View(ListaPagamenti);
        }



        // GET: InfoPagamento/Create
        public ActionResult Create()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            List<int> ListaId = new List<int>();

            try
            {

                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "SELECT idPagamento FROM InfoPagamenti";
                using (SqlDataReader reader = cmd.ExecuteReader())

                    while (reader.Read())
                    {
                        int idPagamento = reader.GetInt32(0);
                        ListaId.Add(idPagamento);
                        Session["listaDegliId"] = ListaId;
                    }


            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return View();
        }


        // POST: InfoPagamento/Create
        [HttpPost]
        public ActionResult Create(InfoPagamento objPagamento)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {

                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "INSERT INTO InfoPagamenti (PeriodoDelPagamento , TipoDiPagamento , Importo , IdLavoratore)" +
                    "VALUES (@periodoPagamento , @tipoPagamento , @Importo , @Idlavoratore)";
                cmd.Parameters.AddWithValue("@periodoPagamento", objPagamento.PeriodoDelPagamento);
                cmd.Parameters.AddWithValue("@tipoPagamento", objPagamento.TipoDiPagamento);
                cmd.Parameters.AddWithValue("@Importo", objPagamento.Importo);
                cmd.Parameters.AddWithValue("@Idlavoratore", objPagamento.idLavoratore);

                cmd.ExecuteNonQuery();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return View();

        }

        public ActionResult DettagliPagamento(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            InfoPagamento ObjPagamento = new InfoPagamento();
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM InfoPagamenti WHERE IdPagamento = @id";
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int idpagamento = reader.GetInt32(0);
                    DateTime periodoDelPagamento = reader.GetDateTime(1);
                    string TipoDiPagamento = reader.GetString(2);
                    decimal Importo = reader.GetDecimal(3);
                    int Idlavoratore = reader.GetInt32(4);

                    ObjPagamento.IdPagamento = idpagamento;
                    ObjPagamento.PeriodoDelPagamento = Convert.ToDateTime(periodoDelPagamento);
                    ObjPagamento.TipoDiPagamento = TipoDiPagamento;
                    ObjPagamento.Importo = Convert.ToDecimal(Importo);
                    ObjPagamento.idLavoratore = Idlavoratore;
                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return View(ObjPagamento);
        }

    }
}
