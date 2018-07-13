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
    public class EmpleadoController:Controller
    {
        string connectionString = @"Data Source = LAPTOP-2SPSCB7F\SQLEXPRESS; Initial Catalog = Serprofing; Integrated Security=True";

        [HttpGet]
        public ActionResult Index()
        {
            DataTable dtbOficina = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("select empleado.idEmpleado, empleado.nombre, empleado.dui, empleado.direccion, empleado.telefono, empleado.fechaIngreso, empleado.activo, empleado.idOficina, Oficina.descripcion from empleado, Oficina where empleado.idOficina = Oficina.idOficina", sqlCon);
                sqlDa.Fill(dtbOficina);
            }
            return View(dtbOficina);

        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new EmpleadoModel());
        }

        [HttpPost]
        public ActionResult Create(EmpleadoModel empleadoModel)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "insert into empleado values(@nombre,@dui,@dir,@tel,@fec,@act,@idOf)";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@nombre", empleadoModel.nombre);
                    sqlCmd.Parameters.AddWithValue("@dui", empleadoModel.dui);
                    sqlCmd.Parameters.AddWithValue("@dir", empleadoModel.direccion);
                    sqlCmd.Parameters.AddWithValue("@tel", empleadoModel.telefono);
                    sqlCmd.Parameters.AddWithValue("@fec", empleadoModel.fechaIngreso);
                    sqlCmd.Parameters.AddWithValue("@act", empleadoModel.activo);
                    sqlCmd.Parameters.AddWithValue("@idOf", empleadoModel.idOficina);
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
            EmpleadoModel  empleadoModel = new EmpleadoModel();
            DataTable dtbOfi = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "select * from empleado where idEmpleado=@idEmp";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@idEmp", id);
                sqlDa.Fill(dtbOfi);
            }
            if (dtbOfi.Rows.Count == 1)
            {
                empleadoModel.idEmpleado = Convert.ToInt32(dtbOfi.Rows[0][0].ToString());
                empleadoModel.nombre = (dtbOfi.Rows[0][1].ToString());
                empleadoModel.dui = Convert.ToInt32(dtbOfi.Rows[0][2].ToString());
                empleadoModel.direccion = (dtbOfi.Rows[0][3].ToString());
                empleadoModel.telefono = Convert.ToInt32(dtbOfi.Rows[0][4].ToString());
                empleadoModel.fechaIngreso = Convert.ToDateTime(dtbOfi.Rows[0][5].ToString());
                empleadoModel.activo = Convert.ToInt32(dtbOfi.Rows[0][6].ToString());
                empleadoModel.idOficina = Convert.ToInt32(dtbOfi.Rows[0][7].ToString());
                return View(empleadoModel);
            }
            else
                return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult Edit(EmpleadoModel empleadoModel)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "update empleado set nombre=@nombre, dui=@dui, direccion=@dir, telefono=@tel, fechaIngreso=@fec, activo=@act, idOficina=@idOf where idEmpleado=@idEmp";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@nombre", empleadoModel.nombre);
                sqlCmd.Parameters.AddWithValue("@dui", empleadoModel.dui);
                sqlCmd.Parameters.AddWithValue("@dir", empleadoModel.direccion);
                sqlCmd.Parameters.AddWithValue("@tel", empleadoModel.telefono);
                sqlCmd.Parameters.AddWithValue("@fec", empleadoModel.fechaIngreso);
                sqlCmd.Parameters.AddWithValue("@act", empleadoModel.activo);
                sqlCmd.Parameters.AddWithValue("@idOf", empleadoModel.idOficina);
                sqlCmd.Parameters.AddWithValue("@idEmp", empleadoModel.idEmpleado);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "delete from empleado where idEmpleado=@idEmp";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@idEmp", id);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

    }
}
