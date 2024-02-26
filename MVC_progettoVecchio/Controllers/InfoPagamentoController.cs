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
        // GET: InfoPagamento
        public ActionResult Index()
        {
            // scrivi la stringa di connessione al database
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            List<InfoPagamento> ListaPagamenti = new List<InfoPagamento>();
            try
            {
                string query;
                conn.Open();
                query = "SELECT * FROM InfoPagamenti";
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
            //try
            //{

            //    conn.Open();

            //    SqlCommand cmd = new SqlCommand();
            //    cmd.Connection = conn;

            //    cmd.CommandText = "INSERT INTO Dipendenti (Nome, Cognome, Indirizzo, CodiceFiscale, Coniugato, NumeroFigli, Mansione)" +
            //        " VALUES (@nome , @cognome , @Indirizzo , @CodicaFiscale , @Coniugato , @NumFigli , @Mansione)";
            //    cmd.Parameters.AddWithValue("@Nome", ObjDipendente.Nome);
            //    cmd.Parameters.AddWithValue("@Cognome", ObjDipendente.Cognome);
            //    cmd.Parameters.AddWithValue("@Indirizzo", ObjDipendente.Indirizzo);
            //    cmd.Parameters.AddWithValue("@CodicaFiscale", ObjDipendente.CodiceFiscale);
            //    cmd.Parameters.AddWithValue("@Coniugato", ObjDipendente.Coniugato);
            //    cmd.Parameters.AddWithValue("@NumFigli", ObjDipendente.NumeroFigli);
            //    cmd.Parameters.AddWithValue("@Mansione", ObjDipendente.Mansione);

            //    cmd.ExecuteNonQuery();


            //    // TODO: Add insert logic here

            //    return RedirectToAction("Index");
            //}
            //catch (Exception ex)
            //{
            //    Response.Write(ex.Message);
            //}
            //finally
            //{
            //    conn.Close();
            //}


        }

        // GET: InfoPagamento/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // POST: InfoPagamento/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: InfoPagamento/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: InfoPagamento/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
