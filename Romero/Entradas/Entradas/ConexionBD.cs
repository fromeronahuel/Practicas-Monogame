using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace Entradas
{
    internal class ConexionBD
    {
        public void Conexion ()
        {
            string stringConexion = "Server=localhost;Port=3306; Database=base_datos;Pwd=;";
            using (MySqlConnection conexion = new MySqlConnection(stringConexion))
            {
                conexion.Open ();
            }
        }

        public void GuardarProgreso(string nombreJugador, int nivel)
        {


            string query = "INSERT INTO progreso (nombreJugador,nivel) VALUES (@nombreJugador, @Nivel)";

            //Try-catch

            try {
              

                    using (MySqlCommand comando = new MySqlCommand(query))
                    {
                    Conexion();
                        comando.Parameters.AddWithValue("@nombreJugador", nombreJugador);
                        comando.Parameters.AddWithValue("@nivel", nivel);
                        comando.ExecuteNonQuery(); //Ejecutamos INSERT
                    }
                


            }


            catch (MySqlException ex) {
                   System.Diagnostics.Debug.WriteLine("MYSQL ERROR", ex.Message);
            }

        }
    }
}
