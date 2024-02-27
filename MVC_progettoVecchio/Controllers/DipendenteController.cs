using MVC_progettoVecchio.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace MVC_progettoVecchio.Controllers
{
    public class DipendenteController : Controller
    {
        // GET: Dipendente
        public ActionResult Index()
        {
            // scrivi la stringa di connessione al database
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            List<Dipendente> Listadipendenti = new List<Dipendente>();
            try
            {
                string query;
                conn.Open();
                query = "SELECT * FROM Dipendenti";
                SqlCommand cmd = new SqlCommand(query, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Se il reader ha delle righe, allora esiste un utente con quel nome e password

                    // Utente trovato

                    while (reader.Read())
                    {
                        int idUtente = reader.GetInt32(0);
                        string Nome = reader.GetString(1);
                        string cognome = reader.GetString(2);
                        string Indirizzo = reader.GetString(3);
                        string CodiceFiscale = reader.GetString(4);
                        bool coniugato = reader.GetBoolean(5);
                        int NumFigli = reader.GetInt32(6);
                        string Mansione = reader.GetString(7);

                        Dipendente objDipendente = new Dipendente(idUtente, Nome, cognome, Indirizzo, CodiceFiscale, coniugato, NumFigli, Mansione);
                        Listadipendenti.Add(objDipendente);
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
            return View(Listadipendenti);
        }

        // GET: Dipendente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dipendente/Create
        [HttpPost]
        public ActionResult Create(Dipendente ObjDipendente)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);


            try
            {

                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "INSERT INTO Dipendenti (Nome, Cognome, Indirizzo, CodiceFiscale, Coniugato, NumeroFigli, Mansione)" +
                    " VALUES (@nome , @cognome , @Indirizzo , @CodicaFiscale , @Coniugato , @NumFigli , @Mansione)";
                cmd.Parameters.AddWithValue("@Nome", ObjDipendente.Nome);
                cmd.Parameters.AddWithValue("@Cognome", ObjDipendente.Cognome);
                cmd.Parameters.AddWithValue("@Indirizzo", ObjDipendente.Indirizzo);
                cmd.Parameters.AddWithValue("@CodicaFiscale", ObjDipendente.CodiceFiscale);
                cmd.Parameters.AddWithValue("@Coniugato", ObjDipendente.Coniugato);
                cmd.Parameters.AddWithValue("@NumFigli", ObjDipendente.NumeroFigli);
                cmd.Parameters.AddWithValue("@Mansione", ObjDipendente.Mansione);

                cmd.ExecuteNonQuery();


                // TODO: Add insert logic here

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


        public ActionResult Edit(int id)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            Dipendente DipendenteDaModificare = new Dipendente();

            try
            {

                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM Dipendenti WHERE IdUtente = @id";

                cmd.Parameters.AddWithValue("@id", id);

                using (SqlDataReader reader = cmd.ExecuteReader())



                    if (reader.Read())
                    {
                        int idUtente = reader.GetInt32(0);
                        string nomelavoratore = reader.GetString(1);
                        string cognome = reader.GetString(2);
                        string Indirizzo = reader.GetString(3);
                        string CodiceFiscale = reader.GetString(4);
                        bool coniugato = reader.GetBoolean(5);
                        int NumFigli = reader.GetInt32(6);
                        string Mansione = reader.GetString(7);

                        DipendenteDaModificare = new Dipendente(idUtente, nomelavoratore, cognome, Indirizzo, CodiceFiscale, coniugato, NumFigli, Mansione);

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

            return View(DipendenteDaModificare);
            // return View("~/Views/Shared/Error.cshtml");
        }

        [HttpPost]
        public ActionResult Edit(Dipendente Dipendente)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            // Dipendente DipendenteDaModificare = new Dipendente();

            try
            {

                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE Dipendenti SET Nome = @Nome, Cognome = @Cognome, Indirizzo = @Indirizzo, CodiceFiscale = @CodiceFiscale, Coniugato = @Coniugato, NumeroFigli = @NumeroFigli, Mansione = @Mansione WHERE IdUtente = @id";

                cmd.Parameters.AddWithValue("@Nome", Dipendente.Nome);
                cmd.Parameters.AddWithValue("@Cognome", Dipendente.Cognome);
                cmd.Parameters.AddWithValue("@Indirizzo", Dipendente.Indirizzo);
                cmd.Parameters.AddWithValue("@CodiceFiscale", Dipendente.CodiceFiscale);
                cmd.Parameters.AddWithValue("@Coniugato", Dipendente.Coniugato);
                cmd.Parameters.AddWithValue("@NumeroFigli", Dipendente.NumeroFigli);
                cmd.Parameters.AddWithValue("@Mansione", Dipendente.Mansione);
                cmd.Parameters.AddWithValue("@id", Dipendente.IdUtente);

                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return RedirectToAction("Index"); ;
        }


        public ActionResult Delete(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {

                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM Dipendenti WHERE IdUtente = @id";

                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return RedirectToAction("Index");
        }







    }
}
