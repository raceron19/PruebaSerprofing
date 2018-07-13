using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using PruebaSerprofing.Models;

namespace PruebaSerprofing.Controllers
{
    public class OficinaController : Controller
    {
        string connectionString = @"Data Source = LAPTOP-2SPSCB7F\SQLEXPRESS; Initial Catalog = Serprofing; Integrated Security=True";
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dtbOficina = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("select * from oficina",sqlCon);
                sqlDa.Fill(dtbOficina);
            }
                return View(dtbOficina);
            
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new OficinaModel());
        }

        [HttpPost]
        public ActionResult Create(OficinaModel oficinadoModel)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "insert into Oficina values(@descripcion,@telefono,@direccion)";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@descripcion", oficinadoModel.Descripcion);
                    sqlCmd.Parameters.AddWithValue("@telefono", oficinadoModel.Telefono);
                    sqlCmd.Parameters.AddWithValue("@direccion", oficinadoModel.Direccion);
                    sqlCmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                
                throw;
            }
            
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            OficinaModel ModelOficina = new OficinaModel();
            DataTable dtbOfi = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "select * from Oficina where idOficina=@idOfi";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@idOfi", id);
                sqlDa.Fill(dtbOfi);
            }
            if (dtbOfi.Rows.Count == 1)
            {
                ModelOficina.idOficina = Convert.ToInt32(dtbOfi.Rows[0][0].ToString());
                ModelOficina.Descripcion = (dtbOfi.Rows[0][1].ToString());
                ModelOficina.Telefono = Convert.ToInt32(dtbOfi.Rows[0][2].ToString());
                ModelOficina.Direccion = (dtbOfi.Rows[0][3].ToString());
                return View(ModelOficina);
            }
            else
                return RedirectToAction("Index");
           
        }

        [HttpPost]
        public ActionResult Edit(OficinaModel oficinaModel)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "update oficina set descripcion=@desc, telefono=@tele, direccion=@dire where idOficina=@idOfi";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@desc", oficinaModel.Descripcion);
                sqlCmd.Parameters.AddWithValue("@tele", oficinaModel.Telefono);
                sqlCmd.Parameters.AddWithValue("@dire", oficinaModel.Direccion);
                sqlCmd.Parameters.AddWithValue("@idOfi", oficinaModel.idOficina);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            using(SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "delete from Oficina where idOficina=@idOfi";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@idOfi", id);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
    }
}