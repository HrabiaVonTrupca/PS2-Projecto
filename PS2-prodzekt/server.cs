using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Diagnostics;

namespace PS2_prodzekt
{
    public class ChatHub : Hub
    {
        public void Send(string message, string conID)
        {
            string userID = Context.ConnectionId;

            // Clients.Client(conID).broadcastMessage(message);

            Debug.WriteLine("User " + userID + "send message: " + message);

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "SERVER=ps2serwer.database.windows.net;DATABASE=PS2FinalProject;USER ID=ps2admin;PASSWORD=Maslo2017;";
                conn.Open();

                SqlCommand command = new SqlCommand(message, conn);

                // command.Parameters.Add(new SqlParameter("0", 1));

                /* Get the rows and display on the screen! 
                 * This section of the code has the basic code
                 * that will display the content from the Database Table
                 * on the screen using an SqlDataReader. */

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Clients.Client(conID).getTables(reader[0]);
                    }
                }

                // Create the command, to insert the data into the Table!
                // this is a simple INSERT INTO command!

                // SqlCommand insertCommand = new SqlCommand("INSERT INTO TableName (FirstColumn, SecondColumn, ThirdColumn, ForthColumn) VALUES (@0, @1, @2, @3)", conn);

                // In the command, there are some parameters denoted by @, you can 
                // change their value on a condition, in my code they're hardcoded.

                // insertCommand.Parameters.Add(new SqlParameter("0", 10));
                // insertCommand.Parameters.Add(new SqlParameter("1", "Test Column"));
                // insertCommand.Parameters.Add(new SqlParameter("2", DateTime.Now));
                // insertCommand.Parameters.Add(new SqlParameter("3", false));
            }
        }

        public override Task OnConnected()
        {
            var name = Context.ConnectionId;
            Debug.WriteLine(name.ToString() + "  connected");

            return base.OnConnected();
        }
    }
}
